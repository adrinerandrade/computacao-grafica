using System.Collections.Generic;

namespace gcgcg
{
  public interface IState
  {
    IState Perform(Command command, Mundo mundo);
  }
}