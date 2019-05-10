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
    public static void UpdateDirections(OpenTK.Input.MouseMoveEventArgs e) {
      X = e.X;
      Y = 600 - e.Y;
    }
  }

}
