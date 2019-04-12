using System;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace gcgcg
{
  class Mundo
  {
    private Ponto4D ptoDirCim = new Ponto4D(100, 100);
    private Ponto4D ptoOrigem = new Ponto4D(0,0);
    private double k = 0;

    public Mundo() {
      Task.Run(async () => {
                        for(;;)
                        {
                            await Task.Delay(10);
                            k++;
                            if (k >= 360) k = 0;
                        }
                    });
    }

    public void Desenha()
    {
      Console.WriteLine("[6] .. Desenha");
      
      GL.LineWidth(5);
      GL.PointSize(5);
      GL.Color3(Color.Yellow);
      GL.Begin(PrimitiveType.Points);
      float x, y, z;
      double i;
        for (i = 0; i <= k; i ++)
        {
            x = (float)(100*Math.Sin(i));
            y = (float)(100*Math.Cos(i));
            z = (float)0;
            GL.Vertex3(x, y, z);
        }
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
  }

}