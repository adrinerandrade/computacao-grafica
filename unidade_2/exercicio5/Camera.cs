using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
namespace gcgcg
{
  public class Camera
    {
        GameWindow window;

        private double left = 0.0;
        private double right = 0.0;
        private double top = 0.0;
        private double bottom = 0.0;

        private double panFactor = 1.0;
        private double zoomFactor = 0.9;

        private IKeyDownListener keyDownListener;

        public Camera(GameWindow window)
        {
            this.window = window;
            window.KeyDown += OnKeyDown;
        }

        public void Ortho()
        {
            GL.Ortho(this.left, this.right, this.bottom, this.top, -1, 1);//camera
        }

        public void SetOnKeyDownListener(IKeyDownListener listener)
        {
            this.keyDownListener = listener;
        }

        public void InitOrtho(double left, double right, double bottom, double top)
        {
            this.left = left;
            this.right = right;
            this.bottom = bottom;
            this.top = top;
        }

        private void OnKeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (this.keyDownListener != null)
                this.keyDownListener.OnKeyPressed(e);
        }
    }

}