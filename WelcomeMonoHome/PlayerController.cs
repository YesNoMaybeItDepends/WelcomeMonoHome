using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

public class PlayerController
{
  CyclopsTroll player;
  IInputService input = ServiceLocator.GetService<IInputService>();

  public void Update()
  {
    // Get input
    KeyStateIS W;
    KeyStateIS A;
    KeyStateIS S;
    KeyStateIS D;

    W = input.GetKeyStateIS(Keys.W);
    A = input.GetKeyStateIS(Keys.A);
    S = input.GetKeyStateIS(Keys.S);
    D = input.GetKeyStateIS(Keys.D);

    List<KeyStateIS> wasdlist = new List<KeyStateIS>() { W, A, S, D };
    Dictionary<Keys, KeyStateIS> wasd = new Dictionary<Keys, KeyStateIS>()
    {
      { Keys.W, W },
      {Keys.A, A},
      {Keys.S, S},
      {Keys.D, D}
    };

    Keys firstDirection = default;
    Keys secondDirection = default;

    if (!input.GetKeyStateIS(firstDirection).isDown)
    {
      if (wasd[Keys.W].isDown)
      {
        firstDirection = Keys.W;
      }
      else if (wasd[Keys.A].isDown)
      {
        firstDirection = Keys.A;
      }
      else if (wasd[Keys.S].isDown)
      {
        firstDirection = Keys.S;
      }
      else if (wasd[Keys.D].isDown)
      {
        firstDirection = Keys.D;
      }
      else
      {
        firstDirection = default;
      }
    }





    if (firstDirection == default)
    {
      if (wasd[Keys.W].isDown)
      {
        firstDirection = Keys.W;
      }
      else if (wasd[Keys.A].isDown)
      {
        firstDirection = Keys.A;
      }
      else if (wasd[Keys.S].isDown)
      {
        firstDirection = Keys.S;
      }
      else if (wasd[Keys.D].isDown)
      {
        firstDirection = Keys.D;
      }
    }
    else if (input.GetKeyStateIS(firstDirection).isDown)
    {
      if (secondDirection == default)
      {
        foreach (KeyValuePair<Keys, KeyStateIS> entry in wasd)
        {
          if (entry.Key != firstDirection && entry.Value.isDown)
          {
            secondDirection = entry.Key;
          }
        }
      }
      else
      {
        if (!input.GetKeyStateIS(secondDirection).isDown)
        {
          secondDirection = default;
        }
      }
    }
    else
    {
      firstDirection = default;
    }
  }

  // public Keys ezmode(Dictionary<Keys, KeyStateIS> WASD)
  // {
  //   if (WASD[Keys.W].isDown)
  //   {
  //     return Keys.W;
  //   }
  //   else if (WASD)
  // }

  // public Keys WASDisdown(List<KeyStateIS> WASD)
  // {
  //   foreach (KeyStateIS input in WASD)
  //   {
  //     if (input.isDown)
  //     {
  //       return input.key;
  //     }
  //   }
  // }
}