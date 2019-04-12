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

    public void Desenha(PrimitiveType primitive)
    {
      Console.WriteLine("[6] .. Desenha");
      
      GL.LineWidth(5);
      GL.PointSize(5);
      int offset = 200;
      Ponto4D[] vertices = {
        new Ponto4D(offset, -offset),
        new Ponto4D(offset, offset),
        new Ponto4D(-offset, offset),
        new Ponto4D(-offset, -offset),
      };
      GL.Begin(primitive);
      
      GL.Color3(Color.Red);
      vertexPoint(vertices[0]);
      GL.Color3(Color.Green);
      vertexPoint(vertices[1]);
      GL.Color3(Color.Blue);
      vertexPoint(vertices[2]);
      GL.Color3(Color.Magenta);
      vertexPoint(vertices[3]);
      GL.End();
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

    private void vertexPoint(Ponto4D ponto) {
      GL.Vertex3(ponto.X, ponto.Y, ponto.Z);
    }

  }

}