namespace gcgcg
{
  public class ChildState : IState
  {
    private Polygon parent;
    public ChildState(Mundo mundo)
    {
      this.parent = mundo.polygonSelected;
      mundo.polygonSelected = null;
    }

    public IState Perform(Command command, Mundo mundo)
    {
      if (command.Equals(Command.MOUSE_MOVE))
      { 
        var hover = PolygonSelector.GetSelected(mundo.polygons, Mouse.X, Mouse.Y);
        if (hover != null) {
          mundo.polygonSelected = hover;
        }
      } else if (command.Equals(Command.CLICK))
      {
        var child = PolygonSelector.GetSelected(mundo.polygons, Mouse.X, Mouse.Y);
        if (child != null)
        {
          mundo.RemovePolygon(child);
          this.parent.children.Add(child);
          mundo.polygonSelected = null;
          return new MainState();
        }
      } else if (command.Equals(Command.ESCAPE))
      {
        return new MainState();
      }
      return this;
    }

  }
}