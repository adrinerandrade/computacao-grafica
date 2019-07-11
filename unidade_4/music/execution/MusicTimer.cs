using System;
using System.Diagnostics;
using System.Threading;

namespace gcgcg
{
  public class MusicTimer
  {
    public static readonly int GRAPHIC_PROGRESSION_RATE = 10;
    private PreciseTimer timer;
    private Runnable bpmRunner;
    private Consumer<byte> graphicRunner;
    private float noteInterval;
    private float graphicInterval;
    private int tickCount;
    private int musicDelay;

    private Process process;
    public static Thread threadMusic { get; set; }
    public MusicTimer(Music music, Runnable bpmRunner, Consumer<byte> graphicRunner)
    {
      this.musicDelay = music.delay;
      // this.ExecuteMusic(music.mp3);
      this.noteInterval = (float) ((60000f / music.bpm) / music.subdivision);
      this.graphicInterval = (float) (noteInterval / GRAPHIC_PROGRESSION_RATE);

      this.bpmRunner = bpmRunner;
      this.graphicRunner = graphicRunner;
      this.timer = new PreciseTimer((int) graphicInterval, Execute);
    }
    public void ExecuteMusic(String Music)
    {
      process = new System.Diagnostics.Process();
      process.StartInfo.FileName = "/bin/bash";
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.Arguments = "-c \" cd player && node player.js --music=../musicRepository/" + Music + " \"";
    }

    public void MusicPlay() {
      Thread.Sleep(this.musicDelay);
      process.Start();
      while (!process.StandardOutput.EndOfStream)
      {
        process.StandardOutput.ReadLine();
      }
      this.process.Dispose();
    }
    public void Start()
    {
      tickCount = GRAPHIC_PROGRESSION_RATE;
      this.timer.Start();
      // threadMusic = new Thread(this.MusicPlay);
      // threadMusic.IsBackground = true;
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