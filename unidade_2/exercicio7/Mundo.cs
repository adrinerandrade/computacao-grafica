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
    private double x = 1;

    public Mundo() {
      circleX = center;
      circleY = center;
      Task.Run(async () => {
        while(true) {
          await Task.Delay(500);
          x++;
          Console.WriteLine(x);
        }
      });
    }

    public void Desenha()
    {
      Console.WriteLine("[6] .. Desenha");
      GL.PointSize(1);
      GL.LineWidth(1);
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

    private void drawLimitCircle() {
      this.drawCircle(center, center, limit);
      GL.Color3(Color.Purple);
      GL.Begin(PrimitiveType.Lines);
        squareBaseX = -(limit * Math.Sin(4));
        squareBaseY = -(limit * Math.Cos(4));
        this.drawSquare(center, new Ponto4D[] {
          new Ponto4D(squareBaseX, squareBaseY),
          new Ponto4D(squareBaseX, -squareBaseY),
          new Ponto4D(-squareBaseX, -squareBaseY),
          new Ponto4D(-squareBaseX, squareBaseY),
        });
      GL.End();
      GL.PointSize(5);
      GL.Color3(Color.Red);
      GL.Begin(PrimitiveType.Points);
        VertexPoint(new Ponto4D(center + squareBaseX, center + squareBaseY));
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
          VertexPoint(new Ponto4D(x + (raio * Math.Sin(i)), y + (raio * Math.Cos(i))));
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
