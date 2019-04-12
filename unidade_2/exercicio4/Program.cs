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
    private PrimitiveType[] primitives = new PrimitiveType[] { 
      PrimitiveType.Points,
      PrimitiveType.Lines,
      PrimitiveType.LineLoop,
      PrimitiveType.LineStrip,
      PrimitiveType.Triangles,
      PrimitiveType.TriangleFan,
      PrimitiveType.Quads,
      PrimitiveType.QuadStrip,
      PrimitiveType.Polygon
    };
    private int index = 0;
    private int xMin = -400;
    private int xMax = 400;
    private int yMin = -400;
    private int yMax = 400;
    private int zMin = -1;
    private int zMax = 1;
    private int keyPressedTimeout = 200;
    private bool listenKeyPress = true;
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
      GL.Ortho(this.xMin, this.xMax, this.yMin, this.yMax, this.zMin, this.zMax);
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
      base.OnRenderFrame(e);
      Console.WriteLine("[4] .. OnRenderFrame");

      GL.Clear(ClearBufferMask.ColorBufferBit);
      GL.ClearColor(Color.Gray);
      GL.MatrixMode(MatrixMode.Modelview);

      mundo.SRU3D();
      mundo.Desenha(getPrimitive());

      this.SwapBuffers();
    }

    public PrimitiveType getPrimitive()
    {
      OpenTK.Input.KeyboardState keyboardState = OpenTK.Input.Keyboard.GetState();
      if (this.listenKeyPress && keyboardState.IsKeyDown(OpenTK.Input.Key.Space)) {
        this.listenKeyPress = false;
        this.index++;
        if (this.index >= this.primitives.Length) this.index = 0;
        Task.Run(async () => {
          await Task.Delay(200);
          this.listenKeyPress = true;
        });
      }
      return this.primitives[this.index];
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
