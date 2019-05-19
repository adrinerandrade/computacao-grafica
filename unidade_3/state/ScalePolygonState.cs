using System;

namespace gcgcg
{
  public class ScalePolygonState : IState
  {
    private double initialX;
    private double initialY;
    public ScalePolygonState()
    {
      this.initialY = Mouse.Y - 100;
    }
    public IState Perform(Command command, Mundo mundo)
    {
      if (command.Equals(Command.MOUSE_MOVE)) {
        if (mundo.polygonSelected != null) {
          mundo.polygonSelected.Scale(this.getScale());
        } else {
          return new MainState();
        }
      } else if (command.Equals(Command.SCALE)) {
        return new MainState();
      } else if (command.Equals(Command.ESCAPE)) {
        return new MainState().Perform(command, mundo);
      }
      return this;
    }
    private double getScale() {
      var diff = (Mouse.Y - this.initialY) / 100;
      return diff < 0 ? 0 : diff;
    }
  }
}