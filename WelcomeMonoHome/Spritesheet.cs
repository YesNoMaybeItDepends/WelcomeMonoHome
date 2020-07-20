using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

public class Spritesheet
{
  public Texture2D texture;

  public int rows;
  public int columns;

  public int cellWidth;
  public int cellHeight;

  public Dictionary<string, Animation> animations;

  public Spritesheet(Texture2D Texture, int Rows, int Columns)
  {
    texture = Texture;

    rows = Rows;
    columns = Columns;

    cellWidth = texture.Width / columns;
    cellHeight = texture.Height / rows;

    animations = new Dictionary<string, Animation>();
  }

  public Spritesheet(Texture2D Texture, int Rows, int Columns, int CellWidth, int CellHeight)
  {
    texture = Texture;

    rows = Rows;
    columns = Columns;

    cellWidth = CellWidth;
    cellHeight = CellHeight;

    animations = new Dictionary<string, Animation>();
  }

  public void AddAnimation(string Name, int Row, int Column, int Rows, int Columns)
  {
    Animation animation = new Animation(Name, this, Row, Column, Rows, Columns);
    animations.Add(Name, animation);
  }

}