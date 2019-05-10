using System;
using System.Collections.Generic;

namespace gcgcg
{
  public class MainState : IState
  {
    public IState Perform(Command command, Mundo mundo)
    {
      Console.Write(command.Equals(Command.NEW_POINT));
      if (command.Equals(Command.NEW_POINT)) {
        return new CreatingPolygonState(mundo);
      }
      return this;
    }

  }
}