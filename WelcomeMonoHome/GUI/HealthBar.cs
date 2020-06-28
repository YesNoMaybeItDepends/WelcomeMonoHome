using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WelcomeMonoHome.GUI
{
  public class HealthBar : IRenderable
  {
    public Texture2D _texture;
    public Vector2 position { get; set; }
    Vector2 _centerPosition;
    Vector2 _topLeftPosition;
    int _width;
    int _height;
    int _borderSize = 5;
    public int _fullnessPercent = 100;

    public HealthBar(Vector2 centerPosition, int totalWidth, int totalHeight)
    {
      IContentManagerService resources = ServiceLocator.GetService<IContentManagerService>();
      _texture = resources.GetTexture("pixel");

      _centerPosition = centerPosition;
      _width = totalWidth;
      _height = totalHeight;
      _topLeftPosition.X = centerPosition.X - _width / 2;
      _topLeftPosition.Y = centerPosition.Y - _height / 2;

      // TODO Should this be here? I don't think so, it should do instantiate()
      IRendererService renderer = ServiceLocator.GetService<IRendererService>();
      renderer.AddRenderable(this);

    }

    public void Draw(SpriteBatch spriteBatch)
    {
      // draw black border
      spriteBatch.Draw(_texture, new Rectangle((int)_topLeftPosition.X - _borderSize, (int)_topLeftPosition.Y - _borderSize, _width + _borderSize * 2, _height + _borderSize * 2), Color.Black);

      // draw white bar
      spriteBatch.Draw(_texture, new Rectangle((int)_topLeftPosition.X, (int)_topLeftPosition.Y, _width, _height), Color.White);

      // draw red bar
      spriteBatch.Draw(_texture, new Rectangle((int)_topLeftPosition.X, (int)_topLeftPosition.Y, (int)(_width * (_fullnessPercent * 0.01)), _height), Color.Red);
    }

    // _spriteBatch.Draw(_pixel, new Rectangle((int)(position.X - _texture.Width / 2), (int)(position.Y - _texture.Height / 2), _texture.Width, _texture.Height), Color.White);

  }
}