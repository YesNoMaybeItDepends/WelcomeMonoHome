using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public abstract class Renderable : IRenderable
{
  public virtual Vector2 position { get; set; }
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

  public void Instantiate()
  {
    ServiceLocator.GetService<IrendererService>().AddRenderable(this);
  }

  public void Destroy()
  {
    ServiceLocator.GetService<IrendererService>().RemoveRenderable(this);
  }

  public abstract void Draw(SpriteBatch spriteBatch);

  public virtual void OnBecameInvisible() { }

  public virtual void OnBecameVisible() { }
}