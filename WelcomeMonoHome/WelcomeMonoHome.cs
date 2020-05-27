using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WelcomeMonoHome
{
  public class WelcomeMonoHome : Game
  {
    public GraphicsDeviceManager graphics;
    GraphicsService graphicsService;

    SpriteBatch spriteBatch;

    Scene scene;

    bool PAUSED = false;
    bool HELD = false;

    public WelcomeMonoHome()
    {
      // initialize graphics
      graphics = new GraphicsDeviceManager(this);
      graphicsService = new GraphicsService(graphics);

      // map services
      ServiceLocator.SetService<IGraphicsService>(graphicsService);

      Content.RootDirectory = "Content";
      IsMouseVisible = true;


    }

    protected override void Initialize()
    {
      graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
      graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
      //graphics.IsFullScreen = true;
      graphics.HardwareModeSwitch = false;
      graphics.ApplyChanges();
      base.Initialize();
    }

    protected override void LoadContent()
    {
      spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();

      if (Keyboard.GetState().IsKeyDown(Keys.Space))
      {
        if (!HELD)
        {
          HELD = true;
        }
      }

      if (Keyboard.GetState().IsKeyUp(Keys.Space) && HELD)
      {
        HELD = false;
        if (PAUSED == false)
        {
          PAUSED = true;
        }
        else
        {
          PAUSED = false;
        }
      }

      if (!PAUSED)
      {
        if (scene == null)
        {
          scene = new Scene(Content, spriteBatch, graphics);
          scene.LoadContent();
          scene.Initialize();
        }

        scene.Update(gameTime);

        base.Update(gameTime);
      }
    }

    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.CornflowerBlue);

      scene.Draw(gameTime);

      base.Draw(gameTime);
    }
  }
}
