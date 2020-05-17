public interface ISceneManagerService
{
  Scene scene { get; set; }

  void SetScene(Scene scene);
  Scene GetScene();
}