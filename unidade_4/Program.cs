/*
  Autor: Dalton Solano dos Reis
*/

using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using OpenTK.Input;

namespace gcgcg
{
  class Render : GameWindow
  {
    Mundo mundo = Mundo.getInstance();
    Camera camera = new Camera();
    Vector3 eye = Vector3.Zero, target = Vector3.Zero, up = Vector3.UnitY;

    public Render(int width, int height) : base(width, height) { }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);

      GL.ClearColor(Color.Gray);
      GL.Enable(EnableCap.DepthTest);

      eye.X = 0;
      eye.Y = 10;
      eye.Z = 40;

    }
    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);

      GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);

      Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1.0f, 50.0f);
      GL.MatrixMode(MatrixMode.Projection);
      GL.LoadMatrix(ref projection);
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
      base.OnUpdateFrame(e);
    }
    protected override void OnRenderFrame(FrameEventArgs e)
    {
      base.OnRenderFrame(e);

      GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

      Matrix4 modelview = Matrix4.LookAt(eye, target, up);
      GL.MatrixMode(MatrixMode.Modelview);
      GL.LoadMatrix(ref modelview);

      mundo.Desenha();

      this.SwapBuffers();
    }

    protected override void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
    {
      mundo.OnKeyDown(e);
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      Render window = new Render(600, 600);
      window.Run();
      window.Run(1.0 / 60.0);
    }
  }

}
