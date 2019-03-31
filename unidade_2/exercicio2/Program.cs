using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace gcgcg
{
  class Render : GameWindow
  {
    Mundo mundo = new Mundo();
    private int E = 400;
    private int D = 400;
    private int B = 400;
    private int C = 400;
    private int I = 1;
    private int O = 1;
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

      OpenTK.Input.KeyboardState keyboardState = OpenTK.Input.Keyboard.GetState();

      // Check Key Presses
      KeyPress(keyboardState);

      GL.MatrixMode(MatrixMode.Projection);
      GL.LoadIdentity();
      GL.Ortho(-this.D, this.E, this.C, -this.B, -this.I, this.O);
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

    public void KeyPress(OpenTK.Input.KeyboardState keyState)
    {
      if (keyState.IsKeyDown(OpenTK.Input.Key.E)) {
        this.E = this.E + 5;
        this.D = this.D - 5;
      }
      if (keyState.IsKeyDown(OpenTK.Input.Key.D)) {
        this.E = this.E - 5;
        this.D = this.D + 5;
      }
      if (keyState.IsKeyDown(OpenTK.Input.Key.C)) {
        this.C = this.C + 5;
        this.B = this.B - 5;
      }
      if (keyState.IsKeyDown(OpenTK.Input.Key.B)) {
        this.C = this.C - 5;
        this.B = this.B + 5;
      }
      if (keyState.IsKeyDown(OpenTK.Input.Key.I)) {
        this.I = this.I + 1;
        this.O = this.O - 1;
      }
      if (keyState.IsKeyDown(OpenTK.Input.Key.O)) {
        this.I = this.I - 1;
        this.O = this.O + 1;
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
