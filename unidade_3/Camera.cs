using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Collections.Generic;

namespace gcgcg
{
  public class Camera : GameWindow
  {

    /// <summary>
    /// Monitora os eventos do GameWindow
    /// </summary>
    private EventObserver eventObserver;
    private Mundo mundo;

    /// <summary>
    /// Constroi uma nova camera
    /// </summary>
    /// <param name="width">Largura da tela</param>
    /// <param name="height">Altura da tela</param>
    /// <returns>Void</returns>
    public Camera(int width, int height, Mundo mundo) : base(width, height) {
      this.mundo = mundo;
      this.eventObserver = new EventObserver(this.mundo);
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

      mundo.Draw();

      this.SwapBuffers();
    }
  }

}
