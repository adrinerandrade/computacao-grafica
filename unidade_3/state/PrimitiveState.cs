using System;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;

namespace gcgcg
{
  public class PrimitiveState : IState
  {
    /// <summary>
    /// Altera a primitiva
    /// </summary>
    /// <param name="command"></param>
    /// <param name="mundo"></param>
    /// <returns></returns>
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