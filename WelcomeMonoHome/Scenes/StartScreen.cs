using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

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

  Image background;
  Button NewGame;

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
    background.Destroy();
    NewGame.Destroy();
  }

  public override void Initialize()
  {
    background = new Image("startScreen_1080", new Vector2(0, 0));
    background.Instantiate();
    Vector2 buttonPos;
    buttonPos.X = _graphicsService.GetScreenWidth() / 2;
    buttonPos.Y = _graphicsService.GetScreenHeight() / 2 - 200;
    NewGame = new Button(buttonPos, 200, 100, Color.White, Color.Black);
    NewGame.text = "New Game";
    NewGame.dothingie = OnStartGameClick;
    NewGame.Instantiate();

    // meme
    Texture2D memetexture = _contentService.GetTexture("SmileyWalk");
    //meme = new AnimatedSprite(memetexture, 4, 4);
    //ServiceLocator.GetService<IRendererService>().AddRenderable(meme);

    // another meme
    Texture2D anothermeme = _contentService.GetTexture("player_machinegun");
  }

  public override void LoadContent()
  {
    _contentService.LoadTexture("startScreen");
    _contentService.LoadTexture("startScreen_1080");
    _contentService.LoadTexture("SmileyWalk");
    _contentService.LoadTexture("player_machinegun");
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
    NewGame.Update();
  }

  public void OnStartGameClick()
  {
    ISceneManagerService _sceneManagerService = ServiceLocator.GetService<ISceneManagerService>();
    Scene scene = new GameScene();
    _sceneManagerService.SetOrChangeScene(scene);
    scene.LoadContent();
    scene.Initialize();
  }
}