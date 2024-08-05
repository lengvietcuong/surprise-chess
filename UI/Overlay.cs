using SplashKitSDK;

namespace SurpriseChess;

public class Overlay : UIElement
{
    private readonly Color color;

    public Overlay(Color color)
    {
        this.color = color;
    }

    public override void Draw()
    {
        SplashKit.FillRectangle(color, 0, 0, UIConstants.WindowWidth, UIConstants.WindowHeight);
    }
}