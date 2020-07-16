using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WelcomeMonoHome.Components;

public class Shooter : Entity
{
  float _speed;

  Texture2D _shooterTexture;
  string _shooterTextureName = "player_machinegun";

  Vector2 _direction;

  public Shooter()
  {
    transform = new Transform(Vector2.Zero);

    _shooterTexture = ServiceLocator.GetService<IContentManagerService>().GetTexture(_shooterTextureName);

    AddComponent(new AnimatedSprite(_shooterTexture, 1, 4));
  }

  public void Initialize()
  {
    IGraphicsService _graphicsService = ServiceLocator.GetService<IGraphicsService>();
    int screenWidth = _graphicsService.GetScreenWidth();
    int screenHeight = _graphicsService.GetScreenHeight();
    transform.position = new Vector2(screenWidth / 2, screenHeight / 2);
  }

  public override void Update(GameTime gameTime)
  {

  }
}