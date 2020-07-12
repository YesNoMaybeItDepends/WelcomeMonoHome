using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

public class InputService : IInputService
{
  Camera _camera;
  GraphicsDeviceManager _graphics;

  MouseState _lastMouseState;
  MouseState _mouseState;

  KeyboardState _lastKeyboardState;
  KeyboardState _keyboardState;

  public InputState leftButton;
  public InputState rightButton;
  public InputState middleButton;
  public ScrollWheelState scrollWheel;

  public Dictionary<Keys, InputState> keys;

  public int mouseWorldX;
  public int mouseWorldY;

  public int mouseCameraX;
  public int mouseCameraY;

  public InputService(Camera camera)
  {
    _camera = camera;
    keys = new Dictionary<Keys, InputState>();
  }

  public void GetMouseClick()
  {
    MouseState mouse = Mouse.GetState();
    //_camera.WorldToScreen(ass.)
    // RAYCAST
    Rectangle mouserect = new Rectangle(mouse.X, mouse.Y, 1, 1);
  }

  public Vector2 GetMouseWorldPos()
  {
    Point point = Mouse.GetState().Position;
    Vector2 pos = Vector2.Zero;
    pos.X = point.X;
    pos.Y = point.Y;
    return _camera.ScreenToWorld(pos);
  }

  public Vector2 GetMouseCameraPos()
  {
    Point point = Mouse.GetState().Position;
    Vector2 pos = Vector2.Zero;
    pos.X = point.X;
    pos.Y = point.Y;
    return pos;
  }

  // EVENT BASED SYSTEM
  public delegate void OnMouseClick(object sender, MouseState MouseState);
  public event OnMouseClick OnMouseClickEvent;

  // ACTION BASED SYSTEM
  public Action OnMouseClickAction;

  // EVENT BASED UPDATE
  public void Update(GameTime gameTime)
  {
    // If it's the first frame we don't have any input at all
    if (_mouseState != null)
    {
      _lastMouseState = _mouseState;
      _lastKeyboardState = _keyboardState;
    }

    _mouseState = Mouse.GetState();
    _keyboardState = Keyboard.GetState();

    if (_mouseState != _lastMouseState || _keyboardState != _lastKeyboardState)
    {
      OnMouseClickEvent(this, _mouseState);
    }
  }

  // ACTION BASED UPDATE
  public void ActionUpdate(GameTime gameTime)
  {
    // If it's the first frame we don't have any input at all
    if (_mouseState != null && _keyboardState != null)
    {
      _lastMouseState = _mouseState;
      _lastKeyboardState = _keyboardState;
    }

    // update input state
    _mouseState = Mouse.GetState();
    _keyboardState = Keyboard.GetState();

    // update buttons state
    GetButtonsState();

    // update keys state

    if (_mouseState != _lastMouseState)
    {
      OnMouseClickAction();
    }
  }

  public InputState GetKeyState(Keys key)
  {
    bool isDown;
    bool isRepeat;

    isDown = _keyboardState.IsKeyDown(key) ? true : false;
    isRepeat = _keyboardState.IsKeyDown(key) == _lastKeyboardState.IsKeyDown(key) ? true : false;

    InputState inputState = new InputState(isDown, isRepeat);
    return inputState;
  }

  public InputState GetButtonState(MouseButtons mouseButton)
  {

    // ButtonState is a struct and it can't be null
    // Switches are a meme, so we must assign it something
    // It will always be reassigned, so it's actually ok
    ButtonState button = default;
    ButtonState lastButton = default;

    switch (mouseButton)
    {
      case MouseButtons.Left:
        {
          button = _mouseState.LeftButton;
          lastButton = _lastMouseState.LeftButton;
          break;
        }
      case MouseButtons.Right:
        {
          button = _mouseState.RightButton;
          lastButton = _lastMouseState.RightButton;
          break;
        }
      case MouseButtons.Middle:
        {
          button = _mouseState.MiddleButton;
          lastButton = _lastMouseState.MiddleButton;
          break;
        }
    }

    bool isDown;
    bool isRepeat;

    isDown = (button == ButtonState.Pressed) ? true : false;
    isRepeat = (button == lastButton) ? true : false;

    InputState buttonState = new InputState(isDown, isRepeat);
    return buttonState;
  }
}

public enum MouseButtons
{
  Left,
  Right,
  Middle
}

public struct InputState
{
  public bool isDown;
  public bool isRepeat;

  public InputState(bool isdown, bool isrepeat)
  {
    isDown = isdown;
    isRepeat = isrepeat;
  }
}

public struct ScrollWheelState
{

}
