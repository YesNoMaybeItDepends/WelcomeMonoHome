using Microsoft.Xna;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using WelcomeMonoHome.GameObjects;
using Microsoft.Xna.Framework;
using System;
using WelcomeMonoHome.GUI;
using Microsoft.Xna.Framework.Input;

public class GameScene : Scene
{
  // Services
  ICollisionManagerService _collisionService;
  IContentManagerService _ContentService;
  IDebugService _debugService;
  IEntityManagerService _entityService;
  IGuiService _guiService;
  IInputService _inputService;
  IRendererService _rendererService;
  ISceneManagerService _sceneService;
  ISoundService _soundService;

  // TODO turn into service
  Random random;

  // player management
  public BBEG _player;

  // hillarious management
  float _hillariousSpawnRate = 1f;
  float _nextTimeToSpawnHillarious = 1;

  // heathpill management
  float _healthPillSpawnRate = 2f;
  float _nextTimeToSpawnHealthPill = 5;

  // helpers
  int SCREENWIDTH;
  int SCREENHEIGHT;

  // debug
  bool hillariousSpawning = false;
  bool BBEGspawning = false;

  CyclopsTroll player;

  public GameScene()
  {
    GraphicsDeviceManager graphics = ServiceLocator.GetService<IGraphicsService>().graphics;
    SCREENWIDTH = graphics.PreferredBackBufferWidth;
    SCREENHEIGHT = graphics.PreferredBackBufferHeight;

    // Get Services
    _entityService = ServiceLocator.GetService<IEntityManagerService>();
    _rendererService = ServiceLocator.GetService<IRendererService>();
    _debugService = ServiceLocator.GetService<IDebugService>();
    _ContentService = ServiceLocator.GetService<IContentManagerService>();
    _sceneService = ServiceLocator.GetService<ISceneManagerService>();
    _collisionService = ServiceLocator.GetService<ICollisionManagerService>();
    _inputService = ServiceLocator.GetService<IInputService>();
    _guiService = ServiceLocator.GetService<IGuiService>();
    _soundService = ServiceLocator.GetService<ISoundService>();

    random = new Random();
  }

  public override void LoadContent()
  {
    _ContentService.LoadTexture("BBEG_ok_mini");
    _ContentService.LoadTexture("boolet");
    _ContentService.LoadTexture("Hillarious_mini");
    _ContentService.LoadTexture("pixel");
    _ContentService.LoadTexture("HealthPill");
    _ContentService.LoadTexture("cyclops_troll_idle_sheet");
    _ContentService.LoadTexture("cyclops_troll_walk_sheet");
    _ContentService.LoadTexture("cyclops_troll_attack_sheet");
    _ContentService.LoadTexture("cyclops_scimitar_sheet");
    _ContentService.LoadSong("song");
  }

  public override void Initialize()
  {
    if (BBEGspawning)
    {
      _player = new BBEG();
      _player.Initialize();
      _player.Instantiate();
      _soundService.PlaySong("song");
    }

    player = new CyclopsTroll();
    player.Initialize();
    player.Instantiate();

    CyclopsScimitar memer = new CyclopsScimitar();
    memer.Instantiate();
    memer.SetPlayer(player);
  }

  public override void Update(GameTime gameTime)
  {
    // Hillarious spawning
    if (hillariousSpawning && gameTime.TotalGameTime.TotalSeconds > _nextTimeToSpawnHillarious)
    {
      _nextTimeToSpawnHillarious = (float)gameTime.TotalGameTime.TotalSeconds + _hillariousSpawnRate;

      Hillarious hillarious = new Hillarious(_player);
      hillarious.Initialize(random);
      hillarious.Instantiate();
    }

    // HealthPill spawning
    if (gameTime.TotalGameTime.TotalSeconds > _nextTimeToSpawnHealthPill)
    {
      _nextTimeToSpawnHealthPill = (float)gameTime.TotalGameTime.TotalSeconds + _healthPillSpawnRate;

      HealthPill pill = new HealthPill(gameTime);
      float pillx = random.Next(0, SCREENWIDTH + 1);
      float pilly = random.Next(0, SCREENHEIGHT + 1);
      pill.transform.position = new Vector2(pillx, pilly);
      pill.Instantiate();
    }
  }

  // TODO do I need to exist?
  public override void Draw(GameTime gametime)
  {

  }

  public override void UnloadContent()
  {
    _ContentService.UnloadContent();
  }

  public override void Start()
  {

  }

  public override void End()
  {

  }
}