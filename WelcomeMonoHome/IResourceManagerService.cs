using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

public interface IResourceManagerService
{
  ContentManager _content { get; set; }

  Dictionary<string, Texture2D> textures { get; set; }
  Dictionary<string, SpriteFont> fonts { get; set; }


  void Initialize();
  Texture2D GetTexture(string name);
  void LoadTexture(string name);
  void UnloadContent(string name);
  void Run();
}