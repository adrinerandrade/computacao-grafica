using System;
using System.Drawing;
using System.Threading;
using OpenTK;
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
      this.mundo.polygonSelected = null;
      this.polygon = new Polygon(new List<Ponto4D> {
        new Ponto4D() { X = Mouse.X, Y = Mouse.Y },
        new Ponto4D() { X = Mouse.X, Y = Mouse.Y }
      });
      mundo.AddPolygon(this.polygon);
    }
    public IState Perform(Command command, Mundo mundo)
    {
      if (command.Equals(Command.NEW_POINT)) {
        this.polygon.AddVertex(new Ponto4D() { X = Mouse.X, Y = Mouse.Y });
      }else if (command.Equals(Command.MOUSE_MOVE)) {
        this.polygon.UpdateVertexLocation(this.polygon.VertexCount() - 1, Mouse.X, Mouse.Y);
      } else if (command.Equals(Command.FINALIZE_POLYGON)) {
        this.polygon.RemoveVertex(this.polygon.VertexCount() - 1);
        if (this.polygon.VertexCount() < 2) {
          Console.WriteLine("Não adicionado");
          this.mundo.polygons.Remove(this.polygon);
        } else {
          this.polygon.primitive = PrimitiveType.LineLoop;
          this.mundo.polygonSelected = this.polygon;  
        }
        return new MainState();
      }
      return this;
    }
  }
}