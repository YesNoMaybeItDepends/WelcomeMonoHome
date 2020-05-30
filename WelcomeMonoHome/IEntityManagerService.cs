using System.Collections.Generic;
using WelcomeMonoHome.Components;
using Microsoft.Xna.Framework;

public interface IEntityManagerService : IService
{
  List<Entity> entities { get; set; }
  List<Entity> entitiesToAdd { get; set; }
  List<Entity> entitiesToRemove { get; set; }

  void AddEntity(Entity entity);
  void RemoveEntity(Entity entity);
  void UpdateEntities(GameTime gameTime);
}