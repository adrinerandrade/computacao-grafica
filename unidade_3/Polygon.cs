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
    public List<Ponto4D> points4D { get; set; }
    public PrimitiveType primitive { get; set; } = PrimitiveType.Lines;
    public List<Polygon> polygons { get; set; }
    public string color;

    public void Draw()
    {
      GL.Color3(Color.Black);
      GL.Begin(primitive);
      foreach (var point in points4D)
      {
        GL.Vertex3(point.X, point.Y, point.Z);
        DrawChildrens();
      }
      GL.End();
    }

    private void DrawChildrens()
    {
      if (polygons != null)
      {
        foreach (var poligono in polygons)
        {
          poligono.Draw();
        }
      }
    }
  }
}