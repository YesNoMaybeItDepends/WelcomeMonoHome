using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace WelcomeMonoHome.GUI
{
  public class GuiConsole
  {
    public List<TextBox> lines;
    public Vector2 topLeftOrigin;
    public Vector2 position;
    public float spacing = 20f;
    public int maxLines = 10;
    public RectanglePrimitive backGround;

    public GuiConsole(Vector2 TopLeftOrigin, Color? Color, float? Transparency)
    {
      topLeftOrigin = TopLeftOrigin;
      lines = new List<TextBox>();
      if (Color != null && Transparency != null)
      {
        backGround = new RectanglePrimitive(new Rectangle((int)topLeftOrigin.X, (int)topLeftOrigin.Y, 200, 200), (Color)Color, (float)Transparency);
        backGround.Instantiate();
      }
    }

    public void WriteLine(string text)
    {
      Vector2 linePos = Vector2.Zero;
      linePos.X = topLeftOrigin.X;
      linePos.Y = topLeftOrigin.Y + (lines.Count * spacing);
      TextBox line = new TextBox(text, linePos);

      lines.Add(line);
      if (lines.Count > maxLines)
      {
        // Console.WriteLine("====");
        // Console.WriteLine(lines.Count);
        // Console.WriteLine(lines[0].text);
        lines[0].Destroy();
        lines.RemoveAt(0);
        // Console.WriteLine(lines.Count);
        // Console.WriteLine(lines[0].text);
        // Console.WriteLine("====");
        foreach (TextBox consoleLine in lines)
        {
          // Console.WriteLine(consoleLine.text);
          // Console.WriteLine(lines.IndexOf(consoleLine));
          // Console.WriteLine(consoleLine.position);
          consoleLine.position = new Vector2(topLeftOrigin.X, topLeftOrigin.Y + (lines.IndexOf(consoleLine) * spacing));
          // Console.WriteLine(consoleLine.position);
          // Console.WriteLine("----");
        }
      }
    }

    public void Clear()
    {
      foreach (TextBox consoleLine in lines)
      {
        consoleLine.Destroy();
      }
      //lines.Clear();
    }

    public void Reset()
    {
      foreach (TextBox consoleLine in lines)
      {
        consoleLine.Destroy();
      }
      lines.Clear();
    }

    public void Draw()
    {

    }
  }
}