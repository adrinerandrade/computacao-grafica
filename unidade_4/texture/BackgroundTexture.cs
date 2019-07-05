public class BackgroundTexture: ITexture {

    public BackgroundTexture(int texture) {
        this.Texture = texture;
        this.IsTop = true;
        this.IsBotton = true;
        this.IsLeft = true;
        this.IsRight = true;
        this.IsBackground = true;
    }

}