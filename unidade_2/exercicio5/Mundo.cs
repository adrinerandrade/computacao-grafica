using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace gcgcg
{
  class Mundo
  {
    private Ponto4D ptoDirCim = new Ponto4D(100, 100);
    private Ponto4D ptoOrigem = new Ponto4D(0,0);

    private float directionX1 = 0;
    private float directionX2 = 100;
    private float directionY1 = 0;
    private float directionY2 = -100;

    public void Desenha()
    {
      Console.WriteLine("[6] .. Desenha");
      
      GL.LineWidth(1);
      GL.PointSize(4);
      GL.Color3(Color.Yellow);
      GL.Begin(PrimitiveType.Lines);
        GL.Vertex3(this.directionX1, this.directionY1, 0);
        GL.Vertex3(this.directionX2, this.directionY2, 0);
      GL.End();
    }

    public void RightMove() {
      this.directionX1 += 5;
      this.directionX2 += 5;
    }

    public void LeftMove() {
      this.directionX1 -= 5;
      this.directionX2 -= 5;
    }

    public void Height() {
      this.directionX2 -= 5;
      this.directionY2 += 5;
    }

    public void Increase() {
      this.directionX2 -= 5;
      this.directionY2 += 5;
    }

    public void Decrease() {
      this.directionX2 += 5;
      this.directionY2 -= 5;
    }

    public void Rotate() {
      float val = 1 + this.directionY2;
      float val2 = 1 - this.directionX2;
      this.directionX2 = (float)(100*Math.Sin(val2));
      this.directionY2 = (float)(100*Math.Cos(val));
      
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
  }

}