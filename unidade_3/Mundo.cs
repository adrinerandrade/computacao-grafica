using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace gcgcg
{
  public class Mundo
  {
    private List<Poligono> poligonos = new List<Poligono>();
    private Poligono poligonoSelected;
    private bool isChildren;
    private Camera camera = new Camera(400, 400);

    public Mundo() {
      camera.Run();
      camera.Run(1.0/60.0);
    }
    
  }

}