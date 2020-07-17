using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class AnimatedSprite : Component, IRenderable
{
  Texture2D _texture;
  Texture2D _pixel;
  public int rows;
  public int columns;
  private int _currentFrame;
  private int _totalFrames;
  private int _frameDelay = 5;
  private int _frameCount = 0;

  public Color color { get; set; }
  Transform _transform;
  Vector2 origin;
  public Vector2 originOffsetPercent = Vector2.One;

  public AnimatedSprite(Texture2D Texture, int Rows, int Columns)
  {
    _texture = Texture;
    rows = Rows;
    columns = Columns;
    _currentFrame = 0;
    _totalFrames = rows * columns;

    // empty memes

    color = Color.White;
    origin = new Vector2(0, 0);
    //_transform = new Transform(new Vector2(600, 600));

    _pixel = ServiceLocator.GetService<IContentManagerService>().GetTexture("pixel");
  }

  public void LoadContent()
  {

  }

  public override void Update()
  {
  }

  public void Draw(SpriteBatch _spriteBatch)
  {
    if (parent != null && _transform == null)
    {
      _transform = parent.transform;
    }

    _frameCount++;
    if (_frameCount == _frameDelay)
    {
      _frameCount = 0;
      _currentFrame++;
      if (_currentFrame == _totalFrames)
      {
        _currentFrame = 0;
      }
    }

    int width = _texture.Width / columns;
    int height = _texture.Height / rows;
    int row = (int)((float)_currentFrame / (float)columns);
    int column = _currentFrame % columns;

    Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
    Rectangle destinationRectangle = new Rectangle((int)_transform.position.X, (int)_transform.position.Y, width, height);

    origin = new Vector2((width * originOffsetPercent.X), (height * originOffsetPercent.Y));

    _spriteBatch.Draw(
      _texture, // texture
      destinationRectangle, // position
      sourceRectangle, // sourceRectangle
      color, // color
      _transform.rotation, // rotation
      origin, // origin
      SpriteEffects.None, // effects
      0f // layerDepth
    );
  }

  public override void Instantiate()
  {
    ServiceLocator.GetService<IRendererService>().AddRenderable(this);
  }

  public override void Destroy()
  {
    ServiceLocator.GetService<IRendererService>().RemoveRenderable(this);
  }

  public void DrawRectangle(SpriteBatch _spriteBatch)
  {

    Rectangle rectangle = GetSpriteRectangle();

    _spriteBatch.Draw(_pixel, rectangle, Color.White * 0.5f);
  }

  public Rectangle GetSpriteRectangle()
  {
    return new Rectangle(
      (int)(_transform.position.X - (_texture.Width * _transform.scale.X) / 2),
      (int)(_transform.position.Y - (_texture.Height * _transform.scale.Y) / 2),
      (int)(_texture.Width * _transform.scale.X),
      (int)(_texture.Height * _transform.scale.Y));
  }
}
