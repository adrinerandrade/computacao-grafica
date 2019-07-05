using System;
using System.Diagnostics;
using System.Threading;

namespace gcgcg
{
  public class PreciseTimer
  {
    private bool isAlive = true;
    private int interval;
    private Runnable runnable;
    private Thread thread;
    public PreciseTimer(int interval, Runnable runnable)
    {
      // Console.WriteLine(interval);
      this.interval = interval;
      this.runnable = runnable;
    }
    public void Start()
    {
      Stopwatch timer = new Stopwatch();
      if (!isAlive) return;
      
      this.thread = new Thread(() => {
        while (this.isAlive)
        {
          timer.Start();
          runnable();
          Thread.Sleep(interval);
          timer.Stop();
          // Console.WriteLine(timer.ElapsedMilliseconds);
          timer.Reset();
        }
      });
      this.thread.Start();
    } 
    public void Stop()
    {
      this.isAlive = false;
    }
  }
}