using System;
using System.Diagnostics.CodeAnalysis;
using WelcomeMonoHome.Components;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class EntityManagerService : IEntityManagerService
{
  public List<Entity> entities { get; set; }
  public List<Entity> entitiesToAdd { get; set; }
  public List<Entity> entitiesToRemove { get; set; }

  public EntityManagerService()
  {
    entities = new List<Entity>();
    entitiesToAdd = new List<Entity>();
    entitiesToRemove = new List<Entity>();
  }

  public void UpdateEntities(GameTime gameTime)
  {
    // remove entities from _entities
    if (entitiesToRemove.Count > 0)
    {
      foreach (Entity entity in entitiesToRemove)
      {
        entities.Remove(entity);
      }
      entitiesToRemove.Clear();
    }

    // add new entities to _entities
    if (entitiesToAdd.Count > 0)
    {
      entities.AddRange(entitiesToAdd);
      entitiesToAdd.Clear();
    }

    // update entities
    foreach (Entity entity in entities)
    {
      entity.Update(gameTime);
    }
  }

  public void AddEntity(Entity entity)
  {
    entitiesToAdd.Add(entity);
  }

  public void RemoveEntity(Entity entity)
  {
    // ServiceLocator.GetService<IrendererService>().RemoveRenderable(entity.sprite);
    // ServiceLocator.GetService<ICollisionManagerService>().RemoveCollidable(entity);
    entitiesToRemove.Add(entity);
  }

  // ! TODO move this to entity.instantiate() as well
  public Entity Instantiate(Entity entity, Vector2 pos)
  {
    AddEntity(entity);
    return entity;
  }
}