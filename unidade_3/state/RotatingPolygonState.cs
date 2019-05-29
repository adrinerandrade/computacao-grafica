using System;

namespace gcgcg
{
  public class RotatePolygonState : IState
  {
    private double initialX;
    private double initialY;
    private double lastAngle;
    private double rotationFactor = 1;

    /// <summary>
    /// Construtror Inicializa os valor iniciais do poligono
    /// </summary>
    /// <param name="mundo"></param>
    public RotatePolygonState(Mundo mundo) {
      var bbox = mundo.polygonSelected.GetBBox();
      this.initialX = bbox.centerX;
      this.initialY = bbox.centerY;
      this.lastAngle = 0;
    }

    /// <summary>
    /// Metodo para iniciar uma rotação
    /// </summary>
    /// <param name="command"></param>
    /// <param name="mundo"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Retorna o angulo bom base no mouse e valor inicial do poligono
    /// </summary>
    /// <returns></returns>
    private double GetAngle() {
      var dX = Mouse.X - this.initialX;
      var dY = -(Mouse.Y - this.initialY);

      var radian = Math.Atan2(dY, dX);
      var angle = 180 + radian * (180 / Math.PI);

      var result = angle > this.lastAngle ? -1 : (angle < this.lastAngle ? 1 : 0);
      this.lastAngle = angle;
      return result;
    }
  }
}