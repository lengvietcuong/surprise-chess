namespace SurpriseChess;

public class Program
{
    public static void Main()
    {
        IBoardSetup boardSetup = new Chess960();
        IChessBot chessBot = new StockFish();
        
        ChessModel model = new(boardSetup, chessBot);
        ChessView view = new();
        ChessController controller = new(model, view);

        controller.Run();
    }
}