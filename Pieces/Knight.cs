namespace SurpriseChess;

public class Knight : SimpleMovementPiece
{
    private static readonly (int, int)[] KnightMoveOffsets = new (int, int)[]
    {
            // The knight can move to the 8 L-shaped squares surrounding it
            (-2, 1), (-1, 2), (1, 2), (2, 1),
            (2, -1), (1, -2), (-1, -2), (-2, -1)
    };

    public Knight(PieceColor color) : base(color, PieceType.Knight, KnightMoveOffsets) { }
}