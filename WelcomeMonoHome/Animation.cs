using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class Animation
{
  string name;
  Spritesheet spritesheet;

  // starting point
  public int row;
  public int column;

  // total number
  public int rows;
  public int columns;

  // rows * columns
  public int length;

  public List<Rectangle> rectangles = new List<Rectangle>();

  public Animation(string Name, Spritesheet Spritesheet, int Row, int Column, int Rows, int Columns)
  {
    name = Name;
    spritesheet = Spritesheet;
    row = Row;
    column = Column;
    rows = Rows;
    columns = Columns;
    length = rows * columns;

    for (int r = 0; r <= rows; r++)
    {
      for (int c = 0; c <= columns; c++)
      {
        rectangles.Add(new Rectangle(
          spritesheet.cellWidth * (c + column),
          spritesheet.cellHeight * (r + row),
          spritesheet.cellWidth,
          spritesheet.cellHeight
        ));
      }
    }
  }
}