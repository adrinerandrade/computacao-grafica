using System;
using System.Collections.Generic;

namespace gcgcg
{
  public class MainState : IState
  {
    public IState Perform(Command command, Mundo mundo)
    {
      if (command.Equals(Command.NEW_POINT)) {
        return new CreatingPolygonState(mundo);
      } else if (command.Equals(Command.SELECT_VERTEX)) {
        return new PointPolygonSelectedState().Perform(command, mundo);
      } else if (command.Equals(Command.DELETE_POLYGON)) {
        return new DeletingPolygonState().Perform(command, mundo);
      } else if (command.Equals(Command.DELETE_VERTEX)) {
        return new PointPolygonSelectedState().Perform(command, mundo);
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