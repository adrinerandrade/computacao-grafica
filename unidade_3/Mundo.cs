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
    private List<Polygon> poligonos = new List<Polygon>();

    /// <summary>
    /// Poligono atualmente selecionado
    /// </summary>
    private Polygon poligonoSelected;

    /// <summary>
    /// Informaçào se o poligono é filho de outro poligono
    /// </summary>
    private bool isChildren;

    /// <summary>
    /// Instancia da camera
    /// </summary>
    private Camera camera = new Camera(400, 400);

    public Mundo() {
      camera.Run();
      camera.Run(1.0/60.0);
      poligonos.Add(new Polygon());
    }

    public void AddPolygon(Polygon polygon) {
      this.poligonos.Add(polygon);
    }
    
  }

}