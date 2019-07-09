namespace gcgcg
{
  public class NoteMatcherBar
  {
    private NoteMatcher noteMatcher1;
    private NoteMatcher noteMatcher2;
    private NoteMatcher noteMatcher3;
    private NoteMatcher noteMatcher4;
    private NoteMatcher noteMatcher5;
    public NoteMatcherBar()
    {
      float z = (((GuitarTab.TABS_SIZE / 2) - 1) * Tab.WIDTH);
      this.noteMatcher1 = new NoteMatcher(z, 0);
      this.noteMatcher2 = new NoteMatcher(z, 1);
      this.noteMatcher3 = new NoteMatcher(z, 2);
      this.noteMatcher4 = new NoteMatcher(z, 3);
      this.noteMatcher5 = new NoteMatcher(z, 4);
    }
    public void Desenha()
    {
      this.noteMatcher1.Desenha();
      this.noteMatcher2.Desenha();
      this.noteMatcher3.Desenha();
      this.noteMatcher4.Desenha();
      this.noteMatcher5.Desenha();
    }
    public void setActive(byte note)
    {
      if (note == 0)
      {
        this.noteMatcher1.setActive();
      } else if (note == 1)
      {
        this.noteMatcher2.setActive();
      } else if (note == 2)
      {
        this.noteMatcher3.setActive();
      } else if (note == 3)
      {
        this.noteMatcher4.setActive();
      } else {
        this.noteMatcher5.setActive();
      }
    }
    public void setInactive(byte note)
    {
      if (note == 0)
      {
        this.noteMatcher1.setInactive();
      } else if (note == 1)
      {
        this.noteMatcher2.setInactive();
      } else if (note == 2)
      {
        this.noteMatcher3.setInactive();
      } else if (note == 3)
      {
        this.noteMatcher4.setInactive();
      } else {
        this.noteMatcher5.setInactive();
      }
    }
  }
}