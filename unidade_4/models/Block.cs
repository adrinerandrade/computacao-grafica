/*
  Autor: Dalton Solano dos Reis
 */
using System;
using OpenTK.Graphics.OpenGL;

namespace gcgcg
{
  internal class Block : Objeto
  {
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
    private float length;
    private float width;
    private float height;
    private float[] color;
    private bool exibeVetorNormal = false;
    private dynamic texture;
    public Block(): this(0, 0, 0, new int[] { 255, 255, 255 }, 2, 2, 2, null)
    {
    }

    public Block(float length, float width, float height): this(0, 0, 0, new int[] { 255, 255, 255 }, length, width, height, null)
    {
    }

    public Block(float x, float y, float z, int[] color, float length, float width, float height, dynamic texture)
    {
      this.x = x;
      this.y = y;
      this.z = z;
      this.color = rgbToGlColor(color[0], color[1], color[2]);
      this.length = length;
      this.width = width;
      this.height = height;
      this.texture = texture;
    }
    protected override void draw()
    {
      var leftX = this.x + this.length / 2;
      var rightX = this.x - this.length / 2;
      var topY = this.y + this.height / 2;
      var bottomY = this.y - this.height / 2;
      var frontZ = this.z + this.width / 2;
      var backZ = this.z - this.width / 2;
      if (this.texture != null) {
        GL.Enable(EnableCap.Texture2D);
        GL.BindTexture(TextureTarget.Texture2D, this.texture.Texture);
      }
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
      GL.Color3(color[0], color[1], color[2]);
      GL.Normal3(0, 1, 0);
      bool isTop = false;
      if (this.texture != null) {
        Type[] types = this.texture.GetType();
      }
      if (this.texture != null && this.texture.GetType() == typeof(ITextureTop)) {
        this.texture.CreateTexture1();
      }
      GL.Vertex3(leftX, topY, backZ);
      if (this.texture != null && this.texture.GetType() == typeof(ITextureTop)) {
        this.texture.CreateTexture2();
      }
      GL.Vertex3(leftX, topY, frontZ);
      if (this.texture != null && this.texture.GetType() == typeof(ITextureTop)) {
        this.texture.CreateTexture3();
      }
      GL.Vertex3(rightX, topY, frontZ);
      if (this.texture != null && this.texture.GetType() == typeof(ITextureTop)) {
        this.texture.CreateTexture4();
      }
      GL.Vertex3(rightX, topY, backZ);
      // Face de baixo
      GL.Color3(color[0], color[1], color[2]);
      GL.Normal3(0, -1, 0);
      GL.Vertex3(leftX, bottomY, backZ);
      GL.Vertex3(leftX, bottomY, frontZ);
      GL.Vertex3(rightX, bottomY, frontZ);
      GL.Vertex3(rightX, bottomY, backZ);
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
      GL.Vertex3(leftX, topY, backZ);
      GL.Vertex3(leftX, topY, frontZ);
      GL.Vertex3(leftX, bottomY, frontZ);
      GL.End();

      if (this.texture != null) {
        GL.Disable(EnableCap.Texture2D);
      }

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