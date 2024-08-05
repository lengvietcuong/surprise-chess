using SplashKitSDK;

namespace SurpriseChess;

public class ChessView
{
    private Window? gameWindow;
    private HomeScreen? homeScreen;
    private GameScreen? gameScreen;
    private GameOverScreen? gameOverScreen;
    private readonly AssetManager assets;

    public ChessView()
    {
        assets = new();
    }

    public void OpenGameWindow()
    {
        gameWindow = SplashKit.OpenWindow(
            UIConstants.WindowTitle, UIConstants.WindowWidth, UIConstants.WindowHeight
        );
    }

    public void CloseGameWindow()
    {
        gameWindow?.Close();
    }

    public bool IsGameWindowOpen => gameWindow != null && !SplashKit.WindowCloseRequested(gameWindow);

    public void DrawHomeScreen()
    {
        homeScreen ??= new(assets);
        homeScreen.Draw();
    }

    public void DrawGameScreen(
        IBoardView board,
        Position? selectedPosition,
        IEnumerable<Position> highlightedMoves
    )
    {
        if (gameScreen == null)
        {
            // Create new game screen
            gameScreen = new(board, selectedPosition, highlightedMoves, assets);
        }
        else
        {
            // Update game screen state
            gameScreen.Board = board;
            gameScreen.SelectedPosition = selectedPosition;
            gameScreen.HighlightedMoves = highlightedMoves;
        }
        gameScreen.Draw();
    }

    public void DrawGameOverScreen(GameResult result)
    {
        if (gameOverScreen == null)
        {
            gameOverScreen = new(result);
        }
        else
        {
            gameOverScreen.Result = result;
        }
        gameOverScreen.Draw();
    }

    public bool IsPlayWithBotButtonClicked => (
        homeScreen != null
        && homeScreen.IsPlayWithBotButtonClicked
    );

    public bool IsPlayWithPlayerButtonClicked => (
        homeScreen != null
        && homeScreen.IsPlayWithPlayerButtonClicked
    );

    public bool IsHomeButtonClicked => (
        gameOverScreen != null
        && gameOverScreen.IsHomeButtonClicked
    );
}