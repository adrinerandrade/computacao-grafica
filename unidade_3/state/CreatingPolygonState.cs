using System;
using System.Collections.Generic;

namespace gcgcg
{
  public class CreatingPolygonState : IState
  {
    private Mundo mundo;
    private Polygon polygon;
    public CreatingPolygonState(Mundo mundo) {
      this.mundo = mundo;
      var mouse = OpenTK.Input.Mouse.GetState();
      this.polygon = new Polygon();
      this.polygon.points4D = new List<Ponto4D>{
        new Ponto4D() { X = mouse.X, Y = mouse.Y },
        new Ponto4D() { X = mouse.X, Y = mouse.Y }
      };
      mundo.AddPolygon(this.polygon);
    }

    public IState Perform(Command command, Mundo mundo)
    {
      if (command.Equals(Command.NEW_POINT)) {
        var mouse = OpenTK.Input.Mouse.GetState();
        this.polygon.points4D.Add(new Ponto4D() { X = mouse.X, Y = mouse.Y });
      } else if (command.Equals(Command.FINALIZE_POLYGON)) {
        return new MainState();
      }
      return this;
    }
  }
}