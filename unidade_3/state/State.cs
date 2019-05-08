using System.Collections.Generic;

namespace gcgcg
{
    public interface State
    {
        State Perform(List<Key> keys, Mundo mundo);
    }
}