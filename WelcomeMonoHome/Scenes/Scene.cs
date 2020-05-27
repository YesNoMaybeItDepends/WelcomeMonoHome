using Microsoft.Xna;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using WelcomeMonoHome.GameObjects;
using Microsoft.Xna.Framework;
using System;
using WelcomeMonoHome.GUI;
using Microsoft.Xna.Framework.Input;

public class Scene
{
  ContentManager _content;
  SpriteBatch _spriteBatch;
  GraphicsDeviceManager _graphics;

  public Camera _camera;

  // Services
  EntityManagerService _entityManagerService;
  RendererService _rendererService;
  DebugService _debugService;
  ContentManagerService _ContentManagerService;
  SceneManagerService _sceneManagerService;
  CollisionManagerService _collisionManagerService;
  InputService _inputService;
  GuiService _guiService;

  // TODO change with entitymanagerservice
  List<Entity> _entities;
  List<Entity> _entitiesToAdd;
  List<Entity> _entitiesToRemove;

  bool isInit = false;

  Random random;

  float _hillariousSpawnRate = 1f;
  float _nextTimeToSpawnHillarious = 1;

  // ? Why did I make a list of healthpills?
  // * I think because healthpills wont get updated
  // TODO for now lul
  List<HealthPill> _healthPills;
  float _healthPillSpawnRate = 2f;
  float _nextTimeToSpawnHealthPill = 5;

  TextBox _entitiesText;

  public BBEG _player;

  // debug
  public GuiConsole console;
  public GuiConsole FPSconsole;

  RectanglePrimitive camerabounds;


  int SCREENWIDTH;
  int SCREENHEIGHT;

