using System;
using System.Collections.Generic;

namespace gcgcg
{
  public class MusicPreProcessor
  {
    public static byte[][] PreProcessNotes(byte[][][] notes)
    {
      List<byte[]> processedNotes = new List<byte[]>();
      for (int i = 0; i < notes.Length; i++)
      {
        byte[][] subDivTime = notes[i];
        for (int y = 0; y < subDivTime.Length; y++)
        {
          byte[] divTimeNote = subDivTime[y];
          byte note = divTimeNote[0];
          byte duration = divTimeNote.Length == 2 ? divTimeNote[1] : (byte) 1;
          AddNote(processedNotes, i, note, duration);
        }
      }
      return processedNotes.ToArray();
    }
    private static void AddNote(List<byte[]> processedNotes, int time, byte note, byte duration)
    {
      AddNote(processedNotes, time, note, duration, 1);
    }
    private static void AddNote(List<byte[]> processedNotes, int time, byte note, byte duration, byte value)
    {
      byte[] divTime;
      if (time <= processedNotes.Count - 1)
      {
        divTime = processedNotes[time];
      } else
      {
        divTime = new byte[5];
        processedNotes.Add(divTime);
      }
      if (note != 0)
      {
        bool isAvailable = divTime[note - 1] == 0;
        if (isAvailable)
        {
          divTime[note - 1] = value;
          duration -= 1;
          if (duration > 0)
          {
            AddNote(processedNotes, ++time, note, duration, 2);
          }
        }
      }
    }
  }
}