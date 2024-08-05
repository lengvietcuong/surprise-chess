using SplashKitSDK;

namespace SurpriseChess;

public class GameOverScreen : UIElement
{
    private CenteredText heading;
    private CenteredText subheading;
    private readonly CenteredButton homeButton;
    private readonly Overlay overlay;
    private GameResult result;

    public GameResult Result
    {
        get => result;
        set
        {
            result = value;
            heading = CreateHeading();
            subheading = CreateSubheading();
        }
    }

    public GameOverScreen(GameResult result)
    {
        this.result = result;
        overlay = new(UIConstants.DarkGrayOverlay);
        heading = CreateHeading();
        subheading = CreateSubheading();
        homeButton = new("Home", UIConstants.Gold, Color.Black, 300);
    }

    private CenteredText CreateHeading() => new(
        GetHeadingText(result),
        Color.White,
        UIConstants.BoldFontName,
        UIConstants.LargeFontSize,
        150
    );

    private CenteredText CreateSubheading() => new(
        GetSubheadingText(result),
        Color.White,
        UIConstants.RegularFontName,
        UIConstants.SmallFontSize,
        200
    );

    public override void Draw()
    {
        overlay.Draw();
        heading.Draw();
        subheading.Draw();
        homeButton.Draw();
    }

    public bool IsHomeButtonClicked => homeButton.IsClicked();

    private static string GetHeadingText(GameResult result) => result switch
    {
        GameResult.WhiteWins => "White Wins!",
        GameResult.BlackWins => "Black Wins!",
        GameResult.DrawByStalemate or GameResult.DrawByInsufficientMaterial => "Draw!",
        _ => throw new ArgumentException("Invalid game result")
    };

    private static string GetSubheadingText(GameResult result) => result switch
    {
        GameResult.WhiteWins or GameResult.BlackWins => "By checkmate",
        GameResult.DrawByStalemate => "By stalemate",
        GameResult.DrawByInsufficientMaterial => "By insufficient material",
        _ => throw new ArgumentException("Invalid game result")
    };
}