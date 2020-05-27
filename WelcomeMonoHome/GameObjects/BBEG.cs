using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WelcomeMonoHome.Components;
using WelcomeMonoHome.GUI;

namespace WelcomeMonoHome.GameObjects
{
  public class BBEG : Entity
  {
    float _rotation;
    float _speed = 350f;

    //Texture2D _booletTexture;
    Texture2D _booletTexture;
    string _booletTextureName = "boolet";
    string _bbegTextureName = "BBEG_ok_mini";

    IEntityManagerService _entityManagerService;
    IDebugService _debugService;

    // absolute
    Vector2 _leftGunPos;
    Vector2 _rightGunPos;

    // relative to texture origin
    readonly Vector2 _relativeLeftGunPos = new Vector2(8, 94);
    readonly Vector2 _relativeRightGunPos = new Vector2(119, 94);

    float rateOfFire = 0.33f;
    float nextShot = 0;

    // hp
    int maximumHP = 10;
    int _currentHP;
    public int currentHP
    {
      get
      {
        return _currentHP;
      }
      set
      {
        if (value >= 0 && value <= 10)
        {
          _currentHP = value;
          healthBar._fullnessPercent = _currentHP * 10;
        }
      }
    }

    // healthbar
    HealthBar healthBar;

    public BBEG()
    {
      // setup textures and sprite
      texture = ServiceLocator.GetService<IContentManagerService>().GetTexture(_bbegTextureName);
      _booletTexture = ServiceLocator.GetService<IContentManagerService>().GetTexture(_booletTextureName);
      sprite = new Sprite(texture, Vector2.Zero);

      // Get services
      _entityManagerService = ServiceLocator.GetService<IEntityManagerService>();
      _debugService = ServiceLocator.GetService<IDebugService>();

      // Get absolute gun positions 
      _leftGunPos = new Vector2((pos.X - texture.Width / 2) + _relativeLeftGunPos.X, (pos.Y - texture.Height / 2) + _relativeLeftGunPos.Y);
      _rightGunPos = new Vector2((pos.X - texture.Width / 2) + _relativeRightGunPos.X, (pos.Y - texture.Height / 2) + _relativeRightGunPos.Y);

      // set collision
      hasCollision = true;

      // hp
      _currentHP = maximumHP;
    }

    public void Initialize()
    {
      IGraphicsService _graphicsService = ServiceLocator.GetService<IGraphicsService>();
      int screenWidth = _graphicsService.GetScreenWidth();
      int screenHeight = _graphicsService.GetScreenHeight();

      // Initialize position at the middle of the screen from the sprite's center
      pos = new Vector2(screenWidth / 2, screenHeight / 2);

      // healthBar
      healthBar = new HealthBar(new Vector2(screenWidth / 2, screenHeight * 0.95f), 600, 25);
    }

    public override void Update(GameTime gameTime)
    {
      // Update position from WASD
      Vector2 direction = Vector2.Zero;

      if (Keyboard.GetState().IsKeyDown(Keys.W))
      {
        direction.Y -= 1;
      }
      if (Keyboard.GetState().IsKeyDown(Keys.S))
      {
        direction.Y += 1;
      }
      if (Keyboard.GetState().IsKeyDown(Keys.A))
      {
        direction.X -= 1;
      }
      if (Keyboard.GetState().IsKeyDown(Keys.D))
      {
        direction.X += 1;
      }

      if (direction != Vector2.Zero)
      {
        direction = Vector2.Normalize(direction);
        pos += direction * _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
      }

      // Fire
      if (Mouse.GetState().LeftButton == ButtonState.Pressed && nextShot < (float)gameTime.TotalGameTime.TotalSeconds)
      {
        nextShot = (float)gameTime.TotalGameTime.TotalSeconds + rateOfFire;

        Boolet leftBoolet = new Boolet((pos + _leftGunPos), true, Mouse.GetState().Position.ToVector2());
        Boolet rightBoolet = new Boolet((pos + _rightGunPos), true, Mouse.GetState().Position.ToVector2());

        leftBoolet.Instantiate();
        rightBoolet.Instantiate();
      }
    }

    public override void OnCollision(Entity collider)
    {
      if (collider is Boolet boolet && !boolet.isPlayerBoolet)
      {
        currentHP--;
      }
      else if (collider is HealthPill)
      {
        currentHP++;
      }
    }
  }
}
