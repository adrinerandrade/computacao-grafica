using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Collections.Generic;

namespace gcgcg
{
    public class Bbox
    {

        /// <summary>
        /// Distancia mais alta
        /// </summary>
        /// <value></value>
        public double? largerDistanceTop { get; set; }

        /// <summary>
        /// distancia mais baixa
        /// </summary>
        /// <value></value>
        public double? largetDistanceBottom { get; set; }

        /// <summary>
        /// maxima distancia para direita
        /// </summary>
        /// <value></value>
        public double? largerDistanceLeft { get; set; }

        /// <summary>
        /// maxima distancia para a esquerda
        /// </summary>
        /// <value></value>
        public double? largerDistanceRight { get; set; }

        /// <summary>
        /// centro x da bbox
        /// </summary>
        /// <value></value>
        public double centerX { get; set; }

        /// <summary>
        /// centro Y da bbox
        /// </summary>
        /// <value></value>
        public double centerY { get; set; }
        private Bbox() { }
        public Bbox(List<Ponto4D> points)
        {
            BBoxDimensions(points);
        }

        /// <summary>
        /// calcula a dimenção da bbox
        /// </summary>
        /// <param name="points"></param>
        public void BBoxDimensions(List<Ponto4D> points)
        {
            this.largerDistanceTop = null;
            this.largetDistanceBottom = null;
            this.largerDistanceLeft = null;
            this.largerDistanceRight = null;
            foreach (var point in points)
            {
                if (largerDistanceTop == null || largerDistanceTop < point.Y)
                {
                    largerDistanceTop = point.Y;
                }
                if (largetDistanceBottom == null || largetDistanceBottom > point.Y)
                {
                    largetDistanceBottom = point.Y;
                }
                if (largerDistanceLeft == null || largerDistanceLeft > point.X)
                {
                    largerDistanceLeft = point.X;
                }
                if (largerDistanceRight == null || largerDistanceRight < point.X)
                {
                    largerDistanceRight = point.X;
                }
            }
            centerY = (largerDistanceTop.Value + largetDistanceBottom.Value) / 2;
            centerX = (largerDistanceRight.Value + largerDistanceLeft.Value) / 2;
        }

        /// <summary>
        /// clona a bbox
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Desenha a bbox
        /// </summary>
        public void Draw()
        {
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

        /// <summary>
        /// Motra o cursor no centro da bbox
        /// </summary>
        private void CursorShow()
        {
            GL.Color3(Color.Red);
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex3(centerX, centerY + 10, 0);
            GL.Vertex3(centerX, centerY - 10, 0);
            GL.Vertex3(centerX + 10, centerY, 0);
            GL.Vertex3(centerX - 10, centerY, 0);
            GL.End();
        }
    }

}
