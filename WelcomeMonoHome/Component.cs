using System;

public class Component
{
  public Entity parent;
  public virtual void Update()
  {

  }

  public virtual void Instantiate()
  {

  }

  public virtual void Destroy()
  {

  }

  public void RemoveFromParent()
  {
    Console.WriteLine("Not implemented");
  }
}