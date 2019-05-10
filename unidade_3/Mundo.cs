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

    public Ponto4D pointSelected { get; set; }

    /// <summary>
    /// Informaçào se o poligono é filho de outro poligono
    /// </summary>
    private bool isChildren;

    /// <summary>
    /// Instancia da camera
    /// </summary>
    private Camera camera;

    public Mundo()
    {
      var polygon = new Polygon();
      polygon.points4D = new List<Ponto4D>() {
        new Ponto4D(300, 500),
        new Ponto4D(100, 300),
        new Ponto4D(200, 100),
        new Ponto4D(300, 300)
      };
      polygons.Add(polygon);
      polygonSelected = polygon;
      camera = new Camera(600, 600, this);
      camera.Run();
      camera.Run(1.0 / 60.0);
    }

    public void AddPolygon(Polygon polygon) {
      this.polygons.Add(polygon);
    }
    
    public void Draw()
    {
      foreach (var polygon in polygons)
      {
        polygon.Draw();
        if (polygon == polygonSelected)
        {
          if (polygon.points4D.Count <= 1) {
            polygonSelected = null;
          }
          if (polygon.Bbox == null)
          {
            polygon.Bbox = new Bbox(polygon.points4D);
          }
          polygon.Bbox.Draw();
          if (pointSelected != null)
          {
            polygon.SelectVertex(pointSelected);
          }
        }
        else
        {
          polygon.Bbox = null;
        }
      }
    }

  }

}