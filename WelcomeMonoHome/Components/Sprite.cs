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
      _texture = texture;
      position = pos;
      origin = new Vector2(texture.Width / 2, texture.Height / 2);
      scale = new Vector2(1, 1);
      color = Color.White;

      // test rectangle
      //_pixel = ServiceLocator.GetService<IContentManagerService>().GetTexture("pixel");
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
      //DrawRectangle(_spriteBatch);
    }

    public void Instantiate()
    {
      ServiceLocator.GetService<IrendererService>().AddRenderable(this);
    }

    public void Destroy()
    {
      ServiceLocator.GetService<IrendererService>().RemoveRenderable(this);
    }

    public void DrawRectangle(SpriteBatch _spriteBatch)
    {
      Rectangle rectangle = new Rectangle((int)(position.X - _texture.Width / 2), (int)(position.Y - _texture.Height / 2), (int)(_texture.Width * scale.X), (int)((_texture.Height * scale.Y)));
      _spriteBatch.Draw(_pixel, rectangle, Color.White * 0.5f);
    }
  }
}
