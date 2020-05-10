using System;
using System.Collections.Generic;
using System.Text;
namespace Client
{
  // interface A
  public interface IServiceA
  {
    void Execute();
  }

  // service A
  public class ServiceA : IServiceA
  {
    public void Execute()
    {
      Console.WriteLine("A service called.");
    }
  }

  // Interface B
  public interface IServiceB
  {
    void Execute();
  }

  // Service B
  public class ServiceB : IServiceB
  {
    public void Execute()
    {
      Console.WriteLine("B service called.");
    }
  }

  // ?
  public interface IService
  {
    T GetService<T>();
  }

  // Service Locator
  public class ServiceLocator : IService
  {
    public Dictionary<object, object> servicecontainer = null;

    // Constructor
    public ServiceLocator()
    {
      servicecontainer = new Dictionary<object, object>();
      servicecontainer.Add(typeof(IServiceA), new ServiceA());
      servicecontainer.Add(typeof(IServiceB), new ServiceB());
    }

    // Get service
    public T GetService<T>()
    {
      try
      {
        return (T)servicecontainer[typeof(T)];
      }
      catch (Exception ex)
      {
        throw new NotImplementedException("Service not available.");
      }
    }
  }

  // Program
  class mememe
  {
    static void meme(string[] args)
    {
      ServiceLocator loc = new ServiceLocator();
      IServiceA Aservice = loc.GetService<IServiceA>();
      Aservice.Execute();

      IServiceB Bservice = loc.GetService<IServiceB>();
      Bservice.Execute();

      Console.ReadLine();
    }
  }
}

