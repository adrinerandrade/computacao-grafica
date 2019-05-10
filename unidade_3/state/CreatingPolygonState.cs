using System;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;

namespace gcgcg
{
  public class CreatingPolygonState : IState
  {
    private Mundo mundo;
    private Polygon polygon;
    public CreatingPolygonState(Mundo mundo) {
      this.mundo = mundo;
      this.polygon = new Polygon();
      this.polygon.points4D = new List<Ponto4D>{
        new Ponto4D() { X = Mouse.X, Y = Mouse.Y }
      };
      mundo.AddPolygon(this.polygon);
    }

    public IState Perform(Command command, Mundo mundo)
    {
      if (command.Equals(Command.NEW_POINT)) {
        this.polygon.points4D.Add(new Ponto4D() { X = Mouse.X, Y = Mouse.Y });
      } else if (command.Equals(Command.FINALIZE_POLYGON)) {
        this.polygon.primitive = PrimitiveType.LineLoop;
        this.mundo.polygonSelected = this.polygon;
        return new MainState();
      }
      return this;
    }
  }
}