using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WelcomeMonoHome
{
  public class Game2 : Game
  {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    // ! TODO TUTORIAL:
    Texture2D ballTexture;
    Vector2 ballPosition;
    float ballSpeed;

    public Game2()
    {
      graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
    }

    protected override void Initialize()
    {
      // ? Add your initialization logic here
      ballPosition = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
      ballSpeed = 100f;

      base.Initialize();
    }

    protected override void LoadContent()
    {
      // Create a new SpriteBatch, which can be used to draw textures.
      spriteBatch = new SpriteBatch(GraphicsDevice);

      // ? use this.Content to load your game content here
      Texture2D image = Content.Load<Texture2D>("BBEG_ok_mini");
      ballTexture = image;
    }

    protected override void UnloadContent()
    {
      // ? Unload any non ContentManager content here
      Content.Unload();
    }

    protected override void Update(GameTime gameTime)
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();

      // ? Add your update logic here
      var kstate = Keyboard.GetState();

      if (kstate.IsKeyDown(Keys.Up))
        ballPosition.Y -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

      if (kstate.IsKeyDown(Keys.Down))
        ballPosition.Y += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

      if (kstate.IsKeyDown(Keys.Left))
        ballPosition.X -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

      if (kstate.IsKeyDown(Keys.Right))
        ballPosition.X += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;


      // keep memer in bound

      if (ballPosition.X > graphics.PreferredBackBufferWidth - ballTexture.Width / 2)
        ballPosition.X = graphics.PreferredBackBufferWidth - ballTexture.Width / 2;
      else if (ballPosition.X < ballTexture.Width / 2)
        ballPosition.X = ballTexture.Width / 2;

      if (ballPosition.Y > graphics.PreferredBackBufferHeight - ballTexture.Height / 2)
        ballPosition.Y = graphics.PreferredBackBufferHeight - ballTexture.Height / 2;
      else if (ballPosition.Y < ballTexture.Height / 2)
        ballPosition.Y = ballTexture.Height / 2;


      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.CornflowerBlue);

      // ? Add your drawing code here

      spriteBatch.Begin();
      spriteBatch.Draw(ballTexture, ballPosition, null, Color.White, 0f, new Vector2(ballTexture.Width / 2, ballTexture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
      spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
