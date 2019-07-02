namespace gcgcg
{
  internal class Tab: Block
  {
    public static readonly int HEIGHT = 2;
    public static readonly int WIDTH = 5;
    public static readonly int LENGTH = 12;
    private byte[] notes;
    public Tab(float x, float y, float z, byte[] notes, byte index)
      : base(x, y, z, new int[] { 255, 255, 255 }, LENGTH, WIDTH, HEIGHT, new TabTexture(Render.tabTexture))
    {
      for (byte i = 0; i < notes.Length; i++)
      {
        if (notes[i] != 0)
        {
          new Note(this, i, notes[i]);
        }
      }
    }
  }
}