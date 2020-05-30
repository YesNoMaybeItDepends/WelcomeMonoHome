public interface ISceneManagerService
{
  Scene currentScene { get; set; }

  void SetOrChangeScene(Scene NewScene) { }
}