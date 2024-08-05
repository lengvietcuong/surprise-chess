using SplashKitSDK;

namespace SurpriseChess;

public class AssetManager
{
    public Bitmap GameArt { get; private set; }
    public Bitmap Board { get; private set; }
    public Bitmap Chains { get; private set; }
    public Bitmap Shield { get; private set; }
    private readonly Dictionary<(PieceColor, PieceType), Bitmap> pieceBitmaps;

    public AssetManager()
    {
        // Initialize fonts
        SplashKit.LoadFont(UIConstants.BoldFontName, "fonts/RobotoSlab-Bold.ttf");
        SplashKit.LoadFont(UIConstants.RegularFontName, "fonts/RobotoSlab-Regular.ttf");

        // Initialize bitmaps
        GameArt = SplashKit.LoadBitmap("game-art", "images/game-art.png");
        Board = SplashKit.LoadBitmap("board", "images/board.png");
        Chains = SplashKit.LoadBitmap("chains", "images/chains.png");
        Shield = SplashKit.LoadBitmap("shield", "images/shield.png");
        pieceBitmaps = new();
        foreach (PieceColor color in Enum.GetValues(typeof(PieceColor)))
        {
            foreach (PieceType type in Enum.GetValues(typeof(PieceType)))
            {
                string lowercaseColor = color.ToString().ToLower();
                string lowercaseType = type.ToString().ToLower();
                string path = $"images/{lowercaseColor}-{lowercaseType}.png";
                Bitmap bitmap = SplashKit.LoadBitmap($"{lowercaseColor}-{lowercaseType}", path);
                pieceBitmaps[(color, type)] = bitmap;
            }
        }
    }

    public Bitmap Piece(PieceColor color, PieceType type) => pieceBitmaps[(color, type)];
}