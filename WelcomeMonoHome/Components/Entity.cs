using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WelcomeMonoHome.Components;
using System;
using System.Collections.Generic;

public abstract class Entity
{
  public abstract void Update(GameTime gameTime);
  public abstract void Draw(SpriteBatch spriteBatch);
  public Vector2 pos;
  public Sprite sprite;
  public Texture2D texture;

  private List<Entity> _entitiesToAdd;
  private List<Entity> _entitiesToRemove;

  public virtual void OnBecameInvisible()
  {
    Console.WriteLine("I just became invisble yo!");
  }
}