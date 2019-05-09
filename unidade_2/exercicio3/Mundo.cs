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

    public void Desenha()
    {
      Console.WriteLine("[6] .. Desenha");
      
      GL.LineWidth(5);
      GL.PointSize(5);
      Ponto4D[] vertices = desenhaTrianguloEquilatero(200);
      foreach (var vertice in vertices) {
        desenhaCirculo((float) vertice.X, (float) vertice.Y, 100);
      }
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

    private Ponto4D[] desenhaTrianguloEquilatero(int tamanhoLado) {
      float offset = tamanhoLado / 2;
      Ponto4D[] vertices = { 
        new Ponto4D(offset, offset),
        new Ponto4D(-offset, offset),
        new Ponto4D(0, -offset)
      };
      GL.Color3(Color.Blue);
      GL.Begin(PrimitiveType.Lines);
        GL.Color3(Color.BlueViolet);
        vertexPoint(vertices[0]); vertexPoint(vertices[1]);
        vertexPoint(vertices[1]); vertexPoint(vertices[2]);
        vertexPoint(vertices[2]); vertexPoint(vertices[0]);
      GL.End();
      return vertices;
    }

    private void vertexPoint(Ponto4D ponto) {
      GL.Vertex3(ponto.X, ponto.Y, ponto.Z);
    }

    private void desenhaCirculo(float x, float y, int raio) {
      GL.Color3(Color.Black);
      GL.Begin(PrimitiveType.Points);
      float tempX, tempY, tempZ;
      double i;
        for (i = 0; i <= 72; i ++)
        {
            tempX = x + (float)(100 * Math.Sin(i));
            tempY = y + (float)(100 * Math.Cos(i));
            tempZ = (float)0;
            GL.Vertex3(tempX, tempY, tempZ);
        }
      GL.End();
    }

  }

}