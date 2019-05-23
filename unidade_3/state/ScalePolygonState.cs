using System;

namespace gcgcg
{
  public class ScalePolygonState : IState
  {
    private double lastMouseY;
    private double plusScaleFactor;
    private double minusScaleFactor;
    public ScalePolygonState()
    {
      this.plusScaleFactor = 1.02;
      this.minusScaleFactor = 1 / this.plusScaleFactor ;
      this.lastMouseY = Mouse.Y - 100;
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
      var scaleFactor = (Mouse.Y < this.lastMouseY ? this.minusScaleFactor : (Mouse.Y > this.lastMouseY ? this.plusScaleFactor : 1));
      this.lastMouseY = Mouse.Y;
      return scaleFactor;
    }
  }
}