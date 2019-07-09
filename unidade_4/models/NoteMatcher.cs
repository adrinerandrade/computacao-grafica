namespace gcgcg 
{
  internal class NoteMatcher : Block
  {
    private static readonly int COLOR_FACTOR = 40;
    private int[] color;
    private bool active = false;
    public NoteMatcher(float z, byte note): 
    base(Note.getXPosition(0, note), (Tab.HEIGHT / 2), z, Note.getColor(note), 1, 1, 0.2f, null)
    {
      this.color = Note.getColor(note);
    }

    public void setActive()
    {
      if (!active) {
        this.color[0] = this.color[0] + COLOR_FACTOR;
        this.color[1] = this.color[1] + COLOR_FACTOR;
        this.color[2] = this.color[2] + COLOR_FACTOR;
        setColor(this.color);
        this.active = true;
      }
    }
    public void setInactive()
    {
      if (active) {
        this.color[0] = this.color[0] - COLOR_FACTOR;
        this.color[1] = this.color[1] - COLOR_FACTOR;
        this.color[2] = this.color[2] - COLOR_FACTOR;
        setColor(this.color); 
        this.active = false;
      }
    }

  }
}