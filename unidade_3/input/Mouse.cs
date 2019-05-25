using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace gcgcg
{
  public class Mouse
  {
    public static double X { get; set; }
    public static double Y { get; set; }
    public static void UpdateDirections(int nextX, int nextY) {
      X = nextX;
      Y = 600 - nextY;
    }
  }

}
