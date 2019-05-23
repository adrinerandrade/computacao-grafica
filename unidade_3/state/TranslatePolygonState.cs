namespace gcgcg
{
  class TranslatePolygonState : IState
  {
    public TranslatePolygonState(Mundo mundo)
    {
      if (mundo.polygonSelected != null) {
        mundo.polygonSelected.Translation(Mouse.X, Mouse.Y);
      }
    }
    public IState Perform(Command command, Mundo mundo)
    {
      if (command.Equals(Command.MOUSE_MOVE)) {
        if (mundo.polygonSelected != null) {
          mundo.polygonSelected.Translation(Mouse.X, Mouse.Y);
        } else {
          return new MainState();
        }
      } else if (command.Equals(Command.MOVE)) {
        return new MainState();
      } else if (command.Equals(Command.ESCAPE)) {
        return new MainState().Perform(command, mundo);
      }
      return this;
    }
  }
}