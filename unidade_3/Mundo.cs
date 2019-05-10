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
        new Ponto4D(100, 100),
        new Ponto4D(-100, 100),
        new Ponto4D(-100, 100),
        new Ponto4D(0, -100),
        new Ponto4D(0, -100),
        new Ponto4D(100, 100)
      };
      polygons.Add(polygon);
      var polygon2 = new Polygon();
      polygon2.points4D = new List<Ponto4D>() {
        new Ponto4D(-100, -100),
        new Ponto4D(-300, -100),
        new Ponto4D(-300, -100),
        new Ponto4D(-200, -300),
        new Ponto4D(-200, -300),
        new Ponto4D(-100, -100)
      };
      polygons.Add(polygon2);
      polygonSelected = polygon2;
      camera = new Camera(400, 400, this);
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
          if (polygon.Bbox == null) {
            polygon.Bbox = new Bbox(polygon.points4D);
          }
          polygon.Bbox.Draw();
        } else {
          polygon.Bbox = null;
        }
      }
    }

  }

}