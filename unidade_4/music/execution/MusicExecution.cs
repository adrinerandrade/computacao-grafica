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
    private List<Consumer<byte>> onUpdateRenderingListeners = new List<Consumer<byte>>();
    private int musicEndDelay = GuitarTab.TABS_SIZE;
    public MusicExecution(string musicName)
    {
      var music = MusicProvider.get(musicName);
      this.noteQueue = new NoteQueue(MusicPreProcessor.PreProcessNotes(music.notes));
      this.musicTimer = new MusicTimer(music, () => {
        if (noteQueue.hasNext())
        {
          executeNotes(noteQueue.next());
        } else if (musicEndDelay > 0)
        {
          musicEndDelay--;
          executeNotes(new byte[5]);
        } else {
          this.musicTimer.Cancel();
          foreach(var onStopAction in onStopListeners)
          {
            onStopAction();
          }
        }
      }, progress => {
        foreach(var onUpdateRenderingListener in onUpdateRenderingListeners)
        {
          onUpdateRenderingListener(progress);
        }
      });
    }
    private void executeNotes(byte[] notes)
    {
      foreach(var onNoteListener in onNoteListeners)
      {
        onNoteListener(notes);
      }
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
    public void onUpdateRendering(Consumer<byte> runnable)
    {
      onUpdateRenderingListeners.Add(runnable);
    }
  }
}