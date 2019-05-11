using System;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.Threading;

namespace gcgcg
{
  public class PointPolygonSelectedState : IState
  {
    public IState Perform(Command command, Mundo mundo)
    {
      if (command.Equals(Command.SELECT_VERTEX))
      {
        SelectVertex(mundo);
        return this;
      }
      else if (command.Equals(Command.MOUSE_MOVE))
      {
        UpdateVertex(mundo);
        return this;
      }
      else if (command.Equals(Command.DELETE_VERTEX))
      {
        DeleteVertex(mundo);
      }
      return new MainState();
    }
    private void SelectVertex(Mundo mundo)
    {
      mundo.pointSelected = new Ponto4D()
      {
        X = Mouse.X,
        Y = Mouse.Y
      };
      Thread t = new Thread(
        () =>
        {
            Thread.Sleep(150);
            UpdateVertex(mundo);
        }
    );
    t.Start();
    }
    private void DeleteVertex(Mundo mundo)
    {
      mundo.polygonSelected.points4D.Remove(mundo.polygonSelected.pointSelected);
      mundo.polygonSelected.pointSelected = null;
      BboxRefresh(mundo);
    }
    private void UpdateVertex(Mundo mundo)
    {
      Ponto4D point4D = mundo.polygonSelected.pointSelected;
      if (point4D != null)
      {
        point4D.X = Mouse.X;
        point4D.Y = Mouse.Y;
      }
      BboxRefresh(mundo);
    }
    private void BboxRefresh(Mundo mundo)
    {
      mundo.polygonSelected.Bbox = null;
    }
  }
}