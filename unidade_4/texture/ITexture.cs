using OpenTK.Graphics.OpenGL;

public abstract class ITexture {

    public int Texture { get; set; }
    public bool IsTop { get; set; } = false;

    public void CreateTexture1() {
        GL.TexCoord2(0.0f, 1.0f);
    }
    public void CreateTexture2() {
        GL.TexCoord2(1.0f, 1.0f);
    }
    public void CreateTexture3() {
        GL.TexCoord2(1.0f, 0.0f);
    }
    public void CreateTexture4() {
        GL.TexCoord2(0.0f, 0.0f);
    }
}
