using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using WelcomeMonoHome.Components;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using WelcomeMonoHome.GameObjects;

public class Boolet : Entity
{
  float _rotation;
  float _speed = 200f;
  Vector2 _targetPos;
  Vector2 _direction;
  bool _isPlayerBoolet;

  public Boolet(Vector2 Pos, Texture2D Texture, bool isPlayerBoolet)
  {
    pos = Pos;
    texture = Texture;
    _isPlayerBoolet = isPlayerBoolet;

    sprite = new Sprite(texture, Vector2.Zero);

    // set targetPos
    if (_isPlayerBoolet)
    {
      _targetPos = Mouse.GetState().Position.ToVector2();
    }
    else
    {
      Scene scene = ServiceLocator.GetService<ISceneManagerService>().GetScene();
      _targetPos = scene.player.pos;
    }

    // set direction 
    _direction = Vector2.Normalize(_targetPos - this.pos);

    // set collision box
    hasCollision = true;
    colRectangle = new Rectangle((int)(sprite.position.X - sprite._texture.Width / 2), (int)(sprite.position.Y - sprite._texture.Height / 2), sprite._texture.Width, sprite._texture.Height);
  }

  public override void Update(GameTime gameTime)
  {
    pos += (_direction * _speed) * (float)gameTime.ElapsedGameTime.TotalSeconds;

    // update collision box
    colRectangle = new Rectangle((int)(sprite.position.X - sprite._texture.Width), (int)(sprite.position.Y - sprite._texture.Height), sprite._texture.Width, sprite._texture.Height);

    // check collision
    foreach (Entity entity in ServiceLocator.GetService<IEntityManagerService>().Entities)
    {
      if (entity != this && entity.hasCollision)
      {
        // resolve collision
        if (colRectangle.Intersects(entity.colRectangle))
        {
          OnCollision(entity);
        }
      }
    }
  }

  public override void OnBecameInvisible()
  {
    ServiceLocator.GetService<IEntityManagerService>().RemoveEntity(this);
  }

  public override void OnCollision(Entity collider)
  {
    if (collider is Hillarious && _isPlayerBoolet)
    {
      collider.OnCollision(this);
      ServiceLocator.GetService<IEntityManagerService>().RemoveEntity(this);
    }
    else if (collider is BBEG && !_isPlayerBoolet)
    {
      collider.OnCollision(this);
      ServiceLocator.GetService<IEntityManagerService>().RemoveEntity(this);
    }
  }
}