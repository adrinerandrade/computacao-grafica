namespace gcgcg
{
  public class Intersection
  {
    /// <summary>
    /// Calcula a interceção utilizando a formula (b + (a - b) * t)
    /// </summary>
    /// <returns></returns>
    public static (double, double) ResolveIntersection((Ponto4D, Ponto4D) scanLine, (Ponto4D, Ponto4D) polygonStraight)
    {
      var Yi = scanLine.Item1.Y;
      var Y1 = polygonStraight.Item1.Y;
      var Y2 = polygonStraight.Item2.Y;
      var t = (Yi - Y1)/(Y2 - Y1);
      var X1 = polygonStraight.Item1.X;
      var X2 = polygonStraight.Item2.X;
      return (t, X1 + (X2 - X1) * t);
    }
  }
}