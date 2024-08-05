using SplashKitSDK;

namespace SurpriseChess;

public class HomeScreen : UIElement
{
    private const string headingText = "Surprise Chess";
    private const string subheadingText = "Just like life, unfair and full of surprises.";

    private readonly AssetManager assets;
    private readonly Overlay overlay;
    private readonly CenteredText heading;
    private readonly CenteredText subheading;
    private readonly CenteredButton playWithBotButton;
    private readonly CenteredButton playWithPlayerButton;

    public HomeScreen(AssetManager assets)
    {
        this.assets = assets;
        overlay = new(UIConstants.LightGrayOverlay);
        heading = new(
            headingText,
            Color.White,
            UIConstants.BoldFontName,
            UIConstants.LargeFontSize,
            150
        );
        subheading = new(
            subheadingText,
            Color.White,
            UIConstants.RegularFontName,
            UIConstants.SmallFontSize,
            200
        );
        playWithBotButton = new("Play with Bot", UIConstants.Gold, Color.Black, 300);
        playWithPlayerButton = new("Play with Player", Color.White, Color.Black, 365);
    }

    public override void Draw()
    {
        SplashKit.DrawBitmap(assets.GameArt, 0, 0);
        overlay.Draw();
        heading.Draw();
        subheading.Draw();
        playWithBotButton.Draw();
        playWithPlayerButton.Draw();
    }

    public bool IsPlayWithBotButtonClicked => playWithBotButton.IsClicked();

    public bool IsPlayWithPlayerButtonClicked => playWithPlayerButton.IsClicked();
}