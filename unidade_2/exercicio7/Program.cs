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
    bool listenKeyPress = true;

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

      OpenTK.Input.KeyboardState keyboardState = OpenTK.Input.Keyboard.GetState();
      KeyPress(keyboardState);
      mundo.SRU3D();
      mundo.Desenha();

      this.SwapBuffers();
    }

    public void KeyPress(OpenTK.Input.KeyboardState keyState) {
      if (keyState.IsKeyDown(OpenTK.Input.Key.C)) {
        this.mundo.moveSelectedPoint(Direction.UP);
      }
      if (keyState.IsKeyDown(OpenTK.Input.Key.B)) {
        this.mundo.moveSelectedPoint(Direction.DOWN);
      }
      if (keyState.IsKeyDown(OpenTK.Input.Key.E)) {
        this.mundo.moveSelectedPoint(Direction.LEFT);
      }
      if (keyState.IsKeyDown(OpenTK.Input.Key.D)) {
        this.mundo.moveSelectedPoint(Direction.RIGHT);
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
