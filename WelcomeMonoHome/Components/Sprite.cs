using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace WelcomeMonoHome.Components
{
  class Sprite
  {
    Texture2D _sprite;
    Color _color;
    Vector2 _origin;
    // ? Entity _parent;

    public Sprite(Texture2D texture)
    {
      Console.WriteLine("Sprite constructor");

      _sprite = texture;
      _color = Color.White;
      _origin = new Vector2(texture.Width / 2, texture.Height / 2);
    }

    public void LoadContent()
    {

    }

    public void Update()
    {

    }

    public void Draw(SpriteBatch _spriteBatch, Vector2 pos)
    {

      _spriteBatch.Draw(_sprite, pos - _origin, _color);
    }
  }
}
