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
    public List<Polygon> children { get; set; } = new List<Polygon>();
    public Color color { get; set; } = Color.Blue;

    private Transformacao4D transformacao = new Transformacao4D();
    
    public Polygon(List<Ponto4D> points4D)
    {
      this.points4D = points4D;
      this.transformacao.atribuirIdentidade();
      this.Bbox = new Bbox(points4D);
    }
    public void AddVertex(Ponto4D point)
    {
      this.points4D.Add(point);
      this.UpdateBBox();
    }
    public void UpdateVertexLocation(int index, double X, double Y)
    {
      var point = this.points4D[index];
      var result = this.GetDesloc(point, new Ponto4D() { X = X, Y = Y });
      point.X = result.X;
      point.Y = result.Y;
      this.UpdateBBox();
    }
    private Ponto4D GetDesloc(Ponto4D sourcePoint, Ponto4D targetPoint) {
      var transformedPoint = this.transformacao.transformPoint(sourcePoint);
      var result = new Ponto4D();
      result.X = sourcePoint.X + targetPoint.X - transformedPoint.X;
      result.Y = sourcePoint.Y + targetPoint.Y - transformedPoint.Y;
      return result;
    }
    public void Translation(double translX, double translY)
    {
      var transl = new Transformacao4D();
      transl.atribuirTranslacao(translX, translY, 0);

      this.transformacao = transl.transformMatrix(this.transformacao);
      this.UpdateBBox();
    }
    public void Scale(double scale)
    {
      var translX = this.Bbox.centerX;
      var translY = this.Bbox.centerY;
      
      var originTrans = new Transformacao4D();
      originTrans.atribuirTranslacao(translX, translY, 0);

      var scaleTrans = new Transformacao4D();
      scaleTrans.atribuirEscala(scale, scale, 1);

      var initialPositionTrans = new Transformacao4D();
      initialPositionTrans.atribuirTranslacao(-translX, -translY, 0);

      var result = originTrans.transformMatrix(scaleTrans);
      result = result.transformMatrix(initialPositionTrans);

      this.transformacao = result.transformMatrix(this.transformacao);
      this.UpdateBBox();
    }
    public void Rotate(double degreeFactor)
    {
      var translX = this.Bbox.centerX;
      var translY = this.Bbox.centerY;

      var originTrans = new Transformacao4D();
      originTrans.atribuirTranslacao(translX, translY, 0);

      var rotationTrans = new Transformacao4D();
      rotationTrans.atribuirRotacaoZ(Transformacao4D.DEG_TO_RAD * degreeFactor);

      var initialPositionTrans = new Transformacao4D();
      initialPositionTrans.atribuirTranslacao(-translX, -translY, 0);

      var result = originTrans.transformMatrix(rotationTrans);
      result = result.transformMatrix(initialPositionTrans);

      this.transformacao = result.transformMatrix(this.transformacao);
      this.UpdateBBox();
    }
    public void RemoveVertex(int index)
    {
      points4D.Remove(this.points4D[index]);
      if (this.selectedPoint == index)
      {
        this.DeselectVertex();
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
    public void DeselectVertex()
    {
      this.selectedPoint = -1;
    }
    public int GetSelectedVertex()
    {
      return selectedPoint;
    }
    public Bbox GetBBox()
    {
      return this.Bbox.Clone();
    }
    public List<Ponto4D> GetTransformedPoints()
    {
      return this.points4D.ConvertAll(point => this.transformacao.transformPoint(point));
    }
    public void Draw()
    {
      GL.PushMatrix();
      GL.LoadMatrix(transformacao.GetDate());

      GL.Color3(color);
      GL.Begin(primitive);
      foreach (var point in points4D)
      {
        GL.Vertex3(point.X, point.Y, point.Z);
      }
      GL.End();
      DrawChildrens();
      if (this.selectedPoint > -1) {
        DrawSelectedVertex(this.points4D[this.selectedPoint]);
      }

      GL.PopMatrix();
    }
    private void DrawChildrens()
    {
      if (children != null)
      {
        foreach (var poligono in children)
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
      var points = this.GetTransformedPoints();
      for (var i = 0; i < points.Count; i++)
      {
        var point = points[i];
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
      this.Bbox.BBoxDimensions(this.GetTransformedPoints());
    }
  }
}