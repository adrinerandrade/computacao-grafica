using System;

namespace gcgcg
{
  internal class NoteMatcherController
  {
    private Tab currentTab;
    private bool[] isNotePressed = new bool[] { false, false, false, false, false };
    private bool[] pressingNoteControl = new bool[] { false, false, false, false, false };
    public void setCurrentTab(Tab tab)
    {
      if (currentTab != null)
      {
        var previousNotes = currentTab.getNotes();
        for (byte i = 0; i < previousNotes.Length; i++)
        {
          if (previousNotes[i] == 2 && pressingNoteControl[i] && isNotePressed[i])
          {
            currentTab.removeNote(i);
          }
        }
      }
      this.currentTab = tab;
      var currentNotes = currentTab.getNotes();
      for (byte i = 0; i < currentNotes.Length; i++)
      {
        pressingNoteControl[i] = currentNotes[i] == 2;
      }  
    }
    public void notePlayed(byte note)
    {
      if (!isNotePressed[note] && this.currentTab != null)
      {
        isNotePressed[note] = true;
        if (currentTab.getNotes()[note] == 1)
        {
          currentTab.removeNote(note);
        }
      }
    }
    public void noteReleased(byte note)
    {
      isNotePressed[note] = false;
      pressingNoteControl[note] = false;
    }
  }
}