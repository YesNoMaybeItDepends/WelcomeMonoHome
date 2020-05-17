using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using WelcomeMonoHome.Components;
using WelcomeMonoHome.GameObjects;

public enum Side
{
  LEFT,
  TOP,
  RIGHT,
  BOTTOM
}

public class Hillarious : Entity
{
  Vector2 TargetPos;
  Vector2 Direction;
  float _speed = 250f;

  string hillariousTextureName = "Hillarious_mini";

  float rateOfFire = 0.5f;
  float nextShot = 0;

  BBEG _player;

  public Hillarious(BBEG player)
  {
    texture = ServiceLocator.GetService<IResourceManagerService>().GetTexture(hillariousTextureName);
    sprite = new Sprite(texture, Vector2.Zero);

    _player = player;

    // enable collision
    hasCollision = true;
  }

  public void Initialize(GraphicsDeviceManager graphics, /*Side Side, */Random random)
  {
    float SCREEN_WIDTH = graphics.PreferredBackBufferWidth;
    float SCREEN_HEIGHT = graphics.PreferredBackBufferHeight;

    float yspawn;
    float xspawn;
    Side side;

    // Determine spawn position
    bool SpawnOnHorizontalSide = random.Next(1, 3) % 2 == 0 ? true : false;

    if (SpawnOnHorizontalSide)
    {
      yspawn = random.Next(0, (int)SCREEN_HEIGHT + 1);
      xspawn = (float)FindNearestNumber(random.Next(0, (int)SCREEN_WIDTH + 1), 0, (int)SCREEN_WIDTH);
      side = (xspawn == 0) ? Side.LEFT : Side.RIGHT;
    }
    else
    {
      xspawn = random.Next(0, (int)SCREEN_WIDTH + 1);
      yspawn = FindNearestNumber(random.Next(0, (int)SCREEN_HEIGHT + 1), 0, (int)SCREEN_HEIGHT);
      side = (yspawn == 0) ? Side.TOP : Side.BOTTOM;
    }

    // set spawn position
    pos = new Vector2(xspawn, yspawn);

    // Determine TargetPos as random point on edge opposite from spawn edge 
    switch (side)
    {
      case Side.LEFT:
        TargetPos = new Vector2(SCREEN_WIDTH, random.Next(0, (int)SCREEN_HEIGHT + 1));
        break;
      case Side.TOP:
        TargetPos = new Vector2(random.Next(0, (int)SCREEN_WIDTH + 1), SCREEN_HEIGHT);
        break;
      case Side.RIGHT:
        TargetPos = new Vector2(0, random.Next(0, (int)SCREEN_HEIGHT + 1));
        break;
      case Side.BOTTOM:
        TargetPos = new Vector2(random.Next(0, (int)SCREEN_WIDTH + 1), 0);
        break;
      default:
        break;
    }

    // Set direction vector 
    Direction = Vector2.Normalize(TargetPos - pos);
  }

  public override void Update(GameTime gameTime)
  {
    pos += Direction * _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

    if (nextShot < (float)gameTime.TotalGameTime.TotalSeconds)
    {
      nextShot = (float)gameTime.TotalGameTime.TotalSeconds + rateOfFire;
      ServiceLocator.GetService<IEntityManagerService>().AddEntity(new Boolet(pos, false, _player.pos));
    }
  }

  int FindNearestNumber(int number, int min, int max)
  {
    if (number > max / 2)
    {
      return max;
    }
    else
    {
      return min;
    }
  }

  public override void OnBecameInvisible()
  {
    ServiceLocator.GetService<IEntityManagerService>().RemoveEntity(this);
  }

  public override void OnCollision(Entity collider)
  {
    if (collider is Boolet boolet && boolet.isPlayerBoolet)
    {
      ServiceLocator.GetService<IEntityManagerService>().RemoveEntity(this);
    }
  }
}