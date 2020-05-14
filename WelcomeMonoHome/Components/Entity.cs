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

  public Vector2 pos
  {
    get
    {
      return _pos;
    }
    set
    {
      _pos = value;
      if (sprite != null)
      {
        sprite.position = _pos;
        Console.WriteLine(sprite.position + " = " + _pos);
      }
      else
      {
        Console.WriteLine("NO SPRITE FOUND");
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

  public virtual void OnBecameVisible()
  {
    Console.WriteLine("I just became visible yo!");
  }

  public virtual void OnBecameInvisible()
  {
    Console.WriteLine("I just became invisble yo!");
  }
}