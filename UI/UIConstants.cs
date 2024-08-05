using SplashKitSDK;

namespace SurpriseChess;

public static class UIConstants
{
    public const string WindowTitle = "Surprise Chess";
    public const string BoldFontName = "RobotoSlabBold";
    public const string RegularFontName = "RobotoSlabRegular";

    public const int SquareSize = 75;
    public const int BoardSize = 8 * SquareSize;
    public const int SmallCircleSize = 15;
    public const int BigCircleSize = SquareSize / 2;

    public const int WindowWidth = BoardSize;
    public const int WindowHeight = BoardSize;
    public const int ButtonWidth = 225;
    public const int ButtonHeight = 50;

    public const int LargeFontSize = 48;
    public const int MediumFontSize = 20;
    public const int SmallFontSize = 16;

    public static readonly Color TransparentGold = Color.RGBAColor(227, 170, 36, 100);
    public static readonly Color TransparentBlack = Color.RGBAColor(0, 0, 0, 35);
    public static readonly Color Transparent = Color.RGBAColor(0, 0, 0, 0);
    public static readonly Color LightGrayOverlay = Color.RGBAColor(0, 0, 0, 160);
    public static readonly Color DarkGrayOverlay = Color.RGBAColor(0, 0, 0, 190);
    public static readonly Color Gold = Color.RGBAColor(227, 170, 36, 255);
}