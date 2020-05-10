using System;
using System.Diagnostics.CodeAnalysis;
using WelcomeMonoHome.Components;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class EntityManagerService : IEntityManagerService
{
  List<Entity> _entities;
  List<Entity> _entitiesToAdd;
  List<Entity> _entitiesToRemove;

  public EntityManagerService(List<Entity> entities, List<Entity> entitiesToAdd, List<Entity> entitiesToRemove)
  {
    _entities = new List<Entity>();
    _entitiesToAdd = new List<Entity>();
    _entitiesToRemove = new List<Entity>();
  }

  public void UpdateEntities(GameTime gametime)
  {
    // debug print _entities.count
    Console.WriteLine($"Gametime: {1 / (float)gametime.TotalGameTime.TotalSeconds} \n Scene._entities.length: {_entities.Count}");

    // remove entities from _entities
    if (_entitiesToRemove.Count > 0)
    {
      foreach (Entity entity in _entitiesToRemove)
      {
        _entities.Remove(entity);
      }
      _entitiesToRemove.Clear();
    }

    // add new entities to _entities
    if (_entitiesToAdd.Count > 0)
    {
      _entities.AddRange(_entitiesToAdd);
      _entitiesToAdd.Clear();
    }

    // update entities
    foreach (Entity entity in _entities)
    {
      entity.Update(gametime);
    }
  }

}