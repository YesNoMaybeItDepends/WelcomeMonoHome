using System;
using System.Collections.Generic;

public class CollisionManagerService : ICollisionManagerService
{
  List<Entity> collidableEntities;
  List<Entity> entitiesToRemove;
  List<Entity> entitiesToAdd;

  public CollisionManagerService()
  {
    collidableEntities = new List<Entity>();
    entitiesToRemove = new List<Entity>();
    entitiesToAdd = new List<Entity>();
  }

  public void AddCollidable(Entity entity)
  {
    entitiesToAdd.Add(entity);
  }

  public void RemoveCollidable(Entity entity)
  {
    entitiesToRemove.Add(entity);
  }

  public void CheckCollision(Entity entity)
  {
    foreach (Entity e in collidableEntities)
    {
      if (entity != e && entity.colRectangle.Intersects(e.colRectangle) && !entity.alreadyCollidedWith.Contains(e))
      {
        // set both entities as already having collided with eachother this frame
        entity.alreadyCollidedWith.Add(e);
        e.alreadyCollidedWith.Add(entity);

        entity.OnCollision(e);
        e.OnCollision(entity);
      }
    }
  }

  public void Update()
  {
    // remove entities from _entities
    if (entitiesToRemove.Count > 0)
    {
      foreach (Entity entity in entitiesToRemove)
      {
        collidableEntities.Remove(entity);
      }
      entitiesToRemove.Clear();
    }

    // add new entities to _entities
    if (entitiesToAdd.Count > 0)
    {
      collidableEntities.AddRange(entitiesToAdd);
      entitiesToAdd.Clear();
    }

    // update collision rectangle
    // TODO AWFUL, change how this works
    if (collidableEntities.Count > 1)
    {
      foreach (Entity e in collidableEntities)
      {
        e.colRectangle = e.sprite.GetSpriteRectangle();
      }
    }

    // check collisions
    if (collidableEntities.Count > 1)
    {
      foreach (Entity e in collidableEntities)
      {
        CheckCollision(e);
      }

    }

    // reset alreadyCollidedWith 
    if (collidableEntities.Count > 0)
    {
      foreach (Entity entity in collidableEntities)
      {
        entity.alreadyCollidedWith.Clear();
      }
    }
  }

}