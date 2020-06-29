using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WelcomeMonoHome
{
  public class WelcomeMonoHome : Game
  {
    public GraphicsDeviceManager graphics;
    GraphicsService _graphicsService;

    SpriteBatch _spriteBatch;

    bool PAUSED = false;
    bool HELD = false;

    // Services
    InputService _inputService;
    EntityManagerService _entityManagerService;
    RendererService _rendererService;
    DebugService _debugService;
    ContentManagerService _contentManagerService;
    SceneManagerService _sceneManagerService;
    CollisionManagerService _collisionManagerService;
    GuiService _guiService;

    // random stuff that needs sorting
    Camera _camera;
    Scene scene;

    public WelcomeMonoHome()
    {
      graphics = new GraphicsDeviceManager(this);
    }

    protected override void Initialize()
    {
      graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
      graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
      graphics.HardwareModeSwitch = false;
      graphics.ApplyChanges();

      // what was in constructor now here

      // initialize graphics
      _graphicsService = new GraphicsService(graphics);
      ServiceLocator.SetService<IGraphicsService>(_graphicsService);

      Content.RootDirectory = "Content";
      IsMouseVisible = true;

      _spriteBatch = new SpriteBatch(GraphicsDevice);

      // random stuff
      _camera = new Camera(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

      // Initialize services
      _inputService = new InputService(_camera);
      _collisionManagerService = new CollisionManagerService();
      _contentManagerService = new ContentManagerService(Content);
      _debugService = new DebugService(Content);
      _entityManagerService = new EntityManagerService();
      _guiService = new GuiService(_inputService);
      _rendererService = new RendererService(_spriteBatch, _camera);
      _sceneManagerService = new SceneManagerService();

      // Set services
      ServiceLocator.SetService<ICollisionManagerService>(_collisionManagerService);
      ServiceLocator.SetService<IContentManagerService>(_contentManagerService);
      ServiceLocator.SetService<IDebugService>(_debugService);
      ServiceLocator.SetService<IEntityManagerService>(_entityManagerService);
      ServiceLocator.SetService<IGuiService>(_guiService);
      ServiceLocator.SetService<IInputService>(_inputService);
      ServiceLocator.SetService<IRendererService>(_rendererService);
      ServiceLocator.SetService<ISceneManagerService>(_sceneManagerService);

      _guiService.NewConsole("help", new Vector2(0, 0), null, null);

      base.Initialize();
    }

    protected override void LoadContent()
    {
      _contentManagerService.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();

      if (!PAUSED)
      {
        if (scene == null)
        {

          scene = new StartScreen();
          //scene = new GameScene();
          _sceneManagerService.SetOrChangeScene(scene);
          scene.LoadContent();
          scene.Initialize();
          return;
        }
        else
        {
          UpdateServices(gameTime);
          // _sceneManagerService.UpdateScene();
          scene.Update(gameTime); // dlet this

          // debug console
          _guiService.ConsoleWriteLine("help", "GameTime: " + gameTime.TotalGameTime.Seconds);
          _guiService.ConsoleWriteLine("help", "Entities: " + _entityManagerService.entities.Count.ToString());
          _guiService.ConsoleWriteLine("help", "Rendered: " + _rendererService._renderableRenderQueue.Count.ToString());
        }
        base.Update(gameTime);
      }
    }

    protected override void Draw(GameTime gameTime)
    {
      //_guiService.ClearConsole("help");
      GraphicsDevice.Clear(Color.CornflowerBlue);

      _rendererService.Run();

      // ! TODO THIS CANT GO HERE
      //  Loop to check entity visibility 
      foreach (Entity entity in ServiceLocator.GetService<IEntityManagerService>().entities)
      {
        // Is it visible?
        if (entity.isVisible != true && _camera.GetRenderedWorld().Contains(entity.pos.X, entity.pos.Y))
        {
          entity.isVisible = true;
        }

        // Is it not visible?
        else if (entity.isVisible != false && !_camera.GetRenderedWorld().Contains(entity.pos.X, entity.pos.Y))
        {
          entity.isVisible = false;
        }
      }

      base.Draw(gameTime);
    }

    void UpdateServices(GameTime gameTime)
    {
      _inputService.Update(gameTime);
      // TODO implement this
      //_sceneManagerService.Update(); 
      _entityManagerService.UpdateEntities(gameTime);
      _collisionManagerService.Update();
    }
  }
}
