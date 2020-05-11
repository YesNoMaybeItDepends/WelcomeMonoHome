using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WelcomeMonoHome.Components;

public interface IEntity
{
  Vector2 pos { get; set; }
  Sprite sprite { get; set; }
  Texture2D texture { get; set; }

  void Update(GameTime gameTime);
  void Draw(SpriteBatch spriteBatch);
}