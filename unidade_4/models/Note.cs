using System;

namespace gcgcg
{
  internal class Note: Block
  {
    public Note(Tab tab, byte index, byte type): base(
      getXPosition(tab.x, index), tab.y + (Tab.HEIGHT / 2), 
      tab.z, 
      getColor(index), 
      getLength(type), 
      getWidth(type), 
      getHeight(type),
      new NoteTexture(Render.noteTexture)
    )
    {
    }
    public static float getXPosition(float tabX, byte index)
    {
      var factor = Tab.LENGTH / 10;
      return tabX + (((index * 2) - 4) * factor);
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
    public static int[] getColor(byte index)
    {
      switch (index)
      {
        case 0:
          return new int[] { 143, 0, 0 };
        case 1:
          return new int[] { 194, 194, 0 };
        case 2:
          return new int[] { 0, 143, 0 };
        case 3:
          return new int[] { 0, 66, 143 };
        default:
          return new int[] { 205, 118, 0 };
      }
    }
  }
}