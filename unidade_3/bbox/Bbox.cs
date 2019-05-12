using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Collections.Generic;

namespace gcgcg
{
  public class Bbox
  {
    public double? largerDistanceTop { get; set; }
    public double? largetDistanceBottom { get; set; }
    public double? largerDistanceLeft { get; set; }
    public double? largerDistanceRight { get; set; }

    public double centerX { get; set; }
    public double centerY {get; set; }
    private Bbox() {}
    public Bbox(List<Ponto4D> points) {
      BBoxDimensions(points);
    }
    private void BBoxDimensions(List<Ponto4D> points) {
      foreach (var point in points)
      {
        if (largerDistanceTop == null || largerDistanceTop < point.Y) {
          largerDistanceTop = point.Y;
        }
        if (largetDistanceBottom == null || largetDistanceBottom > point.Y) {
          largetDistanceBottom = point.Y;
        }
        if (largerDistanceLeft == null || largerDistanceLeft > point.X) {
          largerDistanceLeft = point.X;
        }
        if (largerDistanceRight == null || largerDistanceRight < point.X) {
          largerDistanceRight = point.X;
        }
      }
    }
    public Bbox Clone()
    {
      var bBox = new Bbox();
      bBox.centerX = this.centerX;
      bBox.centerY = this.centerY;
      bBox.largerDistanceLeft = this.largerDistanceLeft;
      bBox.largerDistanceRight = this.largerDistanceRight;
      bBox.largerDistanceTop = this.largerDistanceTop;
      bBox.largetDistanceBottom = this.largetDistanceBottom;
      return bBox;
    }
    public void Draw() {
      GL.Color3(Color.Yellow);
      GL.Begin(PrimitiveType.Lines);
        GL.Vertex3(largerDistanceLeft.Value - 10, largerDistanceTop.Value + 10, 0);
        GL.Vertex3(largerDistanceRight.Value + 10, largerDistanceTop.Value + 10, 0);

        GL.Vertex3(largerDistanceLeft.Value - 10, largerDistanceTop.Value + 10, 0);
        GL.Vertex3(largerDistanceLeft.Value - 10, largetDistanceBottom.Value - 10, 0);

        GL.Vertex3(largerDistanceRight.Value + 10, largerDistanceTop.Value + 10, 0);
        GL.Vertex3(largerDistanceRight.Value + 10, largetDistanceBottom.Value - 10, 0);
        
        GL.Vertex3(largerDistanceLeft.Value - 10, largetDistanceBottom.Value - 10, 0);
        GL.Vertex3(largerDistanceRight.Value + 10, largetDistanceBottom.Value - 10, 0);
      GL.End();
      CursorShow();
    }
    private void CursorShow() {
      CenterBboxCaculation();
      GL.Color3(Color.Red);
      GL.Begin(PrimitiveType.Lines);
        GL.Vertex3(centerX, centerY + 10, 0);
        GL.Vertex3(centerX, centerY - 10, 0);
        GL.Vertex3(centerX + 10, centerY, 0);
        GL.Vertex3(centerX - 10, centerY, 0);
      GL.End();
    }
    private void CenterBboxCaculation() {
      centerY = (largerDistanceTop.Value + largetDistanceBottom.Value) / 2;
      centerX = (largerDistanceRight.Value + largerDistanceLeft.Value) / 2;
    }
  }

}
