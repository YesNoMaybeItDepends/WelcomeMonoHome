using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

public class SoundService : ISoundService
{
  Song Currentsong;

  public SoundService()
  {

  }

  public void PlaySong(string SongName)
  {
    Currentsong = ServiceLocator.GetService<IContentManagerService>().GetSong("song");
    MediaPlayer.Play(Currentsong);
  }

  public void Update()
  {

  }
}