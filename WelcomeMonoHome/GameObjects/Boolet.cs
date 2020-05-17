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
  public bool isPlayerBoolet;

  // public Boolet(Vector2 Pos, Texture2D Texture, bool IsPlayerBoolet)
  // {
  //   texture = Texture;
  //   sprite = new Sprite(texture, Vector2.Zero);

  //   pos = Pos;
  //   isPlayerBoolet = IsPlayerBoolet;


  //   // set targetPos
  //   if (this.isPlayerBoolet)
  //   {
  //     _targetPos = Mouse.GetState().Position.ToVector2();
  //   }
  //   else
  //   {
  //     Scene scene = ServiceLocator.GetService<ISceneManagerService>().GetScene();
  //     _targetPos = scene.player.pos;
  //   }

  //   // set direction 
  //   _direction = Vector2.Normalize(_targetPos - this.pos);

  //   // enable collision
  //   hasCollision = true;
  // }

  public Boolet(Vector2 Pos, bool IsPlayerBoolet, Vector2 TargetPos)
  {
    texture = ServiceLocator.GetService<IResourceManagerService>().GetTexture("boolet");
    sprite = new Sprite(texture, Vector2.Zero);

    pos = Pos;
    _targetPos = TargetPos;

    isPlayerBoolet = IsPlayerBoolet;
    if (isPlayerBoolet)
    {
      sprite.color = Color.Lime;
    }

    // set direction 
    _direction = Vector2.Normalize(_targetPos - this.pos);

    // enable collision
    hasCollision = true;
  }

  // // TODO finish this and add player reference to hillarious so i dont need to locate the whole fucking scene
  // public Boolet(Vector2 Pos, Texture2D Texture, BBEG player)
  // {
  //   texture = Texture;
  //   sprite = new Sprite(texture, Vector2.Zero);

  //   pos = Pos;

  //   isPlayerBoolet = false;


  //   // set targetPos
  //   if (isPlayerBoolet)
  //   {
  //     _targetPos = Mouse.GetState().Position.ToVector2();
  //   }
  //   else
  //   {
  //     Scene scene = ServiceLocator.GetService<ISceneManagerService>().GetScene();
  //     _targetPos = scene.player.pos;
  //   }

  // set direction 
  // _direction = Vector2.Normalize(_targetPos - this.pos);

  //   // enable collision
  //   hasCollision = true;
  // }

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