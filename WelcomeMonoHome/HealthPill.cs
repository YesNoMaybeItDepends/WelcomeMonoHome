using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WelcomeMonoHome.Components;
using WelcomeMonoHome.GameObjects;
using System;

public class HealthPill : Entity
{
  string textureName = "HealthPill";
  float spawnFrameSecond;
  float timeToDespawn = 5f;

  IGuiService gui;

  public HealthPill(GameTime gameTime)
  {
    spawnFrameSecond = (float)gameTime.TotalGameTime.TotalSeconds;
    texture = ServiceLocator.GetService<IContentManagerService>().GetTexture(textureName);
    sprite = new Sprite(texture, Vector2.Zero);
    sprite.scale = new Vector2(3, 3);
    hasCollision = true;
    gui = ServiceLocator.GetService<IGuiService>();
    gui.NewConsole("HealthPills", new Vector2(400, 400), null, null);
  }

  public override void Update(GameTime gameTime)
  {
    // Change pill color to 20% darker for each second 
    float elapsedTime = (float)gameTime.TotalGameTime.TotalSeconds - spawnFrameSecond;
    float m = 255 * (0.2f * elapsedTime);
    float r = 255 - m;
    sprite.color = new Color((int)r, (int)r, (int)r);

    // Destroy if we reach time to despawn
    if (gameTime.TotalGameTime.TotalSeconds > spawnFrameSecond + timeToDespawn)
    {
      Destroy();
    }
  }

  public override void OnCollision(Entity collider)
  {
    if (collider is BBEG bbeg)
    {
      Destroy();
    }
  }

}