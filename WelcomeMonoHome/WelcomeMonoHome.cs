using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WelcomeMonoHome
{
  public class WelcomeMonoHome : Game
  {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    Scene scene;

    public static Texture2D whitePixelTexture;

    public WelcomeMonoHome()
    {
      graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
      IsMouseVisible = true;
    }

    protected override void Initialize()
    {
      base.Initialize();
    }

    protected override void LoadContent()
    {
      whitePixelTexture = new Texture2D(GraphicsDevice, 1, 1);
      spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();

      if (scene == null)
      {
        scene = new Scene(Content, spriteBatch, graphics);
        scene.LoadContent();
        scene.Initialize();
      }

      scene.Update(gameTime);

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.CornflowerBlue);

      scene.Draw();

      base.Draw(gameTime);
    }
  }
}
