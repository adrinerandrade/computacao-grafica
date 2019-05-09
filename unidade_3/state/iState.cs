using System.Collections.Generic;

namespace gcgcg
{
  public interface IState
  {
    IState Perform(List<Key> keys, Mundo mundo);
  }
}