using SplashKitSDK;

namespace SurpriseChess;

public class CenteredText : UIElement
{
    private readonly string text;
    private readonly Color color;
    private readonly string fontName;
    private readonly int fontSize;
    private readonly double marginTop;

    public CenteredText(string text, Color color, string fontName, int fontSize, double marginTop)
    {
        this.text = text;
        this.color = color;
        this.fontName = fontName;
        this.fontSize = fontSize;
        this.marginTop = marginTop;
    }

    public override void Draw()
    {
        double x = UIConstants.WindowWidth / 2;
        double y = marginTop;
        SplashKit.DrawText(
            text,
            color,
            fontName,
            fontSize,
            x - SplashKit.TextWidth(text, fontName, fontSize) / 2,
            y - SplashKit.TextHeight(text, fontName, fontSize) / 2
        );
    }
}
