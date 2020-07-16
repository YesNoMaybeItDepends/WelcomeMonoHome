using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WelcomeMonoHome.Components;
using System;
using System.Collections.Generic;

public abstract class Entity
{
  public List<Component> components = new List<Component>();
  public abstract void Update(GameTime gameTime);
  // TODO TEXTURE CANT GO HERE, TEXTURE MUST GO ON SPRITE COMPONENT. bruh.
  public Texture2D texture;
  private bool? _isVisible;
  private Vector2 _pos;
  public float scale = 1f;

  // Components
  public Input input;
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
        colRectangle = GetComponent<Sprite>().GetSpriteRectangle();
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

    foreach (Component component in components)
    {
      component.Destroy();
    }

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
    foreach (Component component in components)
    {
      component.Instantiate();
    }
  }

  public ComponentType GetComponent<ComponentType>() where ComponentType : Component
  {
    foreach (Component component in components)
    {
      System.Type type = component.GetType();
      if (type == typeof(ComponentType) || type.IsSubclassOf(typeof(ComponentType)))
      {
        return (ComponentType)component;
      }
    }

    return null;
  }

  public virtual void AddComponent(Component component)
  {
    component.parent = this;
    // TODO on component added callback? 
    components.Add(component);
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