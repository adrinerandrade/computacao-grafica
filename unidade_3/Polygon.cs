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
    private List<Ponto4D> points4D;
    private int selectedPoint = -1;
    private Bbox Bbox;
    public PrimitiveType primitive { get; set; } = PrimitiveType.LineStrip;
    public List<Polygon> polygons { get; set; }
    public Color color { get; set; } = Color.Blue;
    
    public Polygon(List<Ponto4D> points4D)
    {
      this.points4D = points4D;
      this.UpdateBBox();
    }
    public void AddVertex(Ponto4D point)
    {
      this.points4D.Add(point);
      this.UpdateBBox();
    }
    public void UpdateVertexLocation(int index, double X, double Y)
    {
      var point = this.points4D[index];
      point.X = X;
      point.Y = Y;
      this.UpdateBBox();
    }
    public void RemoveVertex(int index)
    {
      points4D.Remove(this.points4D[index]);
      if (this.selectedPoint == index)
      {
        this.selectedPoint = -1;
      }
      this.UpdateBBox();
    }
    public int VertexCount() {
      return this.points4D.Count;
    }
    public void SelectNearestVertex(double X, double Y)
    {
      this.selectedPoint = DistanceManhattan(new Ponto4D() { X = X, Y = Y});
    }
    public int GetSelectedVertex()
    {
      return selectedPoint;
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
      if (this.selectedPoint > -1) {
        DrawSelectedVertex(this.points4D[this.selectedPoint]);
      }
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
    private void DrawSelectedVertex(Ponto4D point)
    {
      GL.Color3(Color.Red);
      GL.LineWidth(1);
      GL.PointSize(2);
      GL.Begin(PrimitiveType.Points);
      double x, y, z;
      double i;
      for (i = 0; i <= 72; i++)
      {
        x = point.X + (3 * Math.Cos(i));
        y = point.Y + (3 * Math.Sin(i));
        z = 0;
        GL.Vertex3(x, y, z);
      }
      GL.End();
    }
    public void DrawBBox()
    {
      this.Bbox.Draw();
    }
    private int DistanceManhattan(Ponto4D point4D)
    {
      int selectedPoint = -1;
      double minValue = Double.MaxValue;
      for (var i = 0; i < this.points4D.Count; i++)
      {
        var point = this.points4D[i];
        double distanceX = point.X - point4D.X;
        double distanceY = point.Y - point4D.Y;
        double distanceManhattan = Math.Abs(distanceX) + Math.Abs(distanceY);
        if (minValue > distanceManhattan)
        {
          minValue = distanceManhattan;
          selectedPoint = i;
        }
      }
      return selectedPoint;
    }
    private void UpdateBBox()
    {
      this.Bbox = new Bbox(this.points4D);
    }
  }
}