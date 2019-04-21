using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace gcgcg
{
  public class SRU3D
    {

        //Positive X
        public const int PX = 200;
        //Negative X
        public const int NX = -200;

        //Positive Y
        public const int PY = 200;
        //Negative Y
        public const int NY = -200;

        public static void Render(){
            GL.LineWidth(1);
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0); GL.Vertex3(PX, 0, 0);//x
            GL.Vertex3(0, 0, 0); GL.Vertex3(NX, 0, 0);//x
            GL.Color3(Color.Green);
            GL.Vertex3(0, 0, 0); GL.Vertex3(0, PY, 0);//y
            GL.Vertex3(0, 0, 0); GL.Vertex3(0, NY, 0);//y
            GL.End();
        }
    }
}