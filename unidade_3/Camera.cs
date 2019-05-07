using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace gcgcg
{
  public class Camera : GameWindow
  {

    /// <summary>
    /// Monitora os eventos do GameWindow
    /// </summary>
    private EventObserver eventObserver;

    /// <summary>
    /// Constroi uma nova camera
    /// </summary>
    /// <param name="width">Largura da tela</param>
    /// <param name="height">Altura da tela</param>
    /// <returns>Void</returns>
    public Camera(int width, int height) : base(width, height) {
      this.eventObserver = new EventObserver();
    }

    /// <summary>
    /// Monitora os eventos do mouse
    /// </summary>
    protected override void OnMouseDown(OpenTK.Input.MouseButtonEventArgs e) {
      base.OnMouseDown(e);
      if (e.Mouse.IsButtonDown(OpenTK.Input.MouseButton.Left)) {
        eventObserver.AddKey(Key.MouseLeft);
        eventObserver.SetMouseDown(true);
      }
      if (e.Mouse.IsButtonDown(OpenTK.Input.MouseButton.Right)) {
        eventObserver.AddKey(Key.MouseRight);
        eventObserver.SetMouseDown(true);
      }
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
      GL.Ortho(-400, 400, 400, -400, -1, 1);
    }
    protected override void OnRenderFrame(FrameEventArgs e)
    {
      base.OnRenderFrame(e);
      
      GL.Clear(ClearBufferMask.ColorBufferBit);
      GL.ClearColor(Color.Gray);
      GL.MatrixMode(MatrixMode.Modelview);

      this.SwapBuffers();
    }
  }

}
