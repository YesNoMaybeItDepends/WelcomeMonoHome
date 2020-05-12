using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public abstract class Renderable : IRenderable
{
  Texture2D texture;
  Vector2 position;

  public bool isVisible
  {
    get
    {
      return isVisible;
    }
    set
    {
      if (isVisible == true && value == false)
      {
        isVisible = false;
        OnBecameInvisible();
      }
      else if (isVisible == false && value == true)
      {
        isVisible = true;
        OnBecameVisible();
      }
    }
  }

  public void Draw(SpriteBatch spriteBatch)
  {
    spriteBatch.Draw()
  }

  public abstract void OnBecameInvisible();

  public abstract void OnBecameVisible();
}