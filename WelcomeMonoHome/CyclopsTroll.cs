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
  Keys facingDirection = Keys.S;

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

  string state = "idle";

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

    facingDirection = Keys.S;

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

    ButtonStateIS left = input.GetButtonStateIS(MouseButtons.Left);

    direction = Vector2.Zero;

    // attack state
    if (state == "attack" || (left.isDown && !left.isRepeat))
    {
      // start attack
      if (state != "attack" && (left.isDown && !left.isRepeat))
      {
        state = "attack";
        changeAnimationOnDirection(facingDirection, sheetAttack, "attack");
      }

      // continue attack
      else if (state == "attack")
      {
        // if animation finished
        if (animatedSprite.animationFinished)
        {
          // transition to move, remembering direction
          if ((w || a || s || d))
          {
            state = "walk";
            changeAnimationOnDirection(facingDirection, sheetWalk, "walk");
          }
          else
          {
            // transition to idle
            state = "idle";
            changeAnimationOnDirection(facingDirection, sheetIdle, "idle");
            firstDirection = default;
          }
        }
      }
    }
    else if (state == "walk" || (w || a || s || d))
    {
      // stop walking
      if (state == "walk" && (!w && !a && !s && !d))
      {
        state = "idle";
        changeAnimationOnDirection(firstDirection, sheetIdle, "idle");
        firstDirection = default;
      }
      else
      {
        if (!input.GetKeyStateIS(firstDirection).isDown)
        {
          firstDirection = default;
        }

        // start walking or change direction
        if (w && firstDirection == default)
        {
          if (animatedSprite.GetAnimation() != sheetWalk.animations["walk_north"])
          {
            firstDirection = Keys.W;
            facingDirection = Keys.W;
            changeAnimationOnDirection(firstDirection, sheetWalk, "walk");
            state = "walk";
          }
        }
        else if (a && firstDirection == default)
        {
          if (animatedSprite.GetAnimation() != sheetWalk.animations["walk_west"])
          {
            firstDirection = Keys.A;
            facingDirection = Keys.A;
            changeAnimationOnDirection(firstDirection, sheetWalk, "walk");
            state = "walk";
          }
        }
        else if (s && firstDirection == default)
        {
          if (animatedSprite.GetAnimation() != sheetWalk.animations["walk_south"])
          {
            firstDirection = Keys.S;
            facingDirection = Keys.S;
            changeAnimationOnDirection(firstDirection, sheetWalk, "walk");
            state = "walk";
          }
        }
        else if (d && firstDirection == default)
        {
          if (animatedSprite.GetAnimation() != sheetWalk.animations["walk_east"])
          {
            firstDirection = Keys.D;
            facingDirection = Keys.D;
            changeAnimationOnDirection(firstDirection, sheetWalk, "walk");
            state = "walk";
          }
        }

        // actually move
        UpdateDirection();
        transform.position += (direction * speed) * (float)gameTime.ElapsedGameTime.TotalSeconds;
      }
    }
    else
    {
      // idle update

    }
  }

  public void UpdateDirection()
  {


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

  public void changeAnimationOnDirection(Keys direction, Spritesheet spritesheet, string sheetname)
  {
    if (direction == Keys.W)
    {
      animatedSprite.spritesheet = spritesheet;
      animatedSprite.SetAnimation(sheetname + "_north");
    }
    else if (direction == Keys.A)
    {
      animatedSprite.spritesheet = spritesheet;
      animatedSprite.SetAnimation(sheetname + "_west");
    }
    else if (direction == Keys.S)
    {
      animatedSprite.spritesheet = spritesheet;
      animatedSprite.SetAnimation(sheetname + "_south");
    }
    else if (direction == Keys.D)
    {
      animatedSprite.spritesheet = spritesheet;
      animatedSprite.SetAnimation(sheetname + "_east");
    }
  }

  public void inputIdle()
  {
    IInputService input = ServiceLocator.GetService<IInputService>();

    ButtonStateIS left = input.GetButtonStateIS(MouseButtons.Left);

    if (left.isDown && !left.isRepeat)
    {
      state = "attack";
      return;
    }

    bool w = input.GetKeyStateIS(Microsoft.Xna.Framework.Input.Keys.W).isDown;
    bool a = input.GetKeyStateIS(Microsoft.Xna.Framework.Input.Keys.A).isDown;
    bool s = input.GetKeyStateIS(Microsoft.Xna.Framework.Input.Keys.S).isDown;
    bool d = input.GetKeyStateIS(Microsoft.Xna.Framework.Input.Keys.D).isDown;

    if (w)
    {
      firstDirection = Keys.W;
      facingDirection = Keys.W;
      animatedSprite.spritesheet = sheetIdle;
      animatedSprite.SetAnimation("walk_north");
    }
    else if (a)
    {
      firstDirection = Keys.A;
      facingDirection = Keys.A;
      animatedSprite.SetAnimation("walk_west");
    }
    else if (s)
    {
      firstDirection = Keys.S;
      facingDirection = Keys.S;
      animatedSprite.SetAnimation("walk_south");
    }
    else if (d)
    {
      firstDirection = Keys.D;
      facingDirection = Keys.D;
      animatedSprite.SetAnimation("walk_east");
    }
    else
    {
    }
  }

  public void inputWalk()
  {

  }

  public void inputAttack()
  {

  }

}

public abstract class State
{
  public abstract void OnEnter();
  public abstract void OnExit();
}