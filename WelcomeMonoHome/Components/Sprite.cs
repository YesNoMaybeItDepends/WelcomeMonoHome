using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace WelcomeMonoHome.Components
{
  public class Sprite : IRenderable
  {
    public Vector2 position { get; set; }
    public Texture2D _sprite;
    Color _color;
    Vector2 _origin;
    // ? Entity _parent;

    public Sprite(Texture2D texture, Vector2 pos)
    {
      position = pos;
      _sprite = texture;
      _color = Color.White;
      _origin = new Vector2(texture.Width / 2, texture.Height / 2);
      ServiceLocator.GetService<IrendererService>().AddRenderable(this);
    }

    public void LoadContent()
    {

    }

    public void Update()
    {

    }

    public void Draw(SpriteBatch _spriteBatch)
    {

      _spriteBatch.Draw(_sprite, position - _origin, _color);
    }

    // public void Draw(SpriteBatch _spriteBatch, Vector2 pos)
    // {

    //   _spriteBatch.Draw(_sprite, pos - _origin, _color);
    // }
  }
}
