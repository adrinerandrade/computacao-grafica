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

    public Mundo()
    {
      camera = new Camera(600, 600, this);
      camera.Run();
      camera.Run(1.0 / 60.0);
    }

    public void AddPolygon(Polygon polygon) {
      this.polygons.Add(polygon);
    }
    public void RemovePolygon(Polygon polygon) {
      this.RemovePolygonRecursive(this.polygons, polygon);
    }
    private void RemovePolygonRecursive(List<Polygon> polygons, Polygon polygon) {
      for (var i = 0; i < polygons.Count; i++) {
        if (polygons[i] == polygon) {
          polygons.RemoveAt(i);
          return;
        }
        this.RemovePolygonRecursive(polygons[i].children, polygon);
      }
    }
    public void Draw()
    {
      for (var i = 0; i < this.polygons.Count; i++)
      {
        polygons[i].Draw();
      }
      if (polygonSelected != null) {
        polygonSelected.DrawBBox();
      }
    }

  }

}