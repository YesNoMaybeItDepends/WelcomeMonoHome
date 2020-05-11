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

  // TODO change with entitymanagerservice
  List<Entity> _entities;
  List<Entity> _entitiesToAdd;
  List<Entity> _entitiesToRemove;

  // textures
  private Texture2D _BBEG_ok_mini;
  private Texture2D _boolet;
  private Texture2D _Hillarious_mini;

  bool isInit = false;

  Random random;

  public Scene(ContentManager content, SpriteBatch spritebatch, GraphicsDeviceManager graphics)
  {
    _content = content;
    _spriteBatch = spritebatch;
    _graphics = graphics;

    // TODO change with entitymanagerservice
    _entities = new List<Entity>();
    _entitiesToAdd = new List<Entity>();
    _entitiesToRemove = new List<Entity>();

    _entityManagerService = new EntityManagerService(_entities, _entitiesToAdd, _entitiesToRemove);

    ServiceLocator.SetService<IEntityManagerService>(_entityManagerService);

    if (ServiceLocator.GetService<IEntityManagerService>() != null)
    {
      Console.WriteLine("SUCCESS");
    };

    random = new Random();
  }

  public void Initialize()
  {

  }

  public void LoadContent()
  {
    _BBEG_ok_mini = _content.Load<Texture2D>("BBEG_ok_mini");
    _boolet = _content.Load<Texture2D>("boolet");
    _Hillarious_mini = _content.Load<Texture2D>("Hillarious_mini");
  }

  public void Update(GameTime gametime)
  {
    if (!isInit)
    {
      BBEG memer = new BBEG(_BBEG_ok_mini, _boolet);
      memer.Initialize(_graphics);
      _entities.Add(memer);

      Hillarious memress = new Hillarious(_Hillarious_mini);

      memress.Initialize(_graphics, random);
      _entities.Add(memress);

      isInit = true;
    }
    _entityManagerService.UpdateEntities(gametime);
  }

  // ? pass spritebatch or use _spritebatch?
  public void Draw()
  {
    _spriteBatch.Begin();

    // Loop to check entity visibility 
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


      entity.Draw(_spriteBatch);
    }

    _spriteBatch.End();
  }

  public void UnloadContent()
  {
    _content.Unload();
  }
}