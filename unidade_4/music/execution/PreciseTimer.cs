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
      this.interval = interval;
      this.runnable = runnable;
    }
    public void Start()
    {
      if (!isAlive) return;
      
      this.thread = new Thread(() => {
        while (this.isAlive)
        {
          runnable();
          Thread.Sleep(interval);
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