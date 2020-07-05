using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace WelcomeMonoHome.Components
{
  public class Sprite : IRenderable
  {
    public Texture2D _texture;
    public Texture2D _pixel;
    public Color color;
    // origin from the middle
    public Vector2 origin;
    // ? Entity _parent;

    public Transform transform;

    public Sprite(Texture2D texture, Transform Transform)
    {
      _texture = texture;
      origin = new Vector2(texture.Width / 2, texture.Height / 2);
      color = Color.White;

      // get pixel texture
      _pixel = ServiceLocator.GetService<IContentManagerService>().GetTexture("pixel");
      transform = Transform;
    }

    public void LoadContent()
    {

    }

    public void Update()
    {

    }

    public void Draw(SpriteBatch _spriteBatch)
    {
      _spriteBatch.Draw(
        _texture, // texture
        transform.position, // position
        null, // sourceRectangle?
        color, // color
        0f, // rotation
        origin, // origin
        transform.scale, // scale
        SpriteEffects.None, // effects
        0f // layerDepth
      );

      // draw test rectangle
      DrawRectangle(_spriteBatch);

      //ServiceLocator.GetService<IGuiService>().ConsoleWriteLine("help", position.ToString());
      //ServiceLocator.GetService<IGuiService>().ConsoleWriteLine("help", origin.ToString());
    }

    public void Instantiate()
    {
      ServiceLocator.GetService<IRendererService>().AddRenderable(this);
    }

    public void Destroy()
    {
      ServiceLocator.GetService<IRendererService>().RemoveRenderable(this);
    }

    public void DrawRectangle(SpriteBatch _spriteBatch)
    {

      Rectangle rectangle = GetSpriteRectangle();

      _spriteBatch.Draw(_pixel, rectangle, Color.White * 0.5f);
    }

    public Rectangle GetSpriteRectangle()
    {
      return new Rectangle(
        (int)(transform.position.X - (_texture.Width * transform.scale.X) / 2),
        (int)(transform.position.Y - (_texture.Height * transform.scale.Y) / 2),
        (int)(_texture.Width * transform.scale.X),
        (int)(_texture.Height * transform.scale.Y));
    }
  }
}
