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

    public Mundo() {
      var polygon = new Polygon();
      polygon.points4D = new List<Ponto4D>() {
        new Ponto4D() { X = 200, Y = 200, Z = 0 },
        new Ponto4D() { X = -200, Y = -200, Z = 0 }
      };
      polygons.Add(polygon);
      camera = new Camera(400, 400, this);
      camera.Run();
      camera.Run(1.0/60.0);
    }

    public void Draw() {
      Console.WriteLine(polygons.Count);
      foreach (var polygon in polygons)
      {
          polygon.Draw();
      }
    }
    
  }

}