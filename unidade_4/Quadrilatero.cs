/*
  Autor: Dalton Solano dos Reis
 */
using OpenTK.Graphics.OpenGL;

namespace gcgcg
{
  internal class Quadrilatero : Objeto
  {
    private float x;
    private float y;
    private float z;
    private float comprimento;
    private float largura;
    private float altura;
    private bool exibeVetorNormal = false;
    public Quadrilatero(): this(0, 0, 0, 2, 2, 2)
    {
    }

    public Quadrilatero(float comprimento, float largura, float altura): this(0, 0, 0, comprimento, largura, altura)
    {
    }

    public Quadrilatero(float x, float y, float z, float comprimento, float largura, float altura)
    {
      this.x = x;
      this.y = y;
      this.z = z;
      this.comprimento = comprimento;
      this.largura = largura;
      this.altura = altura;
    }

    //TODO: entender o uso da keyword new ... e replicar para os outros projetos
    new public void Desenha()
    {
      var leftX = this.x + this.comprimento / 2;
      var rightX = this.x - this.comprimento / 2;
      var topY = this.y + this.altura / 2;
      var bottomY = this.y - this.altura / 2;
      var frontZ = this.z + this.largura / 2;
      var backZ = this.z - this.largura / 2;

      var color = rgbToGlColor(92, 51, 23);
      GL.Begin(PrimitiveType.Quads);
      // Face da frente
      GL.Color3(color[0], color[1], color[2]);
      GL.Normal3(0, 0, 1);
      GL.Vertex3(leftX, bottomY, frontZ);
      GL.Vertex3(rightX, bottomY, frontZ);
      GL.Vertex3(rightX, topY, frontZ);
      GL.Vertex3(leftX, topY, frontZ);
      // Face do fundo
      GL.Color3(color[0], color[1], color[2]);
      GL.Normal3(0, 0, -1);
      GL.Vertex3(leftX, bottomY, backZ);
      GL.Vertex3(leftX, topY, backZ);
      GL.Vertex3(rightX, topY, backZ);
      GL.Vertex3(rightX, bottomY, backZ);
      // Face de cima
      var color2 = rgbToGlColor(143, 77, 31);
      GL.Color3(color2[0], color2[1], color2[2]);
      GL.Normal3(0, 1, 0);
      GL.Vertex3(leftX, topY, backZ);
      GL.Vertex3(leftX, topY, frontZ);
      GL.Vertex3(rightX, topY, frontZ);
      GL.Vertex3(rightX, topY, backZ);
      // Face de baixo
      GL.Color3(1, 1, 1);
      GL.Color3(color[0], color[1], color[2]);
      GL.Vertex3(leftX, bottomY, backZ);
      GL.Vertex3(rightX, bottomY, backZ);
      GL.Vertex3(rightX, bottomY, frontZ);
      GL.Vertex3(leftX, bottomY, frontZ);
      // Face da direita
      GL.Color3(color[0], color[1], color[2]);
      GL.Normal3(1, 0, 0);
      GL.Vertex3(rightX, bottomY, backZ);
      GL.Vertex3(rightX, topY, backZ);
      GL.Vertex3(rightX, topY, frontZ);
      GL.Vertex3(rightX, bottomY, frontZ);
      // Face da esquerda
      GL.Color3(color[0], color[1], color[2]);
      GL.Normal3(-1, 0, 0);
      GL.Vertex3(leftX, bottomY, backZ);
      GL.Vertex3(leftX, bottomY, frontZ);
      GL.Vertex3(leftX, topY, frontZ);
      GL.Vertex3(leftX, topY, backZ);
      GL.End();

      if (exibeVetorNormal)
        ajudaExibirVetorNormal();
    }
    public void ajudaExibirVetorNormal()
    {
      GL.LineWidth(3);
      GL.Color3(1.0, 1.0, 1.0);
      GL.Begin(PrimitiveType.Lines);
      // Face da frente
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, 0, 5);
      // Face do fundo
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, 0, -5);
      // Face de cima
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, 5, 0);
      // Face de baixo
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, -5, 0);
      // Face da direita
      GL.Vertex3(0, 0, 0); GL.Vertex3(5, 0, 0);
      // Face da esquerda
      GL.Vertex3(0, 0, 0); GL.Vertex3(-5, 0, 0);
      GL.End();
    }

    private float[] rgbToGlColor(int red, int blue, int green)
    {
      return new float[] { red / 255f, blue / 255f, green / 255f };
    }

    public void trocaExibeVetorNormal() => exibeVetorNormal = !exibeVetorNormal;

  }
}