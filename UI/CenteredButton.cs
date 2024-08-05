using SplashKitSDK;

namespace SurpriseChess;

public class CenteredButton : UIElement
{
    private readonly string text;
    private readonly Color backgroundColor;
    private readonly Color textColor;
    private readonly double marginTop;

    public CenteredButton(string text, Color backgroundColor, Color textColor, double marginTop)
    {
        this.text = text;
        this.backgroundColor = backgroundColor;
        this.textColor = textColor;
        this.marginTop = marginTop;
    }

    private Rectangle GetButtonRect()
    {
        double x = UIConstants.WindowWidth / 2 - UIConstants.ButtonWidth / 2;
        double y = marginTop - UIConstants.ButtonHeight / 2;
        return SplashKit.RectangleFrom(x, y, UIConstants.ButtonWidth, UIConstants.ButtonHeight);
    }

    public override void Draw()
    {
        // Draw button rectangular background
        Rectangle buttonRect = GetButtonRect();
        SplashKit.FillRectangle(backgroundColor, buttonRect);

        // Draw centered button text
        CenteredText buttonText = new(text, textColor, UIConstants.BoldFontName, UIConstants.MediumFontSize, marginTop);
        buttonText.Draw();
    }

    public bool IsClicked()
    {
        if (!SplashKit.MouseClicked(MouseButton.LeftButton)) return false;

        Rectangle buttonRect = GetButtonRect();
        return SplashKit.PointInRectangle(SplashKit.MousePosition(), buttonRect);
    }
}
