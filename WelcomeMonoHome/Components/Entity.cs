using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WelcomeMonoHome.Components;
using System;
using System.Collections.Generic;

public abstract class Entity
{
  public abstract void Update(GameTime gameTime);
  public Texture2D texture;
  private bool? _isVisible;
  private Vector2 _pos;
  public float scale = 1f;

  // Components
  public Input input;
  public Sprite sprite;
  private Transform _transform;
  public Transform transform
  {
    get
    {
      return _transform;
    }
    set
    {
      _transform = value;

      if (hasCollision)
      {
        // update collision box
        colRectangle = sprite.GetSpriteRectangle();
      }
    }
  }

  private bool _hasCollision = false;
  public bool hasCollision
  {
    get
    {
      return _hasCollision;
    }
    set
    {
      if (value == true)
      {
        _hasCollision = value;
        ServiceLocator.GetService<ICollisionManagerService>().AddCollidable(this);
      }
      else if (value == false)
      {
        _hasCollision = false;
        ServiceLocator.GetService<ICollisionManagerService>().RemoveCollidable(this);
      }
    }
  }

  Rectangle _colRectangle;
  public Rectangle colRectangle
  {
    get
    {
      return _colRectangle;
    }
    set
    {
      _colRectangle = value;
    }
  }

  // TODO rename to isInsideScreen
  public bool? isVisible
  {
    get
    {
      return _isVisible;
    }
    set
    {
      if (_isVisible != false && value == false)
      {
        _isVisible = false;
        OnBecameInvisible();
      }
      else if (_isVisible != true && value == true)
      {
        _isVisible = true;
        OnBecameVisible();
      }
    }
  }

  public List<Entity> alreadyCollidedWith = new List<Entity>();

  public virtual void OnBecameVisible()
  {

  }

  public virtual void OnBecameInvisible()
  {

  }

  public virtual void OnCollision(Entity collider)
  {

  }

  public virtual void Destroy()
  {
    IEntityManagerService _entityManagerService = ServiceLocator.GetService<IEntityManagerService>();

    _entityManagerService.RemoveEntity(this);
    sprite.Destroy();

    // TODO replace this logic with a collidable class
    // such that I can do collidable.destroy();
    if (hasCollision)
    {
      ICollisionManagerService _collisionManager = ServiceLocator.GetService<ICollisionManagerService>();
      _collisionManager.RemoveCollidable(this);
    }

    if (input != null)
    {
      input.Unsubscribe();
    }
  }

  public void Instantiate()
  {
    IEntityManagerService _entityManagerService = ServiceLocator.GetService<IEntityManagerService>();

    _entityManagerService.AddEntity(this);
    sprite.Instantiate();
  }
  /*
    // ! TODO MAYBE WE REALLY DONT NEED TO SET THE POSITION WHEN WE INSTANTIATE
    public void Instantiate(Vector2 position)
    {
      transform = new Transform(this, position);
      IEntityManagerService _entityManagerService = ServiceLocator.GetService<IEntityManagerService>();

      _entityManagerService.AddEntity(this);
      sprite.Instantiate();
    }
    */
}