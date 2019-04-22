using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Threading.Tasks;

namespace gcgcg
{
  class Render : GameWindow
  {
    Mundo mundo = new Mundo();
    private bool mouseWasPressed = false;
    private double lastMouseX = 0;
    private double lastMouseY = 0;

    public Render(int width, int height) : base(width, height) { }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      Console.WriteLine("[2] .. OnLoad");
    }
    protected override void OnUpdateFrame(FrameEventArgs e)
    {
      base.OnUpdateFrame(e);
      Console.WriteLine("[3] .. OnUpdateFrame");

      GL.MatrixMode(MatrixMode.Projection);
      GL.LoadIdentity();
      GL.Ortho(-400, 400, -400, 400, -1, 1);
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
      base.OnRenderFrame(e);
      Console.WriteLine("[4] .. OnRenderFrame");
      
      GL.Clear(ClearBufferMask.ColorBufferBit);
      GL.ClearColor(Color.Gray);
      GL.MatrixMode(MatrixMode.Modelview);

      mundo.SRU3D();
      mundo.Desenha();

      this.SwapBuffers();
    }

    protected override void OnMouseUp(OpenTK.Input.MouseButtonEventArgs e) {
      base.OnMouseUp(e);
      this.mundo.reset();
    }

    protected override void OnMouseMove(OpenTK.Input.MouseMoveEventArgs e) {
      base.OnMouseMove(e);
      bool mousePressed = e.Mouse.IsButtonDown(OpenTK.Input.MouseButton.Left);
      this.mouseWasPressed = mousePressed;
      if (mousePressed && mouseWasPressed) {
        double xDesloc = e.X - lastMouseX;
        double yDesloc = lastMouseY - e.Y;
        this.mundo.moveSelectedPoint(xDesloc, yDesloc);
      }
      if (!mousePressed) {
        mouseWasPressed = false;
        lastMouseX = 0;
        lastMouseY = 0;
      } else {
        mouseWasPressed = true;
        lastMouseX = e.X;
        lastMouseY = e.Y;
      }
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
