namespace gcgcg
{
  internal class Background: Block
  {
    public static readonly int HEIGHT = 40;
    public static readonly int WIDTH = 70;
    public static readonly int LENGTH = 50;
    private byte[] notes;
    public Background()
      : base(0, 0, 15, new int[] { 0, 0, 255 }, LENGTH, WIDTH, HEIGHT, new BackgroundTexture(Render.noteTexture))
    {
    }
  }
}