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
        mundo.polygonSelected.SelectNearestVertex(Mouse.X, Mouse.Y);
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
    private void DeleteVertex(Mundo mundo)
    {
      var selectedVertex = mundo.polygonSelected.GetSelectedVertex();
      mundo.polygonSelected.RemoveVertex(selectedVertex);
    }
    private void UpdateVertex(Mundo mundo)
    {
      var selectedVertex = mundo.polygonSelected.GetSelectedVertex();
      mundo.polygonSelected.UpdateVertexLocation(selectedVertex, Mouse.X, Mouse.Y);
    }
  }
}