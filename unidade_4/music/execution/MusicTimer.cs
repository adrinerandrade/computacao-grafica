using System;
using System.Diagnostics;
using System.Timers;

namespace gcgcg
{
  public class MusicTimer
  {
    public static readonly int GRAPHIC_PROGRESSION_RATE = 20;
    private PreciseTimer timer;
    private Runnable bpmRunner;
    private Consumer<byte> graphicRunner;
    private float noteInterval;
    private float graphicInterval;
    private int tickCount;
    public MusicTimer(Music music, Runnable bpmRunner, Consumer<byte> graphicRunner)
    {
      this.noteInterval = (float) ((60000f / music.bpm) / music.subdivision);
      this.graphicInterval = (float) (noteInterval / GRAPHIC_PROGRESSION_RATE);

      this.bpmRunner = bpmRunner;
      this.graphicRunner = graphicRunner;
      this.timer = new PreciseTimer((int) graphicInterval, Execute);
    }
    public void Start()
    {
      tickCount = GRAPHIC_PROGRESSION_RATE;
      this.timer.Start();
    }
    public void Cancel()
    {
      this.timer.Stop();
    }  
    private void Execute()
    {
      var actualProgression = this.graphicInterval * this.tickCount;
      bool reachedNoteInterval = actualProgression >= this.noteInterval;
      if (reachedNoteInterval)
      {
        bpmRunner();
        this.tickCount = 0;
      }
      var progress = tickCount * (100 / GRAPHIC_PROGRESSION_RATE);
      graphicRunner((byte) progress);
      tickCount++;
    }
  }
}