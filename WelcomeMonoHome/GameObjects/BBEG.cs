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
    Sprite _sprite;
    Vector2 _pos;
    Texture2D _texture;
    float _rotation;
    float _speed = 200;
    Vector2 _leftGunPos = new Vector2(8, 94);
    Vector2 _rightGunPos = new Vector2(119, 94);
    List<Entity> _levelEntities;

    Texture2D _booletTexture;

    bool _clickDown = false;

    public BBEG(GraphicsDeviceManager graphics, Texture2D BBEG_texture, Texture2D Boolet_texture, List<Entity> levelEntities)
    {
      _texture = BBEG_texture;
      _booletTexture = Boolet_texture;
      _sprite = new Sprite(_texture);
      _levelEntities = levelEntities;

      // Initialize position at the middle of the screen from the sprite's center
      _pos = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);

    }

    public override void Update(GameTime gameTime)
    {
      // Update position from WASD
      if (Keyboard.GetState().IsKeyDown(Keys.W))
      {
        _pos.Y -= _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
      }
      if (Keyboard.GetState().IsKeyDown(Keys.S))
      {
        _pos.Y += _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
      }
      if (Keyboard.GetState().IsKeyDown(Keys.A))
      {
        _pos.X -= _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
      }
      if (Keyboard.GetState().IsKeyDown(Keys.D))
      {
        _pos.X += _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
      }

      // Fire
      if (Mouse.GetState().LeftButton == ButtonState.Pressed)
      {
        _clickDown = true;
      }
      if (_clickDown && Mouse.GetState().LeftButton == ButtonState.Released)
      {
        _clickDown = false;
        Console.WriteLine("AYOOO");
        _levelEntities.Add(new Boolet((_pos + _leftGunPos), _booletTexture));
      }

      // debug print _entities.count
      Console.WriteLine($" BBEG._entities.length: {_levelEntities.Count}");
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      _sprite.Draw(spriteBatch, _pos);
    }
  }
}
