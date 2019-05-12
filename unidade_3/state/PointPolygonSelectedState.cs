using System;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.Threading;

namespace gcgcg
{
  public class PointPolygonSelectedState : IState
  {
    private bool canMove = false;
    public IState Perform(Command command, Mundo mundo)
    {
      if (command.Equals(Command.SELECT_VERTEX))
      {
        mundo.polygonSelected.SelectNearestVertex(Mouse.X, Mouse.Y);
      } 
      else if (command.Equals(Command.MOVE))
      {
        canMove = !canMove;
        if (canMove)
        {
          UpdateSelectedVertex(mundo);
        }
      }
      else if (command.Equals(Command.MOUSE_MOVE))
      {
        if (canMove)
        {
          UpdateSelectedVertex(mundo);
        }
      }
      else if (command.Equals(Command.DELETE))
      {
        RemoveSelectedVertex(mundo);
        return new MainState();
      }
      else if (command.Equals(Command.ESCAPE)) {
        mundo.polygonSelected.DeselectVertex();
        return new MainState();
      }
      return this;
    }

    private static void RemoveSelectedVertex(Mundo mundo)
    {
      var selectedVertex = mundo.polygonSelected.GetSelectedVertex();
      mundo.polygonSelected.RemoveVertex(selectedVertex);
    }

    private static void UpdateSelectedVertex(Mundo mundo)
    {
      var selectedVertex = mundo.polygonSelected.GetSelectedVertex();
      mundo.polygonSelected.UpdateVertexLocation(selectedVertex, Mouse.X, Mouse.Y);
    }
  }
}