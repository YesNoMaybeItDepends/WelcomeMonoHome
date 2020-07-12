using System;
using Microsoft.Xna.Framework.Input;

[Obsolete("we pollin' now")]
public class Input
{
  IInputService inputService;

  public Action OnMouseClickAction;

  public Input()
  {
    inputService = ServiceLocator.GetService<IInputService>();
    Subscribe();
  }

  public void OnMouseClickHandler(object sender, MouseState mouseState)
  {
    if (OnMouseClickAction != null)
    {
      OnMouseClickAction();
    }
    else
    {
      Console.WriteLine("Class.Input.OnMouseClickAction not implemented");
    }
  }

  public void Subscribe()
  {

  }

  public void Unsubscribe()
  {

  }


}