namespace gcgcg
{
  internal class Tab: Block
  {
    public static readonly int HEIGHT = 2;
    public static readonly int WIDTH = 5;
    public static readonly int LENGTH = 12;
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
    private byte[] notes;
    public Tab(float x, float y, float z, byte[] notes): base(x, y, z, new int[] { 143, 77, 31}, LENGTH, WIDTH, HEIGHT)
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