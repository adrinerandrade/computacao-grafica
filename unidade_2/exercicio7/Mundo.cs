using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gcgcg
{
  class Mundo
  {

    private double center = 150;
    private double limit = 125;
    private double circleX;
    private double circleY;
    private double squareBaseX;
    private double squareBaseY;
    private bool squareConflict;
    private bool circleConflict;

    public Mundo() {
      circleX = center;
      circleY = center;
    }

    public void Desenha()
    {
      Console.WriteLine("[6] .. Desenha");
      GL.PointSize(1);
      GL.LineWidth(1);
      this.verifyConflicts();
      this.drawLimitCircle();
      this.drawMainCircle();
    }

    public void SRU3D()
    {
      Console.WriteLine("[5] .. SRU3D");

      GL.LineWidth(1);
      GL.Begin(PrimitiveType.Lines);
      GL.Color3(Color.Red);
      GL.Vertex3(0, 0, 0); GL.Vertex3(200, 0, 0);
      GL.Color3(Color.Green);
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, 200, 0);
      GL.Color3(Color.Blue);
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, 0, 200);
      GL.End();
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
      double newX = this.circleX + xDesloc;
      double newY = this.circleY + yDesloc;
      if (!verifyNewCirclePositioning(newX, newY)) {
        this.circleX = newX;
        this.circleY = newY;
      };
    }

    private void verifyConflicts() {
      squareConflict = false;
      circleConflict = false;
      circleConflict = verifyCircleConflict();
      squareConflict = verifySquareConlfict();
    }

    private bool verifyNewCirclePositioning(double circleXPosition, double circleYPosition) {
      return Math.Sqrt(Math.Pow(center - circleXPosition, 2) + Math.Pow(center - circleYPosition, 2)) > limit + 2;
    }

    private bool verifyCircleConflict() {
      return Math.Sqrt(Math.Pow(center - circleX, 2) + Math.Pow(center - circleY, 2)) >= limit;
    }

    private bool verifySquareConlfict() {
      double squareX = this.center + this.squareBaseX;
      double squareY = this.center + this.squareBaseY;
      double squareNegX = this.center - this.squareBaseX;
      double squareNegY = this.center - this.squareBaseY;
      return (
        this.circleX >= squareX ||
        this.circleX <= squareNegX ||
        this.circleY >= squareY ||
        this.circleY <= squareNegY
      );
    }

    private void drawLimitCircle() {
      this.drawCircle(center, center, limit);
      GL.Color3(circleConflict ? Color.Cyan : squareConflict ? Color.Yellow : Color.Purple);
      GL.Begin(PrimitiveType.Lines);
        double angle = 45;
        angle *= Math.PI / 180;
        squareBaseX = limit * Math.Cos(angle);
        squareBaseY = limit * Math.Sin(angle);
        this.drawSquare(center, new Ponto4D[] {
          new Ponto4D(squareBaseX, squareBaseY),
          new Ponto4D(squareBaseX, -squareBaseY),
          new Ponto4D(-squareBaseX, -squareBaseY),
          new Ponto4D(-squareBaseX, squareBaseY),
        });
      GL.End();
      GL.PointSize(2);
    }

    private void drawMainCircle() {
      this.drawCircle(this.circleX, this.circleY, 40);
      GL.PointSize(5);
      GL.Begin(PrimitiveType.Points);
        VertexPoint(new Ponto4D(this.circleX, this.circleY));
      GL.End();
      GL.PointSize(2);
    }

    private void drawCircle(double x, double y, double raio) {
      GL.Color3(Color.Black);
      GL.PointSize(2);
      GL.Begin(PrimitiveType.Points);
        for (double i = 0; i <= 720; i ++)
        {
          VertexPoint(new Ponto4D(x + (raio * Math.Cos(i)), y + (raio * Math.Sin(i))));
        }
      GL.End();
    }

    private void drawSquare(double center, Ponto4D[] points) {
        GL.Vertex3(center + points[0].X, center + points[0].Y, 0);
        GL.Vertex3(center + points[1].X, center + points[1].Y, 0);
        GL.Vertex3(center + points[1].X, center + points[1].Y, 0);
        GL.Vertex3(center + points[2].X, center + points[2].Y, 0);
        GL.Vertex3(center + points[2].X, center + points[2].Y, 0);
        GL.Vertex3(center + points[3].X, center + points[3].Y, 0);
        GL.Vertex3(center + points[3].X, center + points[3].Y, 0);
        GL.Vertex3(center + points[0].X, center + points[0].Y, 0);
    }

    private void VertexPoint(Ponto4D ponto) {
      GL.Vertex3(ponto.X, ponto.Y, ponto.Z);
    }

  }
}
