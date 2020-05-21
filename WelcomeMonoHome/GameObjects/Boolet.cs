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
  float _speed;
  // TODO move these to bbeg/hillarious
  const float PLAYER_BOOLET_SPEED = 300f;
  const float HILLARIOUS_BOOLET_SPEED = 200f;
  Vector2 _targetPos;
  Vector2 _direction;
  public bool isPlayerBoolet;
  float _scale = 2f;

  public Boolet(Vector2 Pos, bool IsPlayerBoolet, Vector2 TargetPos)
  {
    texture = ServiceLocator.GetService<IResourceManagerService>().GetTexture("boolet");
    sprite = new Sprite(texture, Vector2.Zero);

    pos = Pos;
    _targetPos = TargetPos;

    isPlayerBoolet = IsPlayerBoolet;
    if (isPlayerBoolet)
    {
      _speed = PLAYER_BOOLET_SPEED;
      sprite.color = Color.Lime;
    }
    else
    {
      _speed = HILLARIOUS_BOOLET_SPEED;
    }

    // set direction 
    _direction = Vector2.Normalize(_targetPos - this.pos);

    // enable collision
    hasCollision = true;

    // double sprite scale
    sprite.scale = new Vector2(2, 2);
  }

  public override void Update(GameTime gameTime)
  {
    pos += (_direction * _speed) * (float)gameTime.ElapsedGameTime.TotalSeconds;
  }

  public override void OnBecameInvisible()
  {
    ServiceLocator.GetService<IEntityManagerService>().RemoveEntity(this);
  }

  public override void OnCollision(Entity collider)
  {
    if (collider is Hillarious && isPlayerBoolet)
    {
      ServiceLocator.GetService<IEntityManagerService>().RemoveEntity(this);
    }
    else if (collider is BBEG && !isPlayerBoolet)
    {
      ServiceLocator.GetService<IEntityManagerService>().RemoveEntity(this);
    }
  }
}