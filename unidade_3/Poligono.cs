using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using OpenTK.Input;
using System.Collections.Generic;

namespace gcgcg
{
  public class Poligono
  {
      private List<Ponto4D> points4D = new List<Ponto4D>();
      private PrimitiveType primitive;
      private List<Poligono> poligonos = new List<Poligono>();
      private string color;
  }
}