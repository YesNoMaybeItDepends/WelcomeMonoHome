using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace WelcomeMonoHome.GUI
{
  public class TextBox : IRenderable
  {
    public string text { get; set; }
    SpriteFont font { get; set; }
    public Vector2 position { get; set; }
    public Transform transform;

    public TextBox(string Text, Vector2 Position)
    {
      // TODO font = resouces.getfont() or whatever
      text = Text;
      position = Position;
      font = ServiceLocator.GetService<IContentManagerService>().GetFont("MyFont");

      ServiceLocator.GetService<IRendererService>().AddRenderable(this);
    }

    // why was this commented out?
    // public ScreenText Instantiate(Vector2 Position)
    // {
    //   transform = new Transform(this, Position);
    // }

    public void Draw(SpriteBatch spriteBatch)
    {
      if (font != null)
      {
        spriteBatch.DrawString(font, text, position, Color.White);

      }
    }

    public void UpdateText(string Text)
    {
      this.text = Text;
    }

    public void Destroy()
    {
      ServiceLocator.GetService<IRendererService>().RemoveRenderable(this);
    }

    public void OnClick()
    {

    }
  }
}