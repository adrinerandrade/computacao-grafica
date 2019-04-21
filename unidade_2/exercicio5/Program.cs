using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace gcgcg
{
  public class Render : GameWindow
    {
        Camera camera;
        Mundo mundo ;
        public Render(int width, int height) : base(width, height)
        {
            this.camera = new Camera(this);
            camera.InitOrtho(-400, 400, -400, 400);//camera

            this.mundo = new Mundo(camera);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            camera.Ortho();//camera

        
            

        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color.White);
            GL.MatrixMode(MatrixMode.Modelview);

            SRU3D.Render();
            mundo.Render();

            this.SwapBuffers();
        }
    }

  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("[1] .. Main");

      Render window = new Render(800, 800);
      window.Run();
      window.Run(1.0/60.0);
    }
  }
}
