using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Camera
{
  public float Zoom;
  public Matrix _transform;
  public Vector2 _pos;
  public float _rotation;
  GraphicsDevice _graphicsDevice;

  int screenWidth;
  int screenHeight;

  public Camera(int ScreenWidth, int ScreenHeight)
  {
    Zoom = 1.0f;
    _rotation = 0.0f;
    _pos = Vector2.Zero;

    screenWidth = ScreenWidth;
    screenHeight = ScreenHeight;
  }



  public Matrix get_transformation(GraphicsDevice graphicsDevice)
  {
    _transform = Matrix.CreateTranslation(_pos.X, _pos.Y, 0);
    _transform = Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
                                          Matrix.CreateRotationZ(_rotation) *
                                          Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                          Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width * 0.5f, graphicsDevice.Viewport.Height * 0.5f, 0));
    return _transform;
  }

  public Vector2 ScreenToWorld(Vector2 ScreenPoint)
  {
    Vector2 WorldPoint;
    WorldPoint = Vector2.Transform(ScreenPoint, Matrix.Invert(_transform));
    return WorldPoint;
  }

  public Vector2 WorldToScreen(Vector2 WorldPoint)
  {
    Vector2 ScreenPoint;
    ScreenPoint = Vector2.Transform(WorldPoint, _transform);
    return ScreenPoint;
  }

  public Rectangle GetRenderedWorld()
  {
    Rectangle CameraWorldRect = new Rectangle(
                  (int)(_pos.X - ((screenWidth / 2) / Zoom)),
                  (int)(_pos.Y - ((screenHeight / 2) / Zoom)),
                  (int)(screenWidth / Zoom),
                  (int)(screenHeight / Zoom));

    return CameraWorldRect;
  }

  public Rectangle GetCameraFrame()
  {
    float width = screenWidth * _pos.X;
    float height = screenHeight * _pos.Y;
    float meme = MathHelper.Max(width, height);
    float x = _pos.X - (meme / 2);
    float y = _pos.Y - (meme / 2);
    Rectangle rectangle = new Rectangle((int)x, (int)y, (int)width, (int)height);
    return rectangle;
    //return new Rectangle(0, 0, (int)((_pos.X * 2f) * Zoom), (int)((_pos.Y * 2f) * Zoom));
  }
}