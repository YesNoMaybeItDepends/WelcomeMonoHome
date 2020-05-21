using Microsoft.Xna;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using WelcomeMonoHome.GameObjects;
using Microsoft.Xna.Framework;
using System;

public class Scene
{
  ContentManager _content;
  SpriteBatch _spriteBatch;
  GraphicsDeviceManager _graphics;

  // Services
  EntityManagerService _entityManagerService;
  RendererService _rendererService;
  DebugService _debugService;
  ResourceManagerService _resourceManagerService;
  SceneManagerService _sceneManagerService;
  CollisionManagerService _collisionManagerService;

  // TODO change with entitymanagerservice
  List<Entity> _entities;
  List<Entity> _entitiesToAdd;
  List<Entity> _entitiesToRemove;

  // textures
  // private Texture2D _BBEG_ok_mini;
  // private Texture2D _boolet;
  // private Texture2D _Hillarious_mini;
  // private Texture2D _pixel;

  // font
  // TODO move somewhere else
  private SpriteFont _font;

  bool isInit = false;

  Random random;

  float _hillariousSpawnRate = 1f;
  float _nextTimeToSpawnHillarious = 1;

  ScreenText _entitiesText;

  public BBEG _player;

  // debug
  ScreenText _debugTimer;
  ScreenText _framerateText;
  ScreenText _playerHPText;

  public Scene(ContentManager content, SpriteBatch spritebatch, GraphicsDeviceManager graphics)
  {
    _content = content;
    _spriteBatch = spritebatch;
    _graphics = graphics;

    // TODO change with entitymanagerservice
    _entities = new List<Entity>();
    _entitiesToAdd = new List<Entity>();
    _entitiesToRemove = new List<Entity>();

    // Initialize Services
    _entityManagerService = new EntityManagerService(_entities, _entitiesToAdd, _entitiesToRemove);
    _rendererService = new RendererService(_spriteBatch);
    _debugService = new DebugService(_content);
    _resourceManagerService = new ResourceManagerService(_content);
    _sceneManagerService = new SceneManagerService(this);
    _collisionManagerService = new CollisionManagerService();

    // Map Services
    ServiceLocator.SetService<IEntityManagerService>(_entityManagerService);
    ServiceLocator.SetService<IrendererService>(_rendererService);
    ServiceLocator.SetService<IDebugService>(_debugService);
    ServiceLocator.SetService<IResourceManagerService>(_resourceManagerService);
    ServiceLocator.SetService<ISceneManagerService>(_sceneManagerService);
    ServiceLocator.SetService<ICollisionManagerService>(_collisionManagerService);

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

    _resourceManagerService.Initialize();
  }

  public void Update(GameTime gametime)
  {

    // initialize loop 
    // TODO this is fucked up, fix
    if (!isInit)
    {
      _player = new BBEG();
      _player.Initialize(_graphics);

      _debugTimer = new ScreenText($"Gametime: {gametime.TotalGameTime.Seconds}", Vector2.Zero);
      _entitiesText = new ScreenText("Entities: ", Vector2.Zero);
      _framerateText = new ScreenText("FPS: ", Vector2.Zero);
      _playerHPText = new ScreenText("HP: ", Vector2.Zero);
      _debugService.AddToTextList(_debugTimer);
      _debugService.AddToTextList(_entitiesText);
      _debugService.AddToTextList(_framerateText);
      _debugService.AddToTextList(_playerHPText);

      isInit = true;
    }


    // hillarious spawn timer
    if (_nextTimeToSpawnHillarious <= gametime.TotalGameTime.TotalSeconds)
    {
      Hillarious memress = new Hillarious(_player);
      memress.Initialize(_graphics, random);
      _entities.Add(memress);

      _nextTimeToSpawnHillarious = (float)gametime.TotalGameTime.TotalSeconds + _hillariousSpawnRate;
    }

    // Update Entities
    _entityManagerService.UpdateEntities(gametime);

    // Update collision queue
    _collisionManagerService.Update();

    // debug
    _entitiesText.UpdateText($"Entities: {_entityManagerService.Entities.Count.ToString()}");
    _debugTimer.UpdateText($"Gametime: {gametime.TotalGameTime.Seconds}");
    _framerateText.UpdateText($"FPS: {(int)(1 / gametime.ElapsedGameTime.TotalSeconds)}");
    _playerHPText.UpdateText($"HP: {_player.currentHP}");
  }

  // ? pass spritebatch or use _spritebatch?
  public void Draw()
  {
    _rendererService.Run();
    // _spriteBatch.Begin();

    // // Loop to check entity visibility 
    foreach (Entity entity in _entities)
    {

      // Is it visible?
      if ((entity.isVisible != true) &&
          (entity.pos.X < _graphics.PreferredBackBufferWidth + entity.texture.Width &&
          entity.pos.X > -1 * entity.texture.Width) &&
          (entity.pos.Y < _graphics.PreferredBackBufferHeight + entity.texture.Height &&
          entity.pos.Y > -1 * entity.texture.Height))
      {
        entity.isVisible = true;
      }

      // Is it not visible?
      if ((entity.isVisible != false) &&
          (entity.pos.X > _graphics.PreferredBackBufferWidth + entity.texture.Width ||
          entity.pos.X < -1 * entity.texture.Width ||
          entity.pos.Y > _graphics.PreferredBackBufferHeight + entity.texture.Height ||
          entity.pos.Y < -1 * entity.texture.Height))
      {
        entity.isVisible = false;
      }
    }

    //   entity.Draw(_spriteBatch);
    // }

    // _spriteBatch.End();
  }

  public void UnloadContent()
  {
    _content.Unload();
  }
}