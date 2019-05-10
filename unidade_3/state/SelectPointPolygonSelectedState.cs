using System;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;

namespace gcgcg
{
  public class SelectPointPolygonSelectedState : IState
  {

    public IState Perform(Command command, Mundo mundo)
    {
      if (command.Equals(Command.SELECT_VERTEX)) {
        mundo.pointSelected = new Ponto4D() {
          X = Mouse.X, Y = Mouse.Y
        };
        return new MainState();
      }
      return this;
    }
  }
}