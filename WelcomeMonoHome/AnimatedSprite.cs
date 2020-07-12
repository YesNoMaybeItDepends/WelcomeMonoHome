using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class AnimatedSprite : IRenderable
{
  public Texture2D texture;
  public int rows;
  public int columns;
  private int currentFrame;
  private int totalFrames;

  Color color;
  Transform transform;
  Vector2 centerPos;

  public AnimatedSprite(Texture2D Texture, int Rows, int Columns)
  {
    texture = Texture;
    rows = Rows;
    columns = Columns;
    currentFrame = 0;
    totalFrames = rows * columns;

    // empty memes

    color = Color.White;
    transform = new Transform(new Vector2(600, 300));
    centerPos = new Vector2(0, 0);
  }

  public void Update()
  {
  }

  public void Draw(SpriteBatch _spriteBatch)
  {
    currentFrame++;
    if (currentFrame == totalFrames)
    {
      currentFrame = 0;
    }

    int width = texture.Width / columns;
    int height = texture.Height / rows;
    int row = (int)((float)currentFrame / (float)columns);
    int column = currentFrame % columns;

    Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
    Rectangle destinationRectangle = new Rectangle((int)transform.position.X, (int)transform.position.Y, width, height);

    _spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);


    // _spriteBatch.Draw(
    //   texture, // texture
    //   destinationRectangle, // position
    //   sourceRectangle, // sourceRectangle?
    //   color, // color
    //   0f, // rotation
    //   centerPos, // origin
    //   SpriteEffects.None, // effects
    //   0f // layerDepth
    // );
  }
}
