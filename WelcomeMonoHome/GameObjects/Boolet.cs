using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using WelcomeMonoHome.Components;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

public class Boolet : Entity
{
  //public Sprite sprite;
  //public override Vector2 pos;
  //public override Texture2D texture;
  float _rotation;
  float _speed = 200f;
  Vector2 _targetPos;
  Vector2 _direction;

  public Boolet(Vector2 pos, Texture2D texture)
  {
    this.pos = pos;
    this.texture = texture;
    sprite = new Sprite(texture, Vector2.Zero);

    _targetPos = Mouse.GetState().Position.ToVector2();
    _direction = Vector2.Normalize(_targetPos - this.pos);

    colRectangle = new Rectangle((int)(sprite.position.X - sprite._texture.Width / 2), (int)(sprite.position.Y - sprite._texture.Height / 2), sprite._texture.Width, sprite._texture.Height);
    hasCollision = true;
  }

  public override void Update(GameTime gameTime)
  {
    pos += (_direction * _speed) * (float)gameTime.ElapsedGameTime.TotalSeconds;

    // collision
    colRectangle = new Rectangle((int)(sprite.position.X - sprite._texture.Width), (int)(sprite.position.Y - sprite._texture.Height), sprite._texture.Width, sprite._texture.Height);

    foreach (Entity entity in ServiceLocator.GetService<IEntityManagerService>().Entities)
    {
      if (entity != this && entity.hasCollision)
      {
        if (colRectangle.Intersects(entity.colRectangle) && entity is Hillarious)
        {
          Console.WriteLine("COOOLLLISIIIIIIIIIIION");
          OnCollision();
          entity.OnCollision();
        }
      }
    }
  }

  public override void OnBecameInvisible()
  {
    ServiceLocator.GetService<IEntityManagerService>().RemoveEntity(this);
  }

  public override void OnCollision()
  {
    ServiceLocator.GetService<IEntityManagerService>().RemoveEntity(this);
  }
}