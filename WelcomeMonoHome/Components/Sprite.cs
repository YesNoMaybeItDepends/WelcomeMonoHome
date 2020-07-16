using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace WelcomeMonoHome.Components
{
  public class Sprite : Component, IRenderable
  {
    Texture2D _texture;
    Texture2D _pixel;
    public Color color { get; set; }
    // origin from the middle
    Vector2 _origin;
    // ? Entity _parent;

    Transform _transform;

    public Sprite(Texture2D texture, Transform Transform)
    {
      _texture = texture;
      _origin = new Vector2(texture.Width / 2, texture.Height / 2);
      color = Color.White;

      // get pixel texture
      _pixel = ServiceLocator.GetService<IContentManagerService>().GetTexture("pixel");
      _transform = Transform;
    }

    public void LoadContent()
    {

    }

    public override void Update()
    {

    }

    public void Draw(SpriteBatch _spriteBatch)
    {
      _spriteBatch.Draw(
        _texture, // texture
        _transform.position, // position
        null, // sourceRectangle?
        color, // color
        0f, // rotation
        _origin, // origin
        _transform.scale, // scale
        SpriteEffects.None, // effects
        0f // layerDepth
      );

      // draw test rectangle
      DrawRectangle(_spriteBatch);

      //ServiceLocator.GetService<IGuiService>().ConsoleWriteLine("help", position.ToString());
      //ServiceLocator.GetService<IGuiService>().ConsoleWriteLine("help", origin.ToString());
    }

    public override void Instantiate()
    {
      ServiceLocator.GetService<IRendererService>().AddRenderable(this);
    }

    public override void Destroy()
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
        (int)(_transform.position.X - (_texture.Width * _transform.scale.X) / 2),
        (int)(_transform.position.Y - (_texture.Height * _transform.scale.Y) / 2),
        (int)(_texture.Width * _transform.scale.X),
        (int)(_texture.Height * _transform.scale.Y));
    }
  }
}
