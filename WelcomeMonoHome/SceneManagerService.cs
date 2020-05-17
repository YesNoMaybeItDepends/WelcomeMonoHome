public class SceneManagerService : ISceneManagerService
{
  public Scene scene { get; set; }

  public SceneManagerService(Scene Scene)
  {
    scene = Scene;
  }

  public void SetScene(Scene Scene)
  {
    scene = Scene;
  }

  public Scene GetScene()
  {
    return scene;
  }
}