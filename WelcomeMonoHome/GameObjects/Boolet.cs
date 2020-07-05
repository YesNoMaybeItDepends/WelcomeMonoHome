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
  const float PLAYER_BOOLET_SPEED = 400f;
  const float HILLARIOUS_BOOLET_SPEED = 200f;
  Vector2 _targetPos;
  Vector2 _direction;
  public bool isPlayerBoolet;
  float _scale = 2f;

  public Boolet(Vector2 Pos, bool IsPlayerBoolet, Vector2 TargetPos)
  {
    transform = new Transform(this, Vector2.Zero);

    texture = ServiceLocator.GetService<IContentManagerService>().GetTexture("boolet");
    sprite = new Sprite(texture, transform);

    transform.position = Pos;
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
    _direction = Vector2.Normalize(_targetPos - transform.position);

    // enable collision
    hasCollision = true;

    // double sprite scale
    transform.scale = new Vector2(2, 2);
  }

  public override void Update(GameTime gameTime)
  {
    transform.position += (_direction * _speed) * (float)gameTime.ElapsedGameTime.TotalSeconds;
  }

  public override void OnBecameInvisible()
  {
    Destroy();
  }

  public override void OnCollision(Entity collider)
  {
    if (collider is Hillarious && isPlayerBoolet)
    {
      Destroy();
    }
    else if (collider is BBEG && !isPlayerBoolet)
    {
      Destroy();
    }
  }
}