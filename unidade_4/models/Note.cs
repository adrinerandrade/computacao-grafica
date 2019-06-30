namespace gcgcg
{
  internal class Note: Block
  {
    public readonly static float LENGTH = 2;
    public Note(Tab tab, byte index, byte type): base(getXPosition(tab, index), tab.y + (Tab.HEIGHT / 2), tab.z, getColor(index), LENGTH, 2, 0.5f)
    {
      tab.addChild(this);
    }
    private static float getXPosition(Tab tab, byte index)
    {
      var factor = Tab.LENGTH / 10;
      return tab.x + (((index * 2) - 4) * factor);
    }
    private static int[] getColor(byte index)
    {
      switch (index)
      {
        case 0:
          return new int[] { 153, 0, 0 };
        case 1:
          return new int[] { 204, 204, 0 };
        case 2:
          return new int[] { 0, 153, 0 };
        case 3:
          return new int[] { 0, 76, 153 };
        default:
          return new int[] { 255, 128, 0 };
      }
    }
  }
}