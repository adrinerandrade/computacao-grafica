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
    public Bbox Bbox { get; set; }
    public List<Polygon> polygons { get; set; }
    public Color color { get; set; } = Color.Black;
    public void SelectClosestVertex(double x, double y) {

    }
    public void Draw()
    {
      GL.Color3(color);
      GL.Begin(primitive);
      foreach (var point in points4D)
      {
        GL.Vertex3(point.X, point.Y, point.Z);
      }
      DrawChildrens();
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