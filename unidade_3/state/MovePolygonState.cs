namespace gcgcg
{
  class MovePolygonState : IState
  {
    public IState Perform(Command command, Mundo mundo)
    {
      if (command.Equals(Command.MOUSE_MOVE)) {
        mundo.polygonSelected.Translation(Mouse.X, Mouse.Y);
      } else if (command.Equals(Command.MOVE)) {
        return new MainState();
      } else if (command.Equals(Command.ESCAPE)) {
        return new MainState().Perform(command, mundo);
      }
      return this;
    }
  }
}