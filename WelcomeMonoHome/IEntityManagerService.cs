using System.Collections.Generic;
using WelcomeMonoHome.Components;

public interface IEntityManagerService : IService
{
  List<Entity> Entities { get; set; }
  List<Entity> EntitiesToAdd { get; set; }
  List<Entity> EntitiesToRemove { get; set; }

  public void AddEntity(Entity entity);
  public void RemoveEntity(Entity entity);
}