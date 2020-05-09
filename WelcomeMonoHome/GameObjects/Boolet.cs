using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using WelcomeMonoHome.Components;

public class Boolet : Entity
{
  Sprite _sprite;
  Vector2 _pos;
  Texture2D _texture;
  float _rotation;
  float _speed;

  public Boolet(Vector2 pos, Texture2D texture)
  {
    _pos = pos;
    _texture = texture;
    _sprite = new Sprite(texture);
  }

  public override void Update(GameTime gameTime)
  {

  }

  public override void Draw(SpriteBatch spriteBatch)
  {
    _sprite.Draw(spriteBatch, _pos);
  }

}