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
    // public Sprite sprite;
    // public Vector2 pos;
    // public Texture2D texture;
    float _rotation;
    float _speed = 300;

    Texture2D _booletTexture;

    IEntityManagerService _entityManagerService;

    // absolute
    Vector2 _leftGunPos;
    Vector2 _rightGunPos;

    // relative to texture origin
    readonly Vector2 _relativeLeftGunPos = new Vector2(8, 94);
    readonly Vector2 _relativeRightGunPos = new Vector2(119, 94);

    bool _clickDown = false;


    public BBEG(Texture2D BBEG_texture, Texture2D Boolet_texture)
    {
      texture = BBEG_texture;
      _booletTexture = Boolet_texture;
      sprite = new Sprite(texture);

      // Get services
      _entityManagerService = ServiceLocator.GetService<IEntityManagerService>();

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
      if (Keyboard.GetState().IsKeyDown(Keys.W))
      {
        pos.Y -= _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
      }
      if (Keyboard.GetState().IsKeyDown(Keys.S))
      {
        pos.Y += _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
      }
      if (Keyboard.GetState().IsKeyDown(Keys.A))
      {
        pos.X -= _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
      }
      if (Keyboard.GetState().IsKeyDown(Keys.D))
      {
        pos.X += _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
      }

      pos = Vector2.Normalize(pos);

      // Fire
      if (Mouse.GetState().LeftButton == ButtonState.Pressed)
      {
        _clickDown = true;
      }
      if (_clickDown && Mouse.GetState().LeftButton == ButtonState.Released)
      {
        _clickDown = false;
        _entityManagerService.AddEntity(new Boolet((pos + _leftGunPos), _booletTexture));
        _entityManagerService.AddEntity(new Boolet((pos + _rightGunPos), _booletTexture));
      }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      sprite.Draw(spriteBatch, pos);
    }
  }
}
