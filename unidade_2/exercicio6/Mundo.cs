using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Collections.Generic;

namespace gcgcg
{
  class Mundo
  {

    private List<Ponto4D> points;
    private short selectedPoint = 0;
    private double controlPointsCount = 25;

    public Mundo() {
      this.init();
    }

    public void init() {
      this.points = new List<Ponto4D> {
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
      for (var i = 0; i < this.points.Count; i++) {
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
      this.drawSpline();
    }

    private void drawSpline() {
      Ponto4D lastSplinePoint = null;
      List<Ponto4D> splinePoints = getSplinePoints();
      for (var i = 0; i < splinePoints.Count; i++) {
        Ponto4D splinePoint = splinePoints[i];
        if (lastSplinePoint != null) {
          GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Yellow);
            VertexPoint(lastSplinePoint);
            VertexPoint(splinePoint);
          GL.End();
        }
        lastSplinePoint = splinePoint;
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
      if (this.controlPointsCount < 50) {
        this.controlPointsCount++;
      }
    }

    public void DecreaseSplineControlPoint() {
      if (this.controlPointsCount > 1) {
        this.controlPointsCount--;
      }
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
      this.init();
    }

    private List<Ponto4D> getSplinePoints() {
      List<Ponto4D> splinePoints = new List<Ponto4D>();
      Console.Write(100 / this.controlPointsCount);
      for (double t = 0; t <= 1; t += 1 / this.controlPointsCount) {
        splinePoints.Add(this.getSplinePoint(t));
      }
      return splinePoints;
    }

    private Ponto4D getSplinePoint(double t) {
      Queue<Ponto4D> queue = new Queue<Ponto4D>();
      this.points.ForEach(Point => queue.Enqueue(Point));
      while (queue.Count > 1) {
        Ponto4D ponto1 = queue.Dequeue();
        Ponto4D ponto2 = queue.Dequeue();
        double newX = calculate(ponto1.X, ponto2.X, t);
        double newY = calculate(ponto1.Y, ponto2.Y, t);
        queue.Enqueue(new Ponto4D(newX, newY));
      }
      return queue.Dequeue();
    }

    private double calculate(double A, double B, double t) {
      return A + (B - A) * t;
    }

  }
}
