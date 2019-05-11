using System;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;

namespace gcgcg
{
  public class DeletingPolygonState : IState
  {
    public IState Perform(Command command, Mundo mundo)
    {
      if (command.Equals(Command.DELETE_POLYGON)) {
        mundo.polygons.Remove(mundo.polygonSelected);
        if (mundo.polygons.Count > 0) {
          mundo.polygonSelected = mundo.polygons[mundo.polygons.Count - 1];
        } else {
          mundo.polygonSelected =  null;
        }
      }
      return new MainState();
    }
  }
}