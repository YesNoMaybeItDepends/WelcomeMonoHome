using System;
using System.Diagnostics.CodeAnalysis;
using WelcomeMonoHome.Components;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class EntityManagerService : IEntityManagerService
{
  public List<Entity> Entities { get; set; }
  public List<Entity> EntitiesToAdd { get; set; }
  public List<Entity> EntitiesToRemove { get; set; }

  public EntityManagerService(List<Entity> entities, List<Entity> entitiesToAdd, List<Entity> entitiesToRemove)
  {
    Entities = entities;
    EntitiesToAdd = entitiesToAdd;
    EntitiesToRemove = entitiesToRemove;
  }

  public void UpdateEntities(GameTime gameTime)
  {
    // remove entities from _entities
    if (EntitiesToRemove.Count > 0)
    {
      foreach (Entity entity in EntitiesToRemove)
      {
        Entities.Remove(entity);
      }
      EntitiesToRemove.Clear();
    }

    // add new entities to _entities
    if (EntitiesToAdd.Count > 0)
    {
      Entities.AddRange(EntitiesToAdd);
      EntitiesToAdd.Clear();
    }

    // update entities
    foreach (Entity entity in Entities)
    {
      entity.Update(gameTime);
    }
  }

  public void AddEntity(Entity entity)
  {
    EntitiesToAdd.Add(entity);
  }

  public void RemoveEntity(Entity entity)
  {
    EntitiesToRemove.Add(entity);
  }

  public Entity Instantiate(Entity entity, Vector2 pos)
  {
    AddEntity(entity);
    return entity;
  }
}