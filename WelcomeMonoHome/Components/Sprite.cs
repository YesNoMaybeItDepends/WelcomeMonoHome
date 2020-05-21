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
    public Color color;
    // origin from the middle
    public Vector2 origin;
    // ? Entity _parent;
    public Vector2 scale;

    public Sprite(Texture2D texture, Vector2 pos)
    {
      position = pos;
      _texture = texture;
      origin = new Vector2(texture.Width / 2, texture.Height / 2);
      scale = new Vector2(1, 1);
      color = Color.White;

      // test rectangle
      //_pixel = ServiceLocator.GetService<IResourceManagerService>().GetTexture("pixel");

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
      // TODO clear this up
      _spriteBatch.Draw(_texture, position - origin, null, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

      // draw test rectangle
      // _spriteBatch.Draw(_pixel, new Rectangle((int)(position.X - _texture.Width / 2), (int)(position.Y - _texture.Height / 2), _texture.Width, _texture.Height), Color.White);
    }
  }
}
