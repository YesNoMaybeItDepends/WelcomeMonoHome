using System;
using Microsoft.Xna.Framework;

namespace WelcomeMonoHome
{
  public static class Program
  {
    [STAThread]
    static void Main()
    {
      using (Game game = new WelcomeMonoHome())
        game.Run();
    }
  }
}
