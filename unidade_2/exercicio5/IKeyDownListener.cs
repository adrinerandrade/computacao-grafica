using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
namespace gcgcg
{
  public interface IKeyDownListener
    {
         void OnKeyPressed(KeyboardKeyEventArgs key);
    }

}