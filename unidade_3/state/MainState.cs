using System.Collections.Generic;

namespace gcgcg
{
  public class MainState : IState
  {
    public IState Perform(Command command, Mundo mundo)
    {
      return this;
    }

  }
}