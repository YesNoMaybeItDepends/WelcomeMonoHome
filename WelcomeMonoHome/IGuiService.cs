using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WelcomeMonoHome.GUI;

public interface IGuiService
{
  Dictionary<string, GuiConsole> consoles { get; set; }
  void NewConsole(string Console, Vector2 TopLeftOrigin, Color? Color, float? Transparency);
  void ConsoleWriteLine(string Console, string text);
}