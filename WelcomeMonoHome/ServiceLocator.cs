using System;
using System.Collections.Generic;

// Service implements Iservice
// Dictionary <object, object>
// Dictionary <Iservice, Service>
// Dictionary.add(typeof(Iservice), Service)
// return (T)Dictionary[typeof(T)]

// ! TODO try to make it like:
// ! Interface IService
// ! Interface IEntityManagerService : IService
// ! class EntityManagerService : IEntityManagerService
// ! Dictionary <IService, object>
// ! Set(IService, Service)
// ! Iservice Get(Iservice)

public static class ServiceLocator
{

  // <IService, Service>
  private static Dictionary<object, object> _services;

  public static void SetService<T>(object Service)
  {
    // Initialize
    if (_services == null)
    {
      _services = new Dictionary<object, object>();
    }

    // Set Service
    _services.Add(typeof(T), Service);
  }

  public static T GetService<T>()
  {
    if (_services != null && _services.ContainsKey(typeof(T)))
    {
      return (T)_services[typeof(T)];
    }
    return default(T);
  }
}