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

    public TextBox(string Text, Vector2 Position)
    {
      IrendererService renderer = ServiceLocator.GetService<IrendererService>();
      // TODO font = resouces.getfont() or whatever
      text = Text;
      position = Position;
      font = ServiceLocator.GetService<IContentManagerService>().GetFont("MyFont");
      renderer.AddRenderable(this);
    }

    // public ScreenText Instantiate(string Text, Vector2 Position)
    // {
    //
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
      IrendererService renderer = ServiceLocator.GetService<IrendererService>();
      renderer.RemoveRenderable(this);
    }
  }
}