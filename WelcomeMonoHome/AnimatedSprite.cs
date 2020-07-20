using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class AnimatedSprite : Component, IRenderable
{
  Texture2D _pixel;
  public int rows;
  public int columns;
  private int _currentFrame;
  private int _totalFrames;
  private int _frameDelay = 5;
  private int _frameCount = 0;

  public Color color { get; set; }
  Transform _transform;
  public Vector2 originOffsetPercent = Vector2.One;
  public Vector2 origin;

  Animation currentAnimation;
  public Spritesheet spritesheet;

  public void SetAnimation(string name)
  {
    currentAnimation = spritesheet.animations[name];
    _currentFrame = 0;
  }

  public AnimatedSprite(Spritesheet Spritesheet)
  {
    spritesheet = Spritesheet;

    _currentFrame = 0;

    color = Color.White;

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

    // animation speed and step counter
    _frameCount++;
    if (_frameCount == _frameDelay)
    {
      _frameCount = 0;
      _currentFrame++;
      if (_currentFrame >= currentAnimation.rectangles.Count)
      {
        _currentFrame = 0;
      }
    }

    Rectangle destinationRectangle = new Rectangle((int)_transform.position.X, (int)_transform.position.Y, spritesheet.cellWidth, spritesheet.cellHeight);

    //destinationRectangle = new Rectangle((int)_transform.position.X, (int)_transform.position.Y, (int)(spritesheet.cellWidth + _transform.position.X), (int)(spritesheet.cellHeight + _transform.position.Y));

    origin = new Vector2((spritesheet.cellWidth * originOffsetPercent.X), (spritesheet.cellHeight * originOffsetPercent.Y));

    _spriteBatch.Draw(
      spritesheet.texture, // texture
      destinationRectangle, // position
      currentAnimation.rectangles[_currentFrame], // sourceRectangle
      color, // color
      _transform.rotation, // rotation
      origin, // origin
      SpriteEffects.None, // effects
      0f // layerDepth
    );

    _spriteBatch.Draw(
      spritesheet.texture, // texture
      destinationRectangle, // position
      currentAnimation.rectangles[_currentFrame], // sourceRectangle
      color, // color
      _transform.rotation, // rotation
      origin, // origin
      SpriteEffects.None, // effects
      0f // layerDepth
    );

    //DrawRectangle(destinationRectangle, currentAnimation.rectangles[_currentFrame], origin, _spriteBatch);

  }

  public override void Instantiate()
  {
    ServiceLocator.GetService<IRendererService>().AddRenderable(this);
  }

  public override void Destroy()
  {
    ServiceLocator.GetService<IRendererService>().RemoveRenderable(this);
  }

  public void DrawRectangle(Rectangle destination, Rectangle source, Vector2 origin, SpriteBatch _spriteBatch)
  {
    //_spriteBatch.Draw(_pixel, rectangle, Color.White * 0.5f);
    _spriteBatch.Draw(
      _pixel, // texture
      destination, // position
      source, // sourceRectangle
      color * 0.5f, // color
      _transform.rotation, // rotation
      origin, // origin
      SpriteEffects.None, // effects
      0f); // layerDepth
  }

  public Rectangle GetSpriteRectangle()
  {
    Texture2D _texture = spritesheet.texture;

    return new Rectangle(
      (int)(_transform.position.X - (_texture.Width * _transform.scale.X) / 2),
      (int)(_transform.position.Y - (_texture.Height * _transform.scale.Y) / 2),
      (int)(_texture.Width * _transform.scale.X),
      (int)(_texture.Height * _transform.scale.Y));
  }
}
