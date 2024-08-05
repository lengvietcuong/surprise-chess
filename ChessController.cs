using SplashKitSDK;

namespace SurpriseChess;

public class ChessController
{
    private readonly ChessModel model;
    private readonly ChessView view;
    private Screen currentScreen;

    public ChessController(ChessModel model, ChessView view)
    {
        this.model = model;
        this.view = view;
        currentScreen = Screen.Home;
    }

    public void Run()
    {
        view.OpenGameWindow();

        while (view.IsGameWindowOpen)
        {
            SplashKit.ProcessEvents();
            UpdateScreen();
            SplashKit.RefreshScreen(60);
        }

        view.CloseGameWindow();
    }

    private void UpdateScreen()
    {
        if (currentScreen == Screen.Home)
        {
            view.DrawHomeScreen();
            if (view.IsPlayWithPlayerButtonClicked)
            {
                model.NewGame(GameMode.PlayerVsPlayer);
                currentScreen = Screen.Game;
            }
            else if (view.IsPlayWithBotButtonClicked)
            {
                model.NewGame(GameMode.PlayerVsBot);
                currentScreen = Screen.Game;
            }
        }
        else if (currentScreen == Screen.Game)
        {
            view.DrawGameScreen(model.Board, model.SelectedPosition, model.HighlightedMoves);
            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                HandleBoardClick(GetBoardPositionOfClick());
            }
            if (model.Result != GameResult.InProgress)
            {
                currentScreen = Screen.GameOver;
            }
        }
        else if (currentScreen == Screen.GameOver)
        {
            view.DrawGameScreen(model.Board, model.SelectedPosition, model.HighlightedMoves);  // Still keep the game screen so players can see the final board state
            view.DrawGameOverScreen(model.Result);
            if (view.IsHomeButtonClicked)
            {
                currentScreen = Screen.Home;
            }
        }
    }

    private static Position GetBoardPositionOfClick()
    {
        int row = 7 - (int)(SplashKit.MouseY() / UIConstants.SquareSize);
        int col = (int)(SplashKit.MouseX() / UIConstants.SquareSize);
        return new Position(row, col);
    }

    private void HandleBoardClick(Position clickedSquare)
    {
        if (model.IsBotsTurn) return;  // Do nothing if it's the bot's turn

        // If the player clicks on one of the highlighted moves, move there
        if (model.HighlightedMoves.Contains(clickedSquare))
        {
            model.HandleMoveTo(clickedSquare);
            return;
        }

        // If the player clicks on one of their pieces, select it
        Piece? clickedPiece = model.Board.GetPieceAt(clickedSquare);
        if (clickedPiece?.Color == model.GameState.CurrentPlayerColor)
        {
            model.Select(clickedSquare);
        }
        else  // Deselect whatever's selected
        {
            model.Deselect();
        }
    }
}