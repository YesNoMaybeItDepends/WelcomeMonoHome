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
    animatedSprite.SetAnimation("attack_south");
    AddComponent(animatedSprite);

    animations = new List<String>();
    foreach (KeyValuePair<string, Animation> a in sheetScimitar.animations)
    {
      animations.Add(a.Key);
    }
  }

  public override void Update(GameTime gameTime)
  {
    IInputService input = ServiceLocator.GetService<IInputService>();
    ButtonStateIS left = input.GetButtonStateIS(MouseButtons.Left);
    if (left.isDown && !left.isRepeat)
    {
      Console.WriteLine(animations.Count);
      Console.WriteLine("Counter; " + acounter);
      if (acounter != animations.Count)
      {
        animatedSprite.SetAnimation(animations[acounter]);
        //acounter++;
      }
      else
      {
        acounter = 0;
        animatedSprite.SetAnimation(animations[acounter]);
      }
    }
  }
}