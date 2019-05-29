using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using OpenTK.Input;
using System.Collections.Generic;

namespace gcgcg
{
  public class Polygon
  {
    /// <summary>
    /// Lista de pontos 4d
    /// </summary>
    private List<Ponto4D> points4D;

    /// <summary>
    /// Informa se existe algum vertice selecionado
    /// </summary>
    private int selectedPoint = -1;

    /// <summary>
    /// instancia da bbox
    /// </summary>
    private Bbox Bbox;

    /// <summary>
    /// Primitiva utilizada pelo poligono
    /// </summary>
    /// <value></value>
    public PrimitiveType primitive { get; set; } = PrimitiveType.LineStrip;

    /// <summary>
    /// Lista de filhos do poligono
    /// </summary>
    /// <typeparam name="Polygon"></typeparam>
    /// <returns></returns>
    public List<Polygon> children { get; set; } = new List<Polygon>();

    /// <summary>
    /// Cor do poligono
    /// </summary>
    /// <value></value>
    public Color color { get; set; } = Color.Blue;

    /// <summary>
    /// Instancia da matris de transformação
    /// </summary>
    /// <returns></returns>
    private Transformacao4D transformacao = new Transformacao4D();

    /// <summary>
    /// Contrutor adiciona os pontos4d
    /// atribui a matriz identidade a matriz de transformacao
    /// cria a bbox com base nos pontos 4d
    /// </summary>
    /// <param name="points4D"></param>    
    public Polygon(List<Ponto4D> points4D)
    {
      this.points4D = points4D;
      this.transformacao.atribuirIdentidade();
      this.Bbox = new Bbox(points4D);
    }

    /// <summary>
    /// Adiciona um novo vertice ao poligo
    /// e atualiza a bbox
    /// </summary>
    /// <param name="point"></param>
    public void AddVertex(Ponto4D point)
    {
      this.points4D.Add(point);
      this.UpdateBBox();
    }

    /// <summary>
    /// gera um deslocamento no vertice do poligono
    /// atualiza a bbox
    /// </summary>
    /// <param name="index"></param>
    /// <param name="X"></param>
    /// <param name="Y"></param>
    public void UpdateVertexLocation(int index, double X, double Y)
    {
      var point = this.points4D[index];
      var result = this.GetDesloc(point, new Ponto4D() { X = X, Y = Y });
      point.X = result.X;
      point.Y = result.Y;
      this.UpdateBBox();
    }

    /// <summary>
    /// Retorna o deslocamento ++
    /// </summary>
    /// <param name="sourcePoint"></param>
    /// <param name="targetPoint"></param>
    /// <returns></returns>
    private Ponto4D GetDesloc(Ponto4D sourcePoint, Ponto4D targetPoint) {
      var transformedPoint = this.transformacao.transformPoint(sourcePoint);
      var result = new Ponto4D();
      result.X = sourcePoint.X + targetPoint.X - transformedPoint.X;
      result.Y = sourcePoint.Y + targetPoint.Y - transformedPoint.Y;
      return result;
    }

    /// <summary>
    /// efetua a translação do poligono e atualiza a bbox
    /// </summary>
    /// <param name="translX"></param>
    /// <param name="translY"></param>
    public void Translation(double translX, double translY)
    {
      var transl = new Transformacao4D();
      transl.atribuirTranslacao(translX, translY, 0);

      this.transformacao = transl.transformMatrix(this.transformacao);
      this.UpdateBBox();
    }

    /// <summary>
    /// Altera a escala do poligono ++
    /// </summary>
    /// <param name="scale"></param>
    public void Scale(double scale)
    {
      var translX = this.Bbox.centerX;
      var translY = this.Bbox.centerY;
      
      var originTrans = new Transformacao4D();
      originTrans.atribuirTranslacao(translX, translY, 0);

      var scaleTrans = new Transformacao4D();
      scaleTrans.atribuirEscala(scale, scale, 1);

      var initialPositionTrans = new Transformacao4D();
      initialPositionTrans.atribuirTranslacao(-translX, -translY, 0);

      var result = originTrans.transformMatrix(scaleTrans);
      result = result.transformMatrix(initialPositionTrans);

      this.transformacao = result.transformMatrix(this.transformacao);
      this.UpdateBBox();
    }

