using System;

namespace gcgcg
{
  internal class Note: Block
  {
    public Note(Tab tab, byte index, byte type): base(
      getXPosition(tab, index), tab.y + (Tab.HEIGHT / 2), 
      tab.z, 
      getColor(index), 
      getLength(type), 
      getWidth(type), 
      getHeight(type))
    {
      tab.addChild(this);
    }
    private static float getXPosition(Tab tab, byte index)
    {
      var factor = Tab.LENGTH / 10;
      return tab.x + (((index * 2) - 4) * factor);
    }
    private static float getWidth(byte type)
    {
      return type == 1 ? 2 : Tab.WIDTH;
    }
    private static float getLength(byte type)
    {
      return type == 1 ? 2 : 0.5f;
    }
    private static float getHeight(byte type)
    {
      return type == 1 ? 1 : 0.3f;
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