using System;
using System.Diagnostics;
using System.Timers;

namespace gcgcg
{
  public class MusicTimer
  {
    public static readonly float GRAPHIC_PROGRESSION_RATE = 15;
    public float interval { get; }
    private Timer timer;
    private Timer graphicTimer;
    private Runnable bpmRunner;
    private Consumer<byte> graphicRunner;
    private int tickCount;
    public MusicTimer(Music music, Runnable bpmRunner, Consumer<byte> graphicRunner)
    {
      this.timer = new Timer();
      this.bpmRunner = bpmRunner;
      this.graphicRunner = graphicRunner;

      this.timer.Interval = (60000f / music.bpm) / music.subdivision;
      this.timer.Elapsed += Execute;
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
      StartGraphicProgression();
      bpmRunner();
    }
    private void StartGraphicProgression()
    {
      if (this.graphicTimer != null)
      {
        this.graphicTimer.Enabled = false;
        graphicRunner(100);
      }
      this.graphicTimer = new Timer();
      tickCount = 0;
      this.graphicTimer.Interval = timer.Interval / GRAPHIC_PROGRESSION_RATE;
      this.graphicTimer.Elapsed += ExecuteGraphicProgression;
      this.graphicTimer.Enabled = true;
      graphicRunner(0);
    }
    private void ExecuteGraphicProgression(Object source, System.Timers.ElapsedEventArgs e)
    {
      tickCount++;
      var progress = tickCount * (100 / GRAPHIC_PROGRESSION_RATE);
      if (progress > 0 && progress < 100)
      {
        graphicRunner((byte) progress);
      }
    }
  }
}