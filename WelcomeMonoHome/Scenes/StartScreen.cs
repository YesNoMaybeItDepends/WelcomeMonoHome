using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class StartScreen : Scene
{

  // Services
  ICollisionManagerService _collisionService;
  IContentManagerService _contentService;
  IDebugService _debugService;
  IEntityManagerService _entityService;
  IGuiService _guiService;
  IInputService _inputService;
  IRendererService _rendererService;
  ISceneManagerService _sceneService;
  IGraphicsService _graphicsService;

  public StartScreen()
  {
    // Get services
    _collisionService = ServiceLocator.GetService<ICollisionManagerService>();
    _contentService = ServiceLocator.GetService<IContentManagerService>();
    _debugService = ServiceLocator.GetService<IDebugService>();
    _entityService = ServiceLocator.GetService<IEntityManagerService>();
    _guiService = ServiceLocator.GetService<IGuiService>();
    _inputService = ServiceLocator.GetService<IInputService>();
    _rendererService = ServiceLocator.GetService<IRendererService>();
    _sceneService = ServiceLocator.GetService<ISceneManagerService>();
    _graphicsService = ServiceLocator.GetService<IGraphicsService>();
  }

  public override void Draw(GameTime gameTime)
  {
    // nothing happens here, should delete
  }

  public override void End()
  {
    // np
  }

  public override void Initialize()
  {
    // np
    Image background = new Image("startScreen_1080", new Vector2(0, 0));
    background.Instantiate();
    Vector2 buttonPos;
    buttonPos.X = _graphicsService.GetScreenWidth() / 2;
    buttonPos.Y = _graphicsService.GetScreenHeight() / 2 - 200;
    Button NewGame = new Button(buttonPos, 200, 100, Color.White, Color.Black);
    NewGame.text = "New Game";
    NewGame.Instantiate();
  }

  public override void LoadContent()
  {
    _contentService.LoadTexture("startScreen");
    _contentService.LoadTexture("startScreen_1080");
  }

  public override void Start()
  {
    // np 
  }

  public override void UnloadContent()
  {
    // np
  }

  public override void Update(GameTime gameTime)
  {
    // np
  }
}