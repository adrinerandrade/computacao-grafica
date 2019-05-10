using System;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;

namespace gcgcg
{
  public class SelectPointPolygonSelectedState : IState
  {

    public IState Perform(Command command, Mundo mundo)
    {
      if (command.Equals(Command.SELECT_VERTEX))
      {
        mundo.pointSelected = new Ponto4D()
        {
          X = Mouse.X,
          Y = Mouse.Y
        };
        return this;
      }
      else if (command.Equals(Command.MOUSE_MOVE))
      {
        UpdateVertex(mundo);
        return this;
      }
      else
      {
        mundo.pointSelected = null;
      }
      return new MainState();
    }
    private void UpdateVertex(Mundo mundo)
    {
      Ponto4D point4D = mundo.polygonSelected.pointSelected;
      if (point4D != null)
      {
        point4D.X = Mouse.X;
        point4D.Y = Mouse.Y;
      }
      mundo.polygonSelected.Bbox = null;
    }
  }
}