using System.Collections.Generic;

namespace gcgcg
{
  public class MainState : IState
  {
    /// <summary>
    /// Verifica qual ação deve tomar com base no comando recebido
    /// </summary>
    /// <param name="command"></param>
    /// <param name="mundo"></param>
    /// <returns></returns>
    public IState Perform(Command command, Mundo mundo)
    {
      if (command.Equals(Command.ESCAPE)) {
        mundo.polygonSelected = null;
      } else if (command.Equals(Command.SELECT_POLYGON)) {
        var selectedPolygon = PolygonSelector.GetSelected(mundo.polygons, Mouse.X, Mouse.Y);
        if (selectedPolygon != null)
        {
          mundo.polygonSelected = selectedPolygon;
        }
      } else if (command.Equals(Command.MOVE)) {
        if (mundo.polygonSelected != null) {
          return new TranslatePolygonState(mundo);
        }
      } else if (command.Equals(Command.SCALE)) {
        if (mundo.polygonSelected != null) {
          return new ScalePolygonState();
        }
      } else if (command.Equals(Command.ROTATE)) {
        if (mundo.polygonSelected != null) {
          return new RotatePolygonState(mundo);
        }
      } else if (command.Equals(Command.CHILD)) {
        if (mundo.polygonSelected != null) {
          return new ChildState(mundo);
        }
      } else if (command.Equals(Command.NEW_POINT)) {
        return new CreatingPolygonState(mundo);
      } else if (command.Equals(Command.SELECT_VERTEX)) {
        if (mundo.polygonSelected != null) {
          return new PointPolygonSelectedState().Perform(command, mundo);
        }
      } else if (command.Equals(Command.DELETE)) {
        return new DeletingPolygonState().Perform(command, mundo);
      } else if (command.Equals(Command.CHANGE_PRIMITIVE)) {
        return new PrimitiveState().Perform(command, mundo);
      } else if (
        command.Equals(Command.CHANGE_BLUE) ||
        command.Equals(Command.CHANGE_COLOR_RED) ||
        command.Equals(Command.CHANGE_GREEN)
      ) {
        return new ColorState().Perform(command, mundo);
      }
      return this;
    }

  }
}