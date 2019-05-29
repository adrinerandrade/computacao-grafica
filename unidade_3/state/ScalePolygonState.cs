using System;

namespace gcgcg
{
  public class ScalePolygonState : IState
  {
    private double lastMouseY;
    private double plusScaleFactor;
    private double minusScaleFactor;

    /// <summary>
    /// Construtor inicializa o fator de scala
    /// e salva o ultimo estado do mouse
    /// </summary>
    public ScalePolygonState()
    {
      this.plusScaleFactor = 1.02;
      this.minusScaleFactor = 1 / this.plusScaleFactor ;
      this.lastMouseY = Mouse.Y - 100;
    }

    /// <summary>
    /// Verifica se é para trocar a escala de um poligono
    /// </summary>
    /// <param name="command"></param>
    /// <param name="mundo"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Retorna a nova escala do poligono com base nas definições do construtor
    /// </summary>
    /// <returns></returns>
    private double getScale() {
      var scaleFactor = (Mouse.Y < this.lastMouseY ? this.minusScaleFactor : (Mouse.Y > this.lastMouseY ? this.plusScaleFactor : 1));
      this.lastMouseY = Mouse.Y;
      return scaleFactor;
    }
  }
}