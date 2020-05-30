using System.Collections.Generic;

public class SceneManagerService : ISceneManagerService
{
  public Scene currentScene { get; set; }

  public SceneManagerService()
  {

  }

  public void StartScene()
  {

  }

  public void EndScene()
  {

  }

  public void SetOrChangeScene(Scene NewScene)
  {
    if (currentScene == null)
    {
      currentScene = NewScene;
      //NewScene.Start();
    }
    else
    {
      //currentScene.End();
      currentScene = NewScene;
      //currentScene.Start();
    }
  }

  public void SetCurrentScene(Scene Scene)
  {
  }



  public void RemoveScene(Scene Scene)
  {

  }

  public Scene GetScene()
  {
    return currentScene;
  }
}