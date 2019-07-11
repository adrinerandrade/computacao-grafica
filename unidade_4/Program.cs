/*
  Autor: Dalton Solano dos Reis
*/

using System;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Threading;

namespace gcgcg
{
  class Render : GameWindow
  {
    Mundo mundo;
    MusicExecution musicExecution;
    NoteMatcherController noteMatcherController;
    Camera camera = new Camera();
    Vector3 eye = Vector3.Zero, target = Vector3.Zero, up = Vector3.UnitY;

    public static int tabTexture;
    public static int noteTexture;

    public bool light { get; set; } = false;

    public Render(int width, int height) : base(width, height)
    {
      this.mundo = Mundo.getInstance();
      this.noteMatcherController = new NoteMatcherController();
      this.musicExecution = new MusicExecution("doremifa");
      this.mundo.NewMusicExecution(noteMatcherController, musicExecution);
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

      // Enable Light 0 and set its parameters.
      GL.Light(LightName.Light0, LightParameter.Position, new float[] { 0.0f, 2.0f, 0.0f });
      GL.Light(LightName.Light0, LightParameter.Ambient, new float[] { 0.3f, 0.3f, 0.3f, 1.0f });
      GL.Light(LightName.Light0, LightParameter.Diffuse, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
      GL.Light(LightName.Light0, LightParameter.Specular, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
      GL.Light(LightName.Light0, LightParameter.SpotExponent, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
      GL.LightModel(LightModelParameter.LightModelAmbient, new float[] { 0.2f, 0.2f, 0.2f, 1.0f });
      GL.LightModel(LightModelParameter.LightModelTwoSide, 1);
      GL.LightModel(LightModelParameter.LightModelLocalViewer, 1);

      // Use GL.Material to set your object's material parameters.
      GL.Material(MaterialFace.Back, MaterialParameter.Ambient, new float[] { 0.3f, 0.3f, 0.3f, 1.0f });
      GL.Material(MaterialFace.Back, MaterialParameter.Diffuse, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
      GL.Material(MaterialFace.Back, MaterialParameter.Specular, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
      GL.Material(MaterialFace.Back, MaterialParameter.Emission, new float[] { 0.0f, 0.0f, 0.0f, 1.0f });

      this.musicExecution.Start();
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

      if (light) {
        GL.Enable(EnableCap.Lighting);
        GL.Enable(EnableCap.Light0);
      }

      GL.Enable(EnableCap.ColorMaterial);
      mundo.Desenha();
      if (light) {
        GL.Disable(EnableCap.Lighting);
        GL.Disable(EnableCap.Light0);
      }
      this.SwapBuffers();
    }

    protected override void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
    {
      if (e.Key == Key.A)
      {
        this.mundo.notePressed(0);
        this.noteMatcherController.notePlayed(0);
      }
      else if (e.Key == Key.S)
      {
        this.mundo.notePressed(1);
        this.noteMatcherController.notePlayed(1);
      }
      else if (e.Key == Key.D)
      {
        this.mundo.notePressed(2);
        this.noteMatcherController.notePlayed(2);
      }
      else if (e.Key == Key.F)
      {
        this.mundo.notePressed(3);
        this.noteMatcherController.notePlayed(3);
      }
      else if (e.Key == Key.G)
      {
        this.mundo.notePressed(4);
        this.noteMatcherController.notePlayed(4);
      } else if (e.Key == Key.L) {
        this.light = !this.light;
      }
    }
    protected override void OnKeyUp(OpenTK.Input.KeyboardKeyEventArgs e)
    {
      if (e.Key == Key.A)
      {
        this.mundo.noteReleased(0);
        this.noteMatcherController.noteReleased(0);
      }
      else if (e.Key == Key.S)
      {
        this.mundo.noteReleased(1);
        this.noteMatcherController.noteReleased(1);
      }
      else if (e.Key == Key.D)
      {
        this.mundo.noteReleased(2);
        this.noteMatcherController.noteReleased(2);
      }
      else if (e.Key == Key.F)
      {
        this.mundo.noteReleased(3);
        this.noteMatcherController.noteReleased(3);
      }
      else if (e.Key == Key.G)
      {
        this.mundo.noteReleased(4);
        this.noteMatcherController.noteReleased(4);
      }
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
      Thread musicCallback = new Thread(MusicPlayCallback);
      musicCallback.IsBackground = true;
      musicCallback.Start();
      Render window = new Render(600, 600);
      window.Run();
      window.Run(1.0 / 60.0);
    }

    public static void MusicPlayCallback()
    {
      while (true)
      {
        if (MusicTimer.threadMusic != null)
        {
          Console.WriteLine("foi");
          MusicTimer.threadMusic.Start();
          break;
        }
      }
    }
  }

}
