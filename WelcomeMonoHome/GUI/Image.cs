using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Image : Renderable
{
  Texture2D texture;
  Color color;
  //public Vector2 position { get; set; }

  public Image(string TextureName, Vector2 Position)
  {
    texture = ServiceLocator.GetService<IContentManagerService>().GetTexture(TextureName);
    position = Position;
    color = Color.White;
  }

  public override void Draw(SpriteBatch spriteBatch)
  {
    spriteBatch.Draw(texture, position, color);
  }
}