using System;

namespace WelcomeMonoHome
{
  public static class Program
  {
    [STAThread]
    static void Main()
    {
      using (var game = new WelcomeMonoHome())
        game.Run();
    }
  }
}
