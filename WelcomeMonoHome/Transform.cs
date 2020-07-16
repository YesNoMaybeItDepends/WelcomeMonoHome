using Microsoft.Xna.Framework;
using WelcomeMonoHome.Components;

public class Transform : Component
{
  private Vector2 _position = new Vector2(0, 0);
  private Vector2 _scale = new Vector2(1, 1);

  // float pos_x;
  // float pos_y;

  // float scale_x;
  // float scale_y;

  public Transform(Vector2 Position)
  {
    position = Position;
  }

  public static Transform Zero
  {
    get
    {
      return new Transform(new Vector2(0, 0));
    }
  }

  public Vector2 position
  {
    get
    {
      return _position;
    }
    set
    {
      _position = value;
    }
  }

  public Vector2 scale
  {
    get
    {
      return _scale;
    }
    set
    {
      _scale = value;
    }
  }

  public float rotation;
}