    /// <summary>
    /// Rotaciona o poligono ++
    /// </summary>
    /// <param name="degreeFactor"></param>
    public void Rotate(double degreeFactor)
    {
      var translX = this.Bbox.centerX;
      var translY = this.Bbox.centerY;

      var originTrans = new Transformacao4D();
      originTrans.atribuirTranslacao(translX, translY, 0);

      var rotationTrans = new Transformacao4D();
      rotationTrans.atribuirRotacaoZ(Transformacao4D.DEG_TO_RAD * degreeFactor);

      var initialPositionTrans = new Transformacao4D();
      initialPositionTrans.atribuirTranslacao(-translX, -translY, 0);

      var result = originTrans.transformMatrix(rotationTrans);
      result = result.transformMatrix(initialPositionTrans);

      this.transformacao = result.transformMatrix(this.transformacao);
      this.UpdateBBox();
    }

    /// <summary>
    /// Remove um vertice do poligono e recalcula da bbox
    /// </summary>
    /// <param name="index"></param>
    public void RemoveVertex(int index)
    {
      points4D.Remove(this.points4D[index]);
      if (this.selectedPoint == index)
      {
        this.DeselectVertex();
      }
      this.UpdateBBox();
    }

    /// <summary>
    /// Retorna a qtd de vertice
    /// </summary>
    /// <returns></returns>
    public int VertexCount() {
      return this.points4D.Count;
    }

    /// <summary>
    /// Calcula a distancia de Manhattan ao clicar na tela para saber o poligono mais proximo
    /// </summary>
    /// <param name="X"></param>
    /// <param name="Y"></param>
    public void SelectNearestVertex(double X, double Y)
    {
      this.selectedPoint = DistanceManhattan(new Ponto4D() { X = X, Y = Y});
    }
    public void DeselectVertex()
    {
      this.selectedPoint = -1;
    }
    public int GetSelectedVertex()
    {
      return selectedPoint;
    }

    /// <summary>
    /// Clona a bbox
    /// </summary>
    /// <returns></returns>
    public Bbox GetBBox()
    {
      return this.Bbox.Clone();
    }
    public List<Ponto4D> GetTransformedPoints()
    {
      return this.points4D.ConvertAll(point => this.transformacao.transformPoint(point));
    }

    /// <summary>
    /// Desenha o poligono e seus filhos e vertices selecionados
    /// </summary>
    public void Draw()
    {
      GL.PushMatrix();
      GL.MultMatrix(transformacao.GetDate());

      GL.Color3(color);
      GL.Begin(primitive);
      foreach (var point in points4D)
      {
        GL.Vertex3(point.X, point.Y, point.Z);
      }
      GL.End();
      DrawChildrens();
      if (this.selectedPoint > -1) {
        DrawSelectedVertex(this.points4D[this.selectedPoint]);
      }

      GL.PopMatrix();
    }

    /// <summary>
    /// Desenha dos filhos
    /// </summary>
    private void DrawChildrens()
    {
      if (children != null)
      {
        foreach (var poligono in children)
        {
          poligono.Draw();
        }
      }
    }

    /// <summary>
    /// Desenha o vertice selecionado
    /// </summary>
    /// <param name="point"></param>
    private void DrawSelectedVertex(Ponto4D point)
    {
      GL.Color3(Color.Red);
      GL.LineWidth(1);
      GL.PointSize(2);
      GL.Begin(PrimitiveType.Points);
      double x, y, z;
      double i;
      for (i = 0; i <= 72; i++)
      {
        x = point.X + (3 * Math.Cos(i));
        y = point.Y + (3 * Math.Sin(i));
        z = 0;
        GL.Vertex3(x, y, z);
      }
      GL.End();
    }

    /// <summary>
    ///  Desenha a bbox
    /// </summary>
    public void DrawBBox()
    {
      this.Bbox.Draw();
    }

    /// <summary>
    /// Calcula da distancia Manhattan, ou seja uma distacia em linha reta do vertice mais proximo
    /// </summary>
    /// <param name="point4D"></param>
    /// <returns></returns>
    private int DistanceManhattan(Ponto4D point4D)
    {
      int selectedPoint = -1;
      double minValue = Double.MaxValue;
      var points = this.GetTransformedPoints();
      for (var i = 0; i < points.Count; i++)
      {
        var point = points[i];
        double distanceX = point.X - point4D.X;
        double distanceY = point.Y - point4D.Y;
        double distanceManhattan = Math.Abs(distanceX) + Math.Abs(distanceY);
        if (minValue > distanceManhattan)
        {
          minValue = distanceManhattan;
          selectedPoint = i;
        }
      }
      return selectedPoint;
    }

    /// <summary>
    /// Atualiza a bbox
    /// </summary>
    private void UpdateBBox()
    {
      this.Bbox.BBoxDimensions(this.GetTransformedPoints());
    }
  }
}