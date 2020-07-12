using System;
using Microsoft.Xna.Framework.Input;

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
    inputService.OnMouseClickEvent += OnMouseClickHandler;
  }

  public void Unsubscribe()
  {
    inputService.OnMouseClickEvent -= OnMouseClickHandler;
  }


}