using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WelcomeMonoHome.Components;
using WelcomeMonoHome.GameObjects;
using System;

public class HealthPill : Entity
{
  string textureName = "HealthPill";
  float spawnFrameSecond;
  float spawnFrameMiliseconds;
  float muhelapsedTime;
  float timeToDespawn = 5f;
  float timeToSpawn = 1f;
  bool _spawning = true;
  Vector2 _spawnScale = new Vector2(1, 1);
  Vector2 _normalScale = new Vector2(3, 3);
  IGuiService gui;

  public HealthPill(GameTime gameTime)
  {
    spawnFrameSecond = (float)gameTime.TotalGameTime.TotalSeconds;
    spawnFrameMiliseconds = (float)gameTime.TotalGameTime.TotalMilliseconds;
    texture = ServiceLocator.GetService<IContentManagerService>().GetTexture(textureName);
    sprite = new Sprite(texture, Vector2.Zero);
    sprite.scale = _spawnScale;
    hasCollision = true;
    gui = ServiceLocator.GetService<IGuiService>();
    gui.NewConsole("HealthPills", new Vector2(400, 400), null, null);
  }

  public override void Update(GameTime gameTime)
  {
    float elapsedTime = (float)gameTime.TotalGameTime.TotalSeconds - spawnFrameSecond;

    // If spawning
    if (elapsedTime < timeToSpawn)
    {
      // "play" spawning "animation"
      float amount = MathHelper.Clamp(elapsedTime / timeToSpawn, 0, 1);
      float percentToAdd = MathHelper.Lerp(1.0f, 3.0f, amount);
      sprite.scale = new Vector2(percentToAdd, percentToAdd);
    }
    // If expired
    else if (gameTime.TotalGameTime.TotalSeconds > spawnFrameSecond + timeToDespawn + timeToSpawn)
    {
      Destroy();
    }
    else
    {
      // Change pill color to 20% darker for each second 
      float percentToSubstract = 255 * (0.17f * elapsedTime);
      float value = 255 - percentToSubstract;
      sprite.color = new Color((int)value, (int)value, (int)value);
    }

  }

  public void Spawning()
  {

  }

  public override void OnCollision(Entity collider)
  {
    if (collider is BBEG bbeg)
    {
      Destroy();
    }
  }

}