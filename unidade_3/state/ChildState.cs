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
        if (hover != this.parent) {
          mundo.polygonSelected = hover;
        }
      } else if (command.Equals(Command.CLICK))
      {
        var selected = PolygonSelector.GetSelected(mundo.polygons, Mouse.X, Mouse.Y);
        if (selected != null && selected != this.parent)
        {
          mundo.RemovePolygon(selected);
          this.parent.children.Add(selected);
          mundo.polygonSelected = this.parent;
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