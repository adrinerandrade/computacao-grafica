using System;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;

namespace gcgcg
{
  public class DeletingPolygonState : IState
  {
    /// <summary>
    /// Verfica se o comando é para remover um poligono por completo.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="mundo"></param>
    /// <returns></returns>
    public IState Perform(Command command, Mundo mundo)
    {
      if (command.Equals(Command.DELETE)) {
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