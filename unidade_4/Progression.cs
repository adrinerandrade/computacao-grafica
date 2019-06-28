namespace gcgcg
{
  public class Progression 
  {
    private float x;
    private float y;
    private float z;
    private float t;

    public Progression(float times) {
      this.t = 100 / times;
    }
    private double calculate(double A, double B, double t) {
      return A + (B - A) * t;
    }
  }
}