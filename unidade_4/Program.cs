/*
  Autor: Dalton Solano dos Reis
*/

using System;
using System.Drawing;
using System.Drawing.Imaging;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace gcgcg
{
  class Render : GameWindow
  {
    Mundo mundo;
    MusicExecution musicExecution;
    Camera camera = new Camera();
    Vector3 eye = Vector3.Zero, target = Vector3.Zero, up = Vector3.UnitY;

    public static int tabTexture;
    public static int noteTexture;

    public Render(int width, int height) : base(width, height)
    {
      this.mundo = Mundo.getInstance();
      this.musicExecution = new MusicExecution("doremifa");
      this.mundo.NewMusicExecution(musicExecution);
      this.musicExecution.OnStop(() => this.Close());
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);

      GL.Enable(EnableCap.DepthTest);

      eye.X = 0;
      eye.Y = 10;
      eye.Z = 32;

      GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
      GL.GenTextures(1, out Render.tabTexture);
      GL.BindTexture(TextureTarget.Texture2D, Render.tabTexture);
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

      Bitmap tabBitmap = new Bitmap(@"./textureRepository/logoGCG.png");
      BitmapData tabData = tabBitmap.LockBits(new System.Drawing.Rectangle(0, 0, tabBitmap.Width, tabBitmap.Height),
      ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

      GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, tabData.Width, tabData.Height, 0,
      OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, tabData.Scan0);

      tabBitmap.UnlockBits(tabData);

      GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
      GL.GenTextures(1, out Render.noteTexture);
      GL.BindTexture(TextureTarget.Texture2D, Render.noteTexture);
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

      Bitmap bitmap = new Bitmap(@"./textureRepository/note.png");
      BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
      ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

      GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
      OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

      bitmap.UnlockBits(data);

      this.musicExecution.Start();
    }

    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);

      GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);

      Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1.0f, 80f);
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
        this.mundo.notePressed(0);
      else
        if (e.Key == Key.S)
        this.mundo.notePressed(1);
      else
        if (e.Key == Key.D)
        this.mundo.notePressed(2);
      else
        if (e.Key == Key.F)
        this.mundo.notePressed(3);
      else
        if (e.Key == Key.G)
        this.mundo.notePressed(4);
    }
    protected override void OnKeyUp(OpenTK.Input.KeyboardKeyEventArgs e)
    {
      if (e.Key == Key.A)
        this.mundo.noteReleased(0);
      else
        if (e.Key == Key.S)
        this.mundo.noteReleased(1);
      else
        if (e.Key == Key.D)
        this.mundo.noteReleased(2);
      else
        if (e.Key == Key.F)
        this.mundo.noteReleased(3);
      else
        if (e.Key == Key.G)
        this.mundo.noteReleased(4);
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
