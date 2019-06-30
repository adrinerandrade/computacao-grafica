using System;
using System.Timers;

namespace gcgcg
{
  public class MusicTimer
  {
    private Timer timer;
    private Runnable runnable;
    public MusicTimer(Music music, Runnable runnable)
    {
      timer = new Timer();
      timer.Interval = (60000f / music.bpm) / music.subdivision;
      timer.Elapsed += Execute;

      this.runnable = runnable;
    }
    public void Start()
    {
      timer.Enabled = true;
    }
    public void Cancel()
    {
      timer.Enabled = false;
    }
    private void Execute(Object source, System.Timers.ElapsedEventArgs e)
    {
      runnable();
    }
  }
}