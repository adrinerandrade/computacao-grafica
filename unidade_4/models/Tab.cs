using System;

namespace gcgcg
{
  internal class Tab: Block
  {
    public static readonly int HEIGHT = 2;
    public static readonly int WIDTH = 5;
    public static readonly int LENGTH = 12;
    private byte[] notes;
    private Note[] notesRef = new Note[] { null, null, null, null, null };
    public Tab(float x, float y, float z, byte[] notes, byte index)
      : base(x, y, z, new int[] { 255, 255, 255 }, LENGTH, WIDTH, HEIGHT, new TabTexture(Render.tabTexture))
    {
      this.notes = notes;
      for (byte i = 0; i < notes.Length; i++)
      {
        if (notes[i] != 0)
        {
          notesRef[i] = new Note(this, i, notes[i]);
          this.addChild(notesRef[i]);
        }
      }
    }
    public void removeNote(byte note)
    {
      if (notesRef[note] != null)
      {
        this.removeChild(notesRef[note]);
      }
    }
    public byte[] getNotes()
    {
      return notes;
    }
  }
}