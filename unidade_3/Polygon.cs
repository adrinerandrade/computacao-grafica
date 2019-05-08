using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using OpenTK.Input;
using System.Collections.Generic;

namespace gcgcg
{
  public class Polygon
  {
      private List<Ponto4D> points4D = new List<Ponto4D>();
      private PrimitiveType primitive;
      private List<Polygon> polygons = new List<Polygon>();
      private string color;

      private void Draw() {
        GL.Color3(Color.Black);
        GL.Begin(primitive);
        foreach (var point in points4D)
        {
          GL.Vertex3(point.X, point.Y, point.Z); 
          foreach (var poligono in polygons)
          {
              poligono.Draw();
          }
        }
        GL.End();
      }
  }
}