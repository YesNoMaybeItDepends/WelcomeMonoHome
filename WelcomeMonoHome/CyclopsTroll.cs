using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WelcomeMonoHome.Components;

public class CyclopsTroll : Entity
{
  float speed;
  Vector2 direction;

  Texture2D textureIdle;
  Texture2D textureWalk;
  Texture2D textureAttack;

  string nameIdle = "cyclops_troll_idle_sheet";
  string nameWalk = "cyclops_troll_walk_sheet";
  string nameAttack = "cyclops_troll_attack_sheet";

  Spritesheet sheetIdle;
  Spritesheet sheetWalk;
  Spritesheet sheetAttack;

  public CyclopsTroll()
  {
    transform = new Transform(Vector2.Zero);

    textureIdle = ServiceLocator.GetService<IContentManagerService>().GetTexture(nameIdle);
    textureWalk = ServiceLocator.GetService<IContentManagerService>().GetTexture(nameWalk);
    textureAttack = ServiceLocator.GetService<IContentManagerService>().GetTexture(nameAttack);

    sheetIdle = new Spritesheet(textureIdle, 4, 4);
    sheetWalk = new Spritesheet(textureWalk, 4, 4);
    sheetAttack = new Spritesheet(textureAttack, 4, 3);

    sheetIdle.AddAnimation("idle_north", 1, 1, 1, 4);
    sheetIdle.AddAnimation("idle_west", 2, 1, 1, 4);
    sheetIdle.AddAnimation("idle_south", 3, 1, 1, 4);
    sheetIdle.AddAnimation("idle_east", 4, 1, 1, 4);

    AnimatedSprite animatedSprite = new AnimatedSprite(textureIdle, 4, 4);
    animatedSprite.originOffsetPercent = new Vector2(0.5f, 0.5f);
    animatedSprite.spritesheet = sheetIdle;
    animatedSprite.currentAnimation = sheetIdle.animations["idle_west"];
    AddComponent(animatedSprite);
  }

  public void Initialize()
  {
    IGraphicsService _graphicsService = ServiceLocator.GetService<IGraphicsService>();
    int screenWidth = _graphicsService.GetScreenWidth();
    int screenHeight = _graphicsService.GetScreenHeight();
    transform.position = new Vector2(screenWidth / 2 + 100, screenHeight / 2 + 100);
  }

  public override void Update(GameTime gameTime)
  {
    IInputService input = ServiceLocator.GetService<IInputService>();
    Vector2 mouse = input.GetMouseWorldPos();
    double angle = Math.Atan2(mouse.Y - transform.position.Y, mouse.X - transform.position.X);
    transform.rotation = (float)angle;
  }
}