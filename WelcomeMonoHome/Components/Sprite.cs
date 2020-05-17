using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace WelcomeMonoHome.Components
{
  public class Sprite : IRenderable
  {
    public Vector2 position { get; set; }
    public Texture2D _texture;
    public Texture2D _pixel;
    Color _color;
    public Vector2 origin;
    // ? Entity _parent;

    public Sprite(Texture2D texture, Vector2 pos)
    {
      position = pos;
      _texture = texture;
      origin = new Vector2(texture.Width / 2, texture.Height / 2);
      ServiceLocator.GetService<IrendererService>().AddRenderable(this);

      // test rectangle
      _pixel = ServiceLocator.GetService<IResourceManagerService>().GetTexture("pixel");
      _color = Color.White;
    }

    public void LoadContent()
    {

    }

    public void Update()
    {

    }

    public void Draw(SpriteBatch _spriteBatch)
    {
      _spriteBatch.Draw(_texture, position - origin, _color);

      // draw test rectangle
      // _spriteBatch.Draw(_pixel, new Rectangle((int)(position.X - _texture.Width / 2), (int)(position.Y - _texture.Height / 2), _texture.Width, _texture.Height), Color.White);
    }
  }
}
