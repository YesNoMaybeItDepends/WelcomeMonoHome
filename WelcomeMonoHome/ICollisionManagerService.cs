public interface ICollisionManagerService
{
  void AddCollidable(Entity entity) { }
  void RemoveCollidable(Entity entity) { }
  void CheckCollision(Entity entity) { }
  void Update() { }
}