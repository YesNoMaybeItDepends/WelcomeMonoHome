using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WelcomeMonoHome.Components;

public class CyclopsTroll : Entity
{
  float speed = 200f;
  Vector2 direction;
  Keys firstDirection = default;

  Texture2D textureIdle;
  Texture2D textureWalk;
  Texture2D textureAttack;

  string nameIdle = "cyclops_troll_idle_sheet";
  string nameWalk = "cyclops_troll_walk_sheet";
  string nameAttack = "cyclops_troll_attack_sheet";

  Spritesheet sheetIdle;
  Spritesheet sheetWalk;
  Spritesheet sheetAttack;

  AnimatedSprite animatedSprite;

  bool attacking = true;

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

    sheetWalk.AddAnimation("walk_north", 1, 1, 1, 4);
    sheetWalk.AddAnimation("walk_west", 2, 1, 1, 4);
    sheetWalk.AddAnimation("walk_south", 3, 1, 1, 4);
    sheetWalk.AddAnimation("walk_east", 4, 1, 1, 4);

    sheetAttack.AddAnimation("attack_north", 1, 1, 1, 3);
    sheetAttack.AddAnimation("attack_west", 2, 1, 1, 3);
    sheetAttack.AddAnimation("attack_south", 3, 1, 1, 3);
    sheetAttack.AddAnimation("attack_east", 4, 1, 1, 3);

    animatedSprite = new AnimatedSprite(sheetIdle);
    animatedSprite.originOffsetPercent = new Vector2(0.5f, 0.5f);
    animatedSprite.spritesheet = sheetIdle;
    animatedSprite.SetAnimation("idle_south");

    animatedSprite.originOffsetPercent = new Vector2(0.5f, 0.5f);
    animatedSprite.spritesheet = sheetAttack;
    animatedSprite.SetAnimation("attack_south");

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

    bool w = input.GetKeyStateIS(Microsoft.Xna.Framework.Input.Keys.W).isDown;
    bool a = input.GetKeyStateIS(Microsoft.Xna.Framework.Input.Keys.A).isDown;
    bool s = input.GetKeyStateIS(Microsoft.Xna.Framework.Input.Keys.S).isDown;
    bool d = input.GetKeyStateIS(Microsoft.Xna.Framework.Input.Keys.D).isDown;

    if (!attacking && !input.GetKeyStateIS(firstDirection).isDown)
    {
      animatedSprite.spritesheet = sheetWalk;

      if (w)
      {
        firstDirection = Keys.W;
        animatedSprite.SetAnimation("walk_north");
      }
      else if (a)
      {
        firstDirection = Keys.A;
        animatedSprite.SetAnimation("walk_west");
      }
      else if (s)
      {
        firstDirection = Keys.S;
        animatedSprite.SetAnimation("walk_south");
      }
      else if (d)
      {
        firstDirection = Keys.D;
        animatedSprite.SetAnimation("walk_east");
      }
      else
      {
        animatedSprite.spritesheet = sheetIdle;

        if (firstDirection == Keys.W)
        {
          animatedSprite.SetAnimation("idle_north");
        }
        else if (firstDirection == Keys.A)
        {
          animatedSprite.SetAnimation("idle_west");
        }
        else if (firstDirection == Keys.S)
        {
          animatedSprite.SetAnimation("idle_south");
          animatedSprite.spritesheet = sheetAttack;
          animatedSprite.SetAnimation("attack_south");
        }
        else if (firstDirection == Keys.D)
        {
          animatedSprite.SetAnimation("idle_east");
        }

        firstDirection = default;
      }
    }

    UpdateDirection();
    transform.position += (direction * speed) * (float)gameTime.ElapsedGameTime.TotalSeconds;

    ButtonStateIS left = input.GetButtonStateIS(MouseButtons.Left);
    if (left.isDown && !left.isRepeat)
    {
      // animatedSprite.spritesheet = sheetAttack;
      // animatedSprite.SetAnimation("attack_south");
    }
  }

  public void UpdateDirection()
  {
    direction = Vector2.Zero;

    if (Keyboard.GetState().IsKeyDown(Keys.W))
    {
      direction.Y -= 1;
    }
    if (Keyboard.GetState().IsKeyDown(Keys.S))
    {
      direction.Y += 1;
    }
    if (Keyboard.GetState().IsKeyDown(Keys.A))
    {
      direction.X -= 1;
    }
    if (Keyboard.GetState().IsKeyDown(Keys.D))
    {
      direction.X += 1;
    }

    if (direction != Vector2.Zero)
    {
      direction = Vector2.Normalize(direction);
    }
  }
}