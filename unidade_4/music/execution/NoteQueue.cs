using System;
using System.Collections.Concurrent;

namespace gcgcg
{
  public class NoteQueue
  {
    private ConcurrentQueue<byte[]> queue;
    public NoteQueue(byte[][] notes)
    {
      queue = new ConcurrentQueue<byte[]>();
      for (var i = 0; i < notes.Length; i++)
      {
        queue.Enqueue(notes[i]);
      }
    }
    public bool hasNext()
    {
      return this.queue.Count != 0;
    }
    public byte[] next()
    {
      return next(3);
    }
    private byte[] next(byte tryCount)
    {
      if (tryCount == 0)
      {
        throw new Exception("CQ: TryPeek failed when it should have succeeded");
      }
      byte[] result;
      if (!this.queue.TryDequeue(out result))
      {
         return next(--tryCount);
      } else
      {
        return result;
      }
    }
  }
}