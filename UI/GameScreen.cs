using SplashKitSDK;

namespace SurpriseChess;

public class GameScreen : UIElement
{
    public IBoardView Board { get; set; }
    public Position? SelectedPosition { get; set; }
    public IEnumerable<Position> HighlightedMoves { get; set; }
    private readonly AssetManager assets;

    public GameScreen(
        IBoardView board,
        Position? selectedPosition,
        IEnumerable<Position> highlightedMoves,
        AssetManager assets
    )
    {
        Board = board;
        SelectedPosition = selectedPosition;
        HighlightedMoves = highlightedMoves;
        this.assets = assets;
    }

    public override void Draw()
    {
        DrawBoard();
        HighlightSelectedPosition();
        HighlightMoves();
        DrawPieces();
    }

    private void DrawBoard()
    {
        SplashKit.DrawBitmap(assets.Board, 0, 0);
    }

    private void DrawPieces()
    {
        foreach ((Position position, Piece piece) in Board.LocatePieces())
        {
            if (piece.IsInvisible) continue;

            Position coordinates = GetDrawingCoordinates(position);
            // Draw piece
            SplashKit.DrawBitmap(
                assets.Piece(piece.Color, piece.Type),
                coordinates.Col,
                coordinates.Row
            );
            // Draw special effects
            if (piece.IsParalyzed)
            {
                SplashKit.DrawBitmap(assets.Chains, coordinates.Col, coordinates.Row);
            }
            if (piece.IsShielded)
            {
                SplashKit.DrawBitmap(assets.Shield, coordinates.Col, coordinates.Row);
            }
        }
    }

    private void HighlightMoves()
    {
        foreach (Position position in HighlightedMoves)
        {
            Position coordinates = GetDrawingCoordinates(position);
            Piece? piece = Board.GetPieceAt(position);
            // Draw a small circle to indicate an empty square, or a big circle to indicate a capture
            int circleSize = piece == null ? UIConstants.SmallCircleSize : UIConstants.BigCircleSize;
            SplashKit.FillCircle(
                UIConstants.TransparentBlack,
                coordinates.Col + UIConstants.SquareSize / 2,
                coordinates.Row + UIConstants.SquareSize / 2,
                circleSize
            );
        }
    }

    private void HighlightSelectedPosition()
    {
        if (SelectedPosition == null) return;

        Position coordinates = GetDrawingCoordinates(SelectedPosition);
        SplashKit.FillRectangle(
            UIConstants.TransparentGold, coordinates.Col, coordinates.Row, UIConstants.SquareSize, UIConstants.SquareSize
        );
    }

    private static Position GetDrawingCoordinates(Position position) =>
    // Invert Y-axis because white starts at row 0 but needs to be drawn at the bottom
    new((7 - position.Row) * UIConstants.SquareSize, position.Col * UIConstants.SquareSize);
}
