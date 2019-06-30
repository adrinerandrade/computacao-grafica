using System;
using System.Collections.Generic;

namespace gcgcg
{
  public class MusicExecution
  {
    private MusicTimer musicTimer;
    private NoteQueue noteQueue;
    private List<Runnable> onStopListeners = new List<Runnable>();
    public MusicExecution(string musicName)
    {
      var music = MusicProvider.get(musicName);
      this.noteQueue = new NoteQueue(MusicPreProcessor.PreProcessNotes(music.notes));
      this.musicTimer = new MusicTimer(music, () => {
        if (noteQueue.hasNext())
        {
          var tabNotes = noteQueue.next();
          Console.WriteLine("[{0}]", string.Join(", ", tabNotes));
        } else
        {
          this.musicTimer.Cancel();
          foreach(var onStopAction in onStopListeners)
          {
            onStopAction();
          }
        }
      });
    }
    public void Start()
    {
      this.musicTimer.Start();
    }
    public void Cancel()
    {
      musicTimer.Cancel();
    }
    public void onStop(Runnable runnable) {
      onStopListeners.Add(runnable);
    }
  }
}