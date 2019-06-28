namespace gcgcg
{
  public class GuitarTab
  {
    private Tab tab = new Tab(0, 0, 0);
    public void Desenha()
    {
      tab.Desenha();
    }
    public void test(){
      tab.translacaoXYZ(10, 10, 10);
    }
  }
}