  public Scene(ContentManager content, SpriteBatch spritebatch, GraphicsDeviceManager graphics)
  {
    _content = content;
    _spriteBatch = spritebatch;
    _graphics = graphics;
    _camera = new Camera(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

    SCREENWIDTH = _graphics.PreferredBackBufferWidth;
    SCREENHEIGHT = _graphics.PreferredBackBufferHeight;

    // TODO change with entitymanagerservice
    _entities = new List<Entity>();
    _entitiesToAdd = new List<Entity>();
    _entitiesToRemove = new List<Entity>();

    _healthPills = new List<HealthPill>();

    // Initialize Services
    _entityManagerService = new EntityManagerService(_entities, _entitiesToAdd, _entitiesToRemove);
    _rendererService = new RendererService(_spriteBatch, _camera);
    _debugService = new DebugService(_content);
    _ContentManagerService = new ContentManagerService(_content);
    _sceneManagerService = new SceneManagerService(this);
    _collisionManagerService = new CollisionManagerService();
    _inputService = new InputService(_camera);
    _guiService = new GuiService();

    // Map Services
    ServiceLocator.SetService<IEntityManagerService>(_entityManagerService);
    ServiceLocator.SetService<IrendererService>(_rendererService);
    ServiceLocator.SetService<IDebugService>(_debugService);
    ServiceLocator.SetService<IContentManagerService>(_ContentManagerService);
    ServiceLocator.SetService<ISceneManagerService>(_sceneManagerService);
    ServiceLocator.SetService<ICollisionManagerService>(_collisionManagerService);
    ServiceLocator.SetService<IInputService>(_inputService);
    ServiceLocator.SetService<IGuiService>(_guiService);

    random = new Random();
  }

  public void Initialize()
  {

  }

  public void LoadContent()
  {
    // _BBEG_ok_mini = _content.Load<Texture2D>("BBEG_ok_mini");
    // _boolet = _content.Load<Texture2D>("boolet");
    // _Hillarious_mini = _content.Load<Texture2D>("Hillarious_mini");
    // _font = _content.Load<SpriteFont>("MyFont");
    // _pixel = _content.Load<Texture2D>("pixel");

    _ContentManagerService.Initialize();
  }

  public void Update(GameTime gameTime)
  {
    // initialize loop 
    // TODO this is fucked up, fix
    if (!isInit)
    {
      // spawn player
      _player = new BBEG();
      _player.Initialize();
      _player.Instantiate();

      // make text box
      console = new GuiConsole(Vector2.Zero, null, null);
      FPSconsole = new GuiConsole(new Vector2(0, SCREENHEIGHT / 2), null, null);

      camerabounds = new RectanglePrimitive(_camera.GetCameraFrame(), Color.Red, 0.5f);
      _debugService.DrawRectangle(camerabounds);

      isInit = true;
    }

    // Clear the console at the start of the update frame
    console.Clear();

    // camera controls
    Vector2 camerapos = Vector2.Zero;
    if (Keyboard.GetState().IsKeyDown(Keys.Down))
    {
      _camera.Zoom -= (float)(1 * gameTime.ElapsedGameTime.TotalSeconds);
    }
    if (Keyboard.GetState().IsKeyDown(Keys.Up))
    {
      _camera.Zoom += (float)(1 * gameTime.ElapsedGameTime.TotalSeconds);
    }
    if (Keyboard.GetState().IsKeyDown(Keys.NumPad8))
    {
      camerapos.Y--;
    }
    if (Keyboard.GetState().IsKeyDown(Keys.NumPad2))
    {
      camerapos.Y++;
    }
    if (Keyboard.GetState().IsKeyDown(Keys.NumPad6))
    {
      camerapos.X++;
    }
    if (Keyboard.GetState().IsKeyDown(Keys.NumPad4))
    {
      camerapos.X--;
    }
    if (camerapos != Vector2.Zero)
    {
      camerapos = Vector2.Normalize(camerapos);
    }
    _camera._pos += camerapos * 200 * (float)gameTime.ElapsedGameTime.TotalSeconds;

    // can spawn hillarious?
    if (_nextTimeToSpawnHillarious <= gameTime.TotalGameTime.TotalSeconds)
    {
      // spawn hillarious
      Hillarious hillarious = new Hillarious(_player);
      hillarious.Initialize(_graphics, random);
      hillarious.Instantiate();

      _nextTimeToSpawnHillarious = (float)gameTime.TotalGameTime.TotalSeconds + _hillariousSpawnRate;
    }

    // can spawn healthPill?
    if (_nextTimeToSpawnHealthPill <= gameTime.TotalGameTime.TotalSeconds)
    {
      // spawn healthPill
      float pillx = random.Next(0, SCREENWIDTH + 1);
      float pilly = random.Next(0, SCREENHEIGHT + 1);
      HealthPill pill = new HealthPill(gameTime);
      pill.pos = new Vector2(pillx, pilly);
      _healthPills.Add(pill);
      pill.Instantiate();

      _nextTimeToSpawnHealthPill = (float)gameTime.TotalGameTime.TotalSeconds + _healthPillSpawnRate;
    }

    // Update Entities
    _entityManagerService.UpdateEntities(gameTime);

    // Update collision queue
    _collisionManagerService.Update();

    // debug
    console.WriteLine($"Entities: {_entityManagerService.Entities.Count.ToString()}");
    console.WriteLine($"Rendered: {_rendererService._renderableRenderQueue.Count.ToString()}");
    console.WriteLine($"Gametime: {gameTime.TotalGameTime.Seconds}");
    console.WriteLine($"HP: {_player.currentHP}");
  }

  public void Draw(GameTime gametime)
  {
    FPSconsole.WriteLine(("FPS: " + (int)(1 / gametime.ElapsedGameTime.TotalSeconds)).ToString());

    // Run Renderer
    _rendererService.Run();

    //  Loop to check entity visibility 
    foreach (Entity entity in ServiceLocator.GetService<IEntityManagerService>().Entities)
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
  }

  public void UnloadContent()
  {
    _content.Unload();
  }
}