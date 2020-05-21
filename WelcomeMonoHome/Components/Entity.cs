using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WelcomeMonoHome.Components;
using System;
using System.Collections.Generic;

public abstract class Entity
{
  public abstract void Update(GameTime gameTime);
  public Sprite sprite;
  public Texture2D texture;
  private bool _isVisible;
  private Vector2 _pos;

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

  public Vector2 pos
  {
    get
    {
      return _pos;
    }
    set
    {
      _pos = value;

      // update sprite position
      if (sprite != null)
      {
        sprite.position = _pos;
      }
      else
      {
        Console.WriteLine("@Entity -> WARNING: SPRITE IS NULL");
      }

      if (hasCollision)
      {
        // update collision box
        colRectangle = new Rectangle((int)(sprite.position.X - sprite._texture.Width / 2), (int)(sprite.position.Y - sprite._texture.Height / 2), sprite._texture.Width, sprite._texture.Height);

        // check for collisions
        ServiceLocator.GetService<ICollisionManagerService>().CheckCollision(this);
      }
    }
  }

  public bool isVisible
  {
    get
    {
      return _isVisible;
    }
    set
    {
      if (_isVisible == true && value == false)
      {
        _isVisible = false;
        OnBecameInvisible();
      }
      else if (_isVisible == false && value == true)
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

  }

  // TODO adds the whole thing to entitymanager and what have you not
  public void Instantiate()
  {

  }
}