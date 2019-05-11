using System;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;

namespace gcgcg
{
  public class PrimitiveState : IState
  {
    public IState Perform(Command command, Mundo mundo)
    {
      if (command.Equals(Command.CHANGE_PRIMITIVE)) {
        if (mundo.polygonSelected.primitive == PrimitiveType.LineStrip) {
          mundo.polygonSelected.primitive = PrimitiveType.LineLoop;
        } else {
          mundo.polygonSelected.primitive = PrimitiveType.LineStrip;
        }
      }
      return new MainState();
    }
  }
}