using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

public class ResourceManagerService : IResourceManagerService
{
  public ContentManager _content { get; set; }

  public Dictionary<string, Texture2D> textures { get; set; }
  public Dictionary<string, SpriteFont> fonts { get; set; }

  public ResourceManagerService(ContentManager content)
  {
    _content = content;
    textures = new Dictionary<string, Texture2D>();
    fonts = new Dictionary<string, SpriteFont>();
  }

  public void Initialize()
  {
    // common
    textures["ERROR"] = _content.Load<Texture2D>("ERROR");
    fonts["MyFont"] = _content.Load<SpriteFont>("MyFont");

    // level specific
    textures["BBEG_ok_mini"] = _content.Load<Texture2D>("BBEG_ok_mini");
    textures["boolet"] = _content.Load<Texture2D>("boolet");
    textures["Hillarious_mini"] = _content.Load<Texture2D>("Hillarious_mini");
    textures["pixel"] = _content.Load<Texture2D>("pixel");
  }

  public Texture2D GetTexture(string name)
  {
    if (textures[name] != null)
    {
      return textures[name];
    }
    return textures["ERROR"];
  }

  public void LoadTexture(string name)
  {
    textures[name] = _content.Load<Texture2D>(name);
  }

  public void UnloadContent(string name)
  {
    _content.Unload();
  }

  public void Run()
  {

  }

}