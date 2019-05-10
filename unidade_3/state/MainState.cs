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
        return new SelectPointPolygonSelectedState().Perform(command, mundo);
      }
      return this;
    }

  }
}