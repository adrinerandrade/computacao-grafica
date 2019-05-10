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
    public PrimitiveType primitive { get; set; } = PrimitiveType.LineStrip;
    public Bbox Bbox { get; set; }
    public List<Polygon> polygons { get; set; }
    public Color color { get; set; } = Color.Black;
    public Ponto4D pointSelected { get; set; }
    public void SelectVertex(Ponto4D point)
    {
        pointSelected = DistanceManhattan(point);
        DrawSelectedVertx(pointSelected);
    }
    public void DrawSelectedVertx(Ponto4D point) {
      GL.Color3(Color.Yellow);
      GL.LineWidth(1);
      GL.PointSize(2);
      GL.Begin(PrimitiveType.Points);
        double x, y, z;
        double i;
        for (i = 0; i <= 72; i ++)
        {
            x = point.X + (10*Math.Cos(i));
            y = point.Y + (10*Math.Sin(i));
            z = 0;
            GL.Vertex3(x, y, z);
        }
      GL.End();
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
    public void DropSelectedVertex() {
      if (pointSelected != null) {
        points4D.Remove(pointSelected);
        pointSelected = null;
      }
    }
    private Ponto4D DistanceManhattan(Ponto4D point4D)
    {
      Ponto4D selectedPoint = null;
      double minValue = Double.MaxValue;
      foreach (var point in points4D)
      {
        double distanceX = point.X - point4D.X;
        double distanceY = point.Y - point4D.Y;
        double distanceManhattan = Math.Abs(distanceX) + Math.Abs(distanceY);
        if (minValue > distanceManhattan) {
          minValue = distanceManhattan;
          selectedPoint = point;
        }
      }
      return selectedPoint;
    }
  }
}