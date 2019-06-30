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
    Mundo mundo;
    MusicExecution musicExecution;
    Camera camera = new Camera();
    Vector3 eye = Vector3.Zero, target = Vector3.Zero, up = Vector3.UnitY;

    public Render(int width, int height) : base(width, height) {
      this.mundo = Mundo.getInstance();
      this.musicExecution = new MusicExecution("doremifa");
      this.mundo.NewMusicExecution(musicExecution);
      this.musicExecution.OnStop(() => this.Close());
      this.musicExecution.Start();
    }

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

      Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1.0f, 50f);
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
      if (e.Key == Key.A)
        Console.WriteLine("play 0");
      else
        if (e.Key == Key.S)
          Console.WriteLine("play 1");
      else
        if (e.Key == Key.D)
          Console.WriteLine("play 2");
      else
        if (e.Key == Key.F)
          Console.WriteLine("play 3");
      else
        if (e.Key == Key.G)
          Console.WriteLine("play 4");
    }
    protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
    {
      this.musicExecution.Cancel();
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
