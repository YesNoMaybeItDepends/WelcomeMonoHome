using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using WelcomeMonoHome.Components;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;


namespace WelcomeMonoHome.GameObjects
{
  public class BBEG : Entity
  {
    float _rotation;
    float _speed = 300f;

    //Texture2D _booletTexture;
    Texture2D _booletTexture;
    string _booletTextureName = "boolet";
    string _bbegTextureName = "BBEG_ok_mini";

    IEntityManagerService _entityManagerService;
    IDebugService _debugService;

    // absolute
    Vector2 _leftGunPos;
    Vector2 _rightGunPos;

    // relative to texture origin
    readonly Vector2 _relativeLeftGunPos = new Vector2(8, 94);
    readonly Vector2 _relativeRightGunPos = new Vector2(119, 94);

    float rateOfFire = 0.5f;
    float nextShot = 0;


    public BBEG()
    {
      texture = ServiceLocator.GetService<IResourceManagerService>().GetTexture(_bbegTextureName);
      _booletTexture = ServiceLocator.GetService<IResourceManagerService>().GetTexture(_booletTextureName);
      sprite = new Sprite(texture, Vector2.Zero);

      // Get services
      _entityManagerService = ServiceLocator.GetService<IEntityManagerService>();
      _debugService = ServiceLocator.GetService<IDebugService>();

      _entityManagerService.AddEntity(this);

      // Get absolute gun positions 
      _leftGunPos = new Vector2((pos.X - texture.Width / 2) + _relativeLeftGunPos.X, (pos.Y - texture.Height / 2) + _relativeLeftGunPos.Y);
      _rightGunPos = new Vector2((pos.X - texture.Width / 2) + _relativeRightGunPos.X, (pos.Y - texture.Height / 2) + _relativeRightGunPos.Y);
    }

    public void Initialize(GraphicsDeviceManager graphics)
    {
      // Initialize position at the middle of the screen from the sprite's center
      pos = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
    }

    public override void Update(GameTime gameTime)
    {
      // Update position from WASD
      Vector2 direction = Vector2.Zero;

      if (Keyboard.GetState().IsKeyDown(Keys.W))
      {
        direction.Y -= 1;
      }
      if (Keyboard.GetState().IsKeyDown(Keys.S))
      {
        direction.Y += 1;
      }
      if (Keyboard.GetState().IsKeyDown(Keys.A))
      {
        direction.X -= 1;
      }
      if (Keyboard.GetState().IsKeyDown(Keys.D))
      {
        direction.X += 1;
      }

      if (direction != Vector2.Zero)
      {
        direction = Vector2.Normalize(direction);
        pos += direction * _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
      }

      // Fire
      if (Mouse.GetState().LeftButton == ButtonState.Pressed && nextShot < (float)gameTime.TotalGameTime.TotalSeconds)
      {
        nextShot = (float)gameTime.TotalGameTime.TotalSeconds + rateOfFire;
        _entityManagerService.AddEntity(new Boolet((pos + _leftGunPos), _booletTexture));
        _entityManagerService.AddEntity(new Boolet((pos + _rightGunPos), _booletTexture));
      }
    }
  }
}
