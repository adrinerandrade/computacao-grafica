using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace gcgcg
{
    public class Mundo
    {

        /// <summary>
        /// Poligonos plotados na tela
        /// </summary>
        public List<Polygon> polygons { get; set; } = new List<Polygon>();

        /// <summary>
        /// Poligono atualmente selecionado
        /// </summary>
        public Polygon polygonSelected { get; set; }

        /// <summary>
        /// Instancia da camera
        /// </summary>
        private Camera camera;
        /// <summary>
        /// Inicia uma camera
        /// </summary>
        public Mundo()
        {
            camera = new Camera(600, 600, this);
            camera.Run();
            camera.Run(1.0 / 60.0);
        }

        /// <summary>
        /// Adiciona um poligono na lista
        /// </summary>
        /// <param name="polygon"></param>
        public void AddPolygon(Polygon polygon)
        {
            this.polygons.Add(polygon);
        }

        /// <summary>
        /// Remove um poligono da lista
        /// </summary>
        /// <param name="polygon"></param>
        public void RemovePolygon(Polygon polygon)
        {
            this.RemovePolygonRecursive(this.polygons, polygon);
        }

        /// <summary>
        /// Removo os poligonos recusivamente com seus filhos
        /// </summary>
        /// <param name="polygons"></param>
        /// <param name="polygon"></param>
        private void RemovePolygonRecursive(List<Polygon> polygons, Polygon polygon)
        {
            for (var i = 0; i < polygons.Count; i++)
            {
                if (polygons[i] == polygon)
                {
                    polygons.RemoveAt(i);
                    return;
                }
                this.RemovePolygonRecursive(polygons[i].children, polygon);
            }
        }

        /// <summary>
        /// Desenha os poligonos na tela
        /// </summary>
        public void Draw()
        {
            for (var i = 0; i < this.polygons.Count; i++)
            {
                polygons[i].Draw();
            }
            if (polygonSelected != null)
            {
                polygonSelected.DrawBBox();
            }
        }

    }

}