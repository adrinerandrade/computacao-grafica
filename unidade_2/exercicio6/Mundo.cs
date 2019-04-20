using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace gcgcg
{
  class Mundo
  {

    private Ponto4D[] points;
    private short selectedPoint = 0;

    public Mundo() {
      this.points = new Ponto4D[] {
        new Ponto4D(-100, -100),
        new Ponto4D(-100, 100),
        new Ponto4D(100, 100),
        new Ponto4D(100, -100),
      };
    }

    public void Desenha()
    {
      Console.WriteLine("[6] .. Desenha");
      GL.LineWidth(1);
      GL.PointSize(5);
      Ponto4D lastPoint = null;
      for (var i = 0; i < this.points.Length; i++) {
        Ponto4D point = this.points[i];
        GL.Begin(PrimitiveType.Points);
          GL.Color3(i == this.selectedPoint ? Color.Red : Color.Blue);
          VertexPoint(point);
        GL.End();
        if (lastPoint != null) {
          GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Cyan);
            VertexPoint(lastPoint);
            VertexPoint(point);
          GL.End();
        }
        lastPoint = point;
      }
    }

    public void SelectPoint(short pointIndex) {
      this.selectedPoint = pointIndex;
    }

    private Ponto4D GetSelectedPoint() {
      return this.points[this.selectedPoint];
    }

    public void SRU3D()
    {
      Console.WriteLine("[5] .. SRU3D");

      GL.LineWidth(1);
      GL.Begin(PrimitiveType.Lines);
      GL.Color3(Color.Red);
      GL.Vertex3(0, 0, 0); GL.Vertex3(200, 0, 0);
      GL.Color3(Color.Green);
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, -200, 0);
      GL.Color3(Color.Blue);
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, 0, 200);
      GL.End();
    }

    public void IncreaseSplineControlPoint() {

    }

    public void DecreaseSplineControlPoint() {
      
    }

    public void moveSelectedPoint(Direction direction) {
      double xDesloc = 0;
      double yDesloc = 0;
      double moveSpeed = 1;
      switch (direction) {
        case Direction.UP:
          yDesloc = moveSpeed;
          break;
        case Direction.DOWN:
          yDesloc = -moveSpeed;
          break;
        case Direction.RIGHT:
          xDesloc = moveSpeed;
          break;
        case Direction.LEFT:
          xDesloc = -moveSpeed;
          break;
      }
      
      Ponto4D ponto = this.GetSelectedPoint();
      ponto.X = ponto.X + xDesloc;
      ponto.Y = ponto.Y + yDesloc;
    }

    private void VertexPoint(Ponto4D ponto) {
      GL.Vertex3(ponto.X, ponto.Y, ponto.Z);
    }

    public void reset() {
        throw new NotImplementedException();
    }

  }
}
