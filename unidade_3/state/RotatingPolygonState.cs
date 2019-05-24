using System;

namespace gcgcg
{
  public class RotatePolygonState : IState
  {
    private double initialX;
    private double initialY;
    private double lastAngle;
    private double rotationFactor = 1;
    public RotatePolygonState() {
      this.initialX = Mouse.X;
      this.initialY = Mouse.Y;
      this.lastAngle = 0;
    }
    public IState Perform(Command command, Mundo mundo)
    {
      if (command.Equals(Command.MOUSE_MOVE)) {
        if (mundo.polygonSelected != null) {
          mundo.polygonSelected.Rotate(GetAngle());
        } else {
          return new MainState();
        }
      } else if (command.Equals(Command.ROTATE)) {
        return new MainState();
      } else if (command.Equals(Command.ESCAPE)) {
        return new MainState().Perform(command, mundo);
      }
      return this;
    }
    private double GetAngle() {
      var dX = Mouse.X - this.initialX;
      var dY = -(Mouse.Y - this.initialY);

      var inRads = Math.Atan2(dY, dX);;
      if (inRads > 0)
      {
        inRads = inRads;
      } else
      {
        inRads = 2 * Math.PI - inRads;
      }
      var angle = inRads;
      // Console.WriteLine((inRads > 0 ? inRads : (2* Math.PI + inRads)) * 360 / (2* Math.PI));
      
      var radiusFactor = (angle < this.lastAngle ? -this.rotationFactor : (angle > this.lastAngle ? this.rotationFactor : 0));
      this.lastAngle = angle;
      return radiusFactor;
    }
  }
}