using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WelcomeMonoHome.GUI;

public class GuiService : IGuiService
{
  public Dictionary<string, GuiConsole> consoles { get; set; }

  public GuiService()
  {
    consoles = new Dictionary<string, GuiConsole>();
  }

  public void NewConsole(string Console, Vector2 TopLeftOrigin, Color? Color, float? Transparency)
  {
    if (!consoles.ContainsKey(Console))
    {
      GuiConsole console = new GuiConsole(TopLeftOrigin, Color, Transparency);
      consoles[Console] = console;
    }

  }

  public void ConsoleWriteLine(string Console, string text)
  {
    if (consoles[Console] is GuiConsole console)
    {
      console.WriteLine(text);
    }
  }

  public void ClearConsole(string Console)
  {
    if (consoles[Console] is GuiConsole console)
    {
      console.Clear();
    }
  }
}