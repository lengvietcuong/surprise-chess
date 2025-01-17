namespace SurpriseChess;

public class ChessModel
{
    public Board Board { get; private set; } = null!;
    private readonly IBoardSetup boardSetup;
    private readonly IChessBot chessBot;
    private Arbiter arbiter = null!;
    private EffectApplier effectApplier = null!;
    private readonly Random random = new();

    public GameMode GameMode { get; private set; }
    public GameState GameState { get; private set; } = null!;
    public GameResult Result { get; private set; }

    public Position? SelectedPosition { get; private set; }
    public HashSet<Position> HighlightedMoves { get; private set; } = null!;

    public ChessModel(IBoardSetup boardSetup, IChessBot chessBot)
    {
        this.boardSetup = boardSetup;
        this.chessBot = chessBot;
    }

    public void NewGame(GameMode gameMode)
    {
        Board = new Board(boardSetup);
        GameMode = gameMode;
        GameState = new(Board);
        Result = GameResult.InProgress;
        arbiter = new Arbiter(Board, GameState);
        effectApplier = new EffectApplier(Board);
        SelectedPosition = null;
        HighlightedMoves = new HashSet<Position>();
    }

    public void Select(Position position)
    {
        SelectedPosition = position;
        HighlightedMoves = arbiter.GetLegalMoves(position);
    }

    public void Deselect()
    {
        SelectedPosition = null;
        HighlightedMoves.Clear();
    }

    public void HandleMoveTo(Position destination)
    {
        if (SelectedPosition == null) return;

        GameState.UpdateStateAfterMove(SelectedPosition, destination);
        Board.MakeMove(SelectedPosition, destination);

        effectApplier.ClearEffects();
        effectApplier.ApplyEffects(destination);

        Result = arbiter.GetGameResult(GameState.CurrentPlayerColor);
        Deselect();

        // Check if the bot needs to make a move
        if (Result == GameResult.InProgress && IsBotsTurn)
        {
            HandleBotMove();
        }
    }

    public bool IsBotsTurn => (
        // Currently, the bot's color is always black (randomized colors and board flipping may come in the future)
        GameMode == GameMode.PlayerVsBot
        && GameState.CurrentPlayerColor == PieceColor.Black
    );

    public async void HandleBotMove()
    {
        (Position source, Position destination) = await GetBotMove();
        Select(source);
        await Task.Delay(1000);  // Wait 1s so the player has time to see the move
        HandleMoveTo(destination);
    }

    private async Task<(Position, Position)> GetBotMove()
    {
        string fen = FEN.GetFEN(Board, GameState);
        List<(Position, Position)> bestMoves = await chessBot.GetBestMoves(fen);

        // The bot doesn't know the special effects (paralysis and shield),
        // so check if the bot's move is legal before making it
        foreach ((Position source, Position destination) in bestMoves)
        {
            if (arbiter.GetLegalMoves(source).Contains(destination))
            {
                return (source, destination);
            }
        }
        // If the bot's didn't generate a legal move, choose a random move
        return GetRandomMove();
    }

    private (Position, Position) GetRandomMove()
    {
        List<(Position, Position)> legalMoves = new();
        foreach ((Position source, Piece _) in Board.LocatePieces(GameState.CurrentPlayerColor))
        {
            foreach (Position destination in arbiter.GetLegalMoves(source))
            {
                legalMoves.Add((source, destination));
            }
        }
        return legalMoves[random.Next(legalMoves.Count)];
    }
}
