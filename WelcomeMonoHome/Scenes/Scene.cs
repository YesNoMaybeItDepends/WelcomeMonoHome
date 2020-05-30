using Microsoft.Xna.Framework;

public abstract class Scene
{
  public bool isContentLoaded = false;
  public bool isStarted = false;
  public abstract void LoadContent();
  public abstract void Start();
  public abstract void Initialize();
  public abstract void Update(GameTime gameTime);
  public abstract void Draw(GameTime gameTime);
  public abstract void End();
  public abstract void UnloadContent();
}