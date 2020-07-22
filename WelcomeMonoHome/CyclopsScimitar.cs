using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class CyclopsScimitar : Entity
{
  Texture2D textureScimitar;
  string textureName = "cyclops_scimitar_sheet";

  Spritesheet sheetScimitar;

  AnimatedSprite animatedSprite;

  List<string> animations;
  int acounter = 0;

  CyclopsTroll player;

  float speed = 100f;

  string state = "idle";

  public CyclopsScimitar()
  {
    transform = new Transform(new Vector2(400, 400));

    textureScimitar = ServiceLocator.GetService<IContentManagerService>().GetTexture(textureName);

    //sheetScimitar = new Spritesheet(textureScimitar, 13, 10);
    sheetScimitar = new Spritesheet(textureScimitar, 13, 10);

    sheetScimitar.AddAnimation("roar", 1, 1, 1, 10);
    sheetScimitar.AddAnimation("idle_south", 2, 1, 1, 4);
    sheetScimitar.AddAnimation("idle_west", 3, 1, 1, 4);
    sheetScimitar.AddAnimation("idle_north", 4, 1, 1, 4);
    sheetScimitar.AddAnimation("idle_east", 5, 1, 1, 4);
    sheetScimitar.AddAnimation("walk_south", 6, 1, 1, 4);
    sheetScimitar.AddAnimation("walk_west", 7, 1, 1, 4);
    sheetScimitar.AddAnimation("walk_north", 8, 1, 1, 4);
    sheetScimitar.AddAnimation("walk_east", 9, 1, 1, 4);
    sheetScimitar.AddAnimation("attack_south", 10, 1, 1, 3);
    sheetScimitar.AddAnimation("attack_west", 11, 1, 1, 3);
    sheetScimitar.AddAnimation("attack_north", 12, 1, 1, 3);
    sheetScimitar.AddAnimation("attack_east", 13, 1, 1, 3);

    animatedSprite = new AnimatedSprite(sheetScimitar);
    animatedSprite.originOffsetPercent = new Vector2(0.5f, 0.5f);
    animatedSprite.spritesheet = sheetScimitar;
    animatedSprite.SetAnimation("idle_south");
    AddComponent(animatedSprite);

    animations = new List<String>();
    foreach (KeyValuePair<string, Animation> a in sheetScimitar.animations)
    {
      animations.Add(a.Key);
    }
  }

  public void SetPlayer(CyclopsTroll Player)
  {
    player = Player;
  }

  public override void Update(GameTime gameTime)
  {
    if (state == "attack" && !animatedSprite.animationFinished)
    {
      return;
    }
    else
    {
      state = "idle";
    }

    Vector2 direction = player.transform.position - transform.position;
    float x = Math.Abs(direction.X);
    float y = Math.Abs(direction.Y);
    float largest = Math.Max(x, y);

    Console.WriteLine("Direction: " + direction);
    Console.WriteLine("Largest: " + largest);

    if (direction.X != direction.Y)
    {
      if (largest == x)
      {
        if (direction.X > 0)
        {
          if (largest <= 40)
          {
            state = "attack";
            animatedSprite.SetAnimation("attack_east");
          }
          else
          {
            animatedSprite.SetAnimation("walk_east");
          }
        }
        else if (direction.X < 0)
        {
          if (largest <= 40)
          {
            state = "attack";
            animatedSprite.SetAnimation("attack_west");
          }
          else
          {
            animatedSprite.SetAnimation("walk_west");
          }
        }
      }
      else if (largest == y)
      {
        if (direction.Y > 0)
        {
          if (largest <= 40)
          {
            state = "attack";
            animatedSprite.SetAnimation("attack_south");
          }
          else
          {
            animatedSprite.SetAnimation("walk_south");
          }
        }
        else if (direction.Y < 0)
        {
          if (largest <= 40)
          {
            state = "attack";
            animatedSprite.SetAnimation("attack_north");
          }
          else
          {
            animatedSprite.SetAnimation("walk_north");
          }
        }
      }
    }

    if (largest >= 40)
    {
      direction = Vector2.Normalize(direction);
      transform.position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
  }
}




// if (direction.X != direction.Y)
// {
//   if (largest == x)
//   {
//     Console.WriteLine("Largest: X");
//     if (direction.X > 0 && animatedSprite.GetAnimation() != sheetScimitar.animations["walk_east"])
//     {
//       if (largest <= 40 && animatedSprite.GetAnimation() != sheetScimitar.animations["attack_east"])
//       {
//         animatedSprite.SetAnimation("attack_east");
//       }
//       animatedSprite.SetAnimation("walk_east");
//     }
//     else if (direction.X < 0 && animatedSprite.GetAnimation() != sheetScimitar.animations["walk_west"])
//     {
//       animatedSprite.SetAnimation("walk_west");
//     }
//   }
//   else if (largest == y)
//   {
//     Console.WriteLine("Largest: Y");
//     if (direction.Y > 0 && animatedSprite.GetAnimation() != sheetScimitar.animations["walk_south"])
//     {
//       animatedSprite.SetAnimation("walk_south");
//     }
//     else if (direction.Y < 0 && animatedSprite.GetAnimation() != sheetScimitar.animations["walk_north"])
//     {
//       animatedSprite.SetAnimation("walk_north");
//     }
//   }
// }
