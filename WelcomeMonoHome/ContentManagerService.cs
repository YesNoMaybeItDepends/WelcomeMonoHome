using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

public class ContentManagerService : IContentManagerService
{
  public ContentManager _content { get; set; }

  public Dictionary<string, Texture2D> textures { get; set; }
  public Dictionary<string, SpriteFont> fonts { get; set; }

  public ContentManagerService(ContentManager content)
  {
    _content = content;
    textures = new Dictionary<string, Texture2D>();
    fonts = new Dictionary<string, SpriteFont>();
  }

  public void Initialize()
  {
    // common
    textures["ERROR"] = _content.Load<Texture2D>("ERROR");
    textures["pixel"] = _content.Load<Texture2D>("pixel");
    // rename MyFont to font
    fonts["MyFont"] = _content.Load<SpriteFont>("MyFont");
  }

  public Texture2D GetTexture(string name)
  {
    if (textures.ContainsKey(name) && textures[name] != null)
    {
      return textures[name];
    }
    return textures["ERROR"];
  }

  public void LoadTexture(string name)
  {
    textures[name] = _content.Load<Texture2D>(name);
  }

  public SpriteFont GetFont(string name)
  {
    return fonts[name];
    // TODO add error handling
  }

  public void LoadFont(string name)
  {
    fonts[name] = _content.Load<SpriteFont>(name);
  }

  public void UnloadContent()
  {
    _content.Unload();
  }

  public void Run()
  {

  }

}