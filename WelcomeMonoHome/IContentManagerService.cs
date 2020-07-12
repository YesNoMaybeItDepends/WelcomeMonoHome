using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

public interface IContentManagerService
{
  ContentManager _content { get; set; }

  Dictionary<string, Texture2D> textures { get; set; }
  Dictionary<string, SpriteFont> fonts { get; set; }


  void Initialize();
  void UnloadContent();
  void Run();

  Texture2D GetTexture(string name);
  SpriteFont GetFont(string name);

  void LoadTexture(string name);
  void LoadFont(string name);
  void LoadSoundEffect(string name);
  void LoadSong(string name);
  SoundEffect GetSoundEffect(string name);
  Song GetSong(string name);
}