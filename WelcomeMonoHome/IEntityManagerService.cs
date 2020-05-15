using System.Collections.Generic;
using WelcomeMonoHome.Components;
using Microsoft.Xna.Framework;

public interface IEntityManagerService : IService
{
  List<Entity> Entities { get; set; }
  List<Entity> EntitiesToAdd { get; set; }
  List<Entity> EntitiesToRemove { get; set; }

  void AddEntity(Entity entity);
  void RemoveEntity(Entity entity);
  void UpdateEntities(GameTime gameTime);
}