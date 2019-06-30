using System;
using System.Collections.Generic;

namespace gcgcg
{
  public class MusicExecution
  {
    private MusicTimer musicTimer;
    private NoteQueue noteQueue;
    private List<Runnable> beforeStartListeners = new List<Runnable>();
    private List<Consumer<byte[]>> onNoteListeners = new List<Consumer<byte[]>>();
    private List<Runnable> onStopListeners = new List<Runnable>();
    public MusicExecution(string musicName)
    {
      var music = MusicProvider.get(musicName);
      this.noteQueue = new NoteQueue(MusicPreProcessor.PreProcessNotes(music.notes));
      this.musicTimer = new MusicTimer(music, () => {
        if (noteQueue.hasNext())
        {
          foreach(var onNoteListener in onNoteListeners)
          {
          onNoteListener(noteQueue.next());
          }
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
      foreach(var beforeStartListener in beforeStartListeners)
      {
        beforeStartListener();
      }
      this.musicTimer.Start();
    }
    public void Cancel()
    {
      musicTimer.Cancel();
    }
    public void OnBeforeStart(Runnable runnable) {
      beforeStartListeners.Add(runnable);
    }
    public void OnNote(Consumer<byte[]> consumer) {
      onNoteListeners.Add(consumer);
    }
    public void OnStop(Runnable runnable) {
      onStopListeners.Add(runnable);
    }
  }
}