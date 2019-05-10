using System.Collections.Generic;
using System.Linq;

namespace gcgcg
{
  public class Command
  {
    public static readonly Command NEW_POINT = new Command(new Key[] { Key.B, Key.MouseRight });
    public static readonly Command FINALIZE_POLYGON = new Command(new Key[] { Key.Space });
    public static readonly Command SELECT_POLYGON = new Command(new Key[] { Key.B, Key.MouseLeft });
    public static readonly Command SELECT_VERTEX = new Command(new Key[] { Key.ControlLeft, Key.B, Key.MouseLeft });
    public static readonly Command CHANGE_PRIMITIVE = new Command(new Key[] { Key.P });
    public static readonly Command CHANGE_COLOR_RED = new Command(new Key[] { Key.R });
    public static readonly Command CHANGE_GREEN = new Command(new Key[] { Key.G });
    public static readonly Command CHANGE_BLUE = new Command(new Key[] { Key.A });
    public static readonly Command MOUSE_MOVE = new Command(new Key[] { Key.MouseMove });
    public static readonly Command NONE = new Command(new Key[] { });
    private static IEnumerable<Command> Values
    {
      get
      {
        yield return NEW_POINT;
        yield return FINALIZE_POLYGON;
        yield return SELECT_POLYGON;
        yield return SELECT_VERTEX;
        yield return CHANGE_PRIMITIVE;
        yield return CHANGE_COLOR_RED;
        yield return CHANGE_GREEN;
        yield return CHANGE_BLUE;
        yield return MOUSE_MOVE;
        yield return NONE;
      }
    }
    public static Command GetCommand(List<Key> keys)
    {
      foreach (var command in Values)
      {
        if (Enumerable.SequenceEqual(keys, command.keys)) {
          return command;
        }
      }
      return NONE;
    }
    private Key[] keys { get; }
    private Command(Key[] keys)
    {
      this.keys = keys;
    }
  }
}