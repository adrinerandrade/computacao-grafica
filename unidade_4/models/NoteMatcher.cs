namespace gcgcg 
{
  internal class NoteMatcher : Block
  {
    public NoteMatcher(float z, byte note): 
    base(Note.getXPosition(0, note), (Tab.HEIGHT / 2), z, Note.getColor(note), 1, 1, 0.2f, null)
    {
    }

  }
}