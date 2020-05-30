using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class StartScreen : Scene
{
  SpriteBatch _spriteBatch;

  // Services
  ICollisionManagerService _collisionService;
  IContentManagerService _contentService;
  IDebugService _debugService;
  IEntityManagerService _entityService;
  IGuiService _guiService;
  IInputService _inputService;
  IRendererService _rendererService;
  ISceneManagerService _sceneService;

  StartScreen()
  {
    // Get services
    _collisionService = ServiceLocator.GetService<ICollisionManagerService>();
    _contentService = ServiceLocator.GetService<IContentManagerService>();
    _debugService = ServiceLocator.GetService<IDebugService>();
    _entityService = ServiceLocator.GetService<IEntityManagerService>();
    _guiService = ServiceLocator.GetService<IGuiService>();
    _inputService = ServiceLocator.GetService<IInputService>();
    _rendererService = ServiceLocator.GetService<IRendererService>();
    _sceneService = ServiceLocator.GetService<ISceneManagerService>();
  }

  public override void Draw(GameTime gameTime)
  {
    throw new System.NotImplementedException();
  }

  public override void End()
  {
    throw new System.NotImplementedException();
  }

  public override void Initialize()
  {
    throw new System.NotImplementedException();
  }

  public override void LoadContent()
  {
    throw new System.NotImplementedException();
  }

  public override void Start()
  {
    throw new System.NotImplementedException();
  }

  public override void UnloadContent()
  {
    throw new System.NotImplementedException();
  }

  public override void Update(GameTime gameTime)
  {
    throw new System.NotImplementedException();
  }
}