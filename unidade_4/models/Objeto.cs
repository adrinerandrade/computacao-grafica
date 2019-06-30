/*
  Autor: Dalton Solano dos Reis
*/

using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System;
using System.Collections.Generic;

namespace gcgcg
{
  internal abstract class Objeto
  {
    protected List<Ponto4D> listaPto = new List<Ponto4D>();
    private BBox bBox = new BBox();
    private Transformacao4D matriz = new Transformacao4D();
    private List<Objeto> children = new List<Objeto>();

    /// Matrizes temporarias que sempre sao inicializadas com matriz Identidade entao podem ser "static".
    private static Transformacao4D matrizTmpTranslacao = new Transformacao4D();
    private static Transformacao4D matrizTmpTranslacaoInversa = new Transformacao4D();
    private static Transformacao4D matrizTmpEscala = new Transformacao4D();
    private static Transformacao4D matrizTmpRotacao = new Transformacao4D();
    private static Transformacao4D matrizGlobal = new Transformacao4D();

    public Objeto()
    {
    }

    public void AdicionaPto(Ponto4D pto) {
      listaPto.Add(pto);
    }
    public void Desenha()
    {
      GL.PushMatrix();
      GL.MultMatrix(matriz.GetDate());
      draw();
      for (var i = 0; i < children.Count; i++)
      {
        children[i].Desenha();
      }

      GL.PopMatrix();
    }
    protected abstract void draw();
    public void atualizarBBox()
    {
      if (listaPto.Count > 0) {
        bBox.atribuirBBox(listaPto[0]);
        for (int i = 1; i < listaPto.Count; i++)
        {
          bBox.atualizarBBox(listaPto[i]);
        }
        bBox.processarCentroBBox();
      }
    }
    public void exibeMatriz()
    {
      matriz.exibeMatriz();
    }
    public void exibePontos()
    {
      Console.WriteLine("P0[" + listaPto[0].X + "," + listaPto[0].Y + "," + listaPto[0].Z + "," + listaPto[0].W + "]");
      Console.WriteLine("P1[" + listaPto[1].X + "," + listaPto[1].Y + "," + listaPto[1].Z + "," + listaPto[1].W + "]");
    }
    public void atribuirIdentidade()
    {
      matriz.atribuirIdentidade();
    }
    public void addChild(Objeto child)
    {
      this.children.Add(child);
    }
    public void translacaoXYZ(double tx, double ty, double tz)
    {
      Transformacao4D matrizTranslate = new Transformacao4D();
      matrizTranslate.atribuirTranslacao(tx, ty, tz);
      matriz = matrizTranslate.transformMatrix(matriz);
    }
    public void escalaXYZ(double Sx, double Sy)
    {
      Transformacao4D matrizScale = new Transformacao4D();
      matrizScale.atribuirEscala(Sx, Sy, 1.0);
      matriz = matrizScale.transformMatrix(matriz);
    }

    public void escalaXYZPtoFixo(double escala, Ponto4D ptoFixo)
    {
      matrizGlobal.atribuirIdentidade();

      matrizTmpTranslacao.atribuirTranslacao(ptoFixo.X, ptoFixo.Y, ptoFixo.Z);
      matrizGlobal = matrizTmpTranslacao.transformMatrix(matrizGlobal);

      matrizTmpEscala.atribuirEscala(escala, escala, 1.0);
      matrizGlobal = matrizTmpEscala.transformMatrix(matrizGlobal);

      ptoFixo.inverterSinal();
      matrizTmpTranslacaoInversa.atribuirTranslacao(ptoFixo.X, ptoFixo.Y, ptoFixo.Z);
      matrizGlobal = matrizTmpTranslacaoInversa.transformMatrix(matrizGlobal);

      matriz = matriz.transformMatrix(matrizGlobal);
    }
    public void rotacaoZ(double angulo)
    {
      matrizTmpRotacao.atribuirRotacaoZ(Transformacao4D.DEG_TO_RAD * angulo);
      matriz = matrizTmpRotacao.transformMatrix(matriz);
    }
    public void rotacaoZPtoFixo(double angulo, Ponto4D ptoFixo)
    {
      matrizGlobal.atribuirIdentidade();

      matrizTmpTranslacao.atribuirTranslacao(ptoFixo.X, ptoFixo.Y, ptoFixo.Z);
      matrizGlobal = matrizTmpTranslacao.transformMatrix(matrizGlobal);

      matrizTmpRotacao.atribuirRotacaoZ(Transformacao4D.DEG_TO_RAD * angulo);
      matrizGlobal = matrizTmpRotacao.transformMatrix(matrizGlobal);

      ptoFixo.inverterSinal();
      matrizTmpTranslacaoInversa.atribuirTranslacao(ptoFixo.X, ptoFixo.Y, ptoFixo.Z);
      matrizGlobal = matrizTmpTranslacaoInversa.transformMatrix(matrizGlobal);

      matriz = matriz.transformMatrix(matrizGlobal);
    }
  }
}