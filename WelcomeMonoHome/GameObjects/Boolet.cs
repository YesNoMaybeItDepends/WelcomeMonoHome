using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using WelcomeMonoHome.Components;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

public class Boolet : Entity
{
  //public Sprite sprite;
  //public override Vector2 pos;
  //public override Texture2D texture;
  float _rotation;
  float _speed = 100f;
  Vector2 _targetPos;
  Vector2 _direction;

  public Boolet(Vector2 pos, Texture2D texture)
  {
    this.pos = pos;
    this.texture = texture;
    sprite = new Sprite(texture, Vector2.Zero);

    _targetPos = Mouse.GetState().Position.ToVector2();
    _direction = Vector2.Normalize(_targetPos - this.pos);
  }

  public override void Update(GameTime gameTime)
  {
    pos += (_direction * _speed) * (float)gameTime.ElapsedGameTime.TotalSeconds;
  }

  public override void OnBecameInvisible()
  {
    ServiceLocator.GetService<IEntityManagerService>().EntitiesToRemove.Add(this);
  }
}