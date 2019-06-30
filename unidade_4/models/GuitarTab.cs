using System;

namespace gcgcg
{
  public class GuitarTab
  {
    private static readonly int TABS_SIZE = 14;
    private static readonly float AVOID_CONFLICT_FACTOR = 0.01f;
    private readonly float INITIAL_POSITION;
    private Tab[] tabs = new Tab[TABS_SIZE];
    public GuitarTab()
    {
      INITIAL_POSITION = -((TABS_SIZE / 2) * Tab.WIDTH);
      for (var i = 0; i < tabs.Length; i++)
      {
        this.NewTab(new byte[5]);
      }
    }
    public void NewTab(byte[] notes)
    {
      for (var i = TABS_SIZE - 2; i >= 0; i--)
      {
        if (tabs[i] != null)
        {
          tabs[i + 1] = tabs[i];
          tabs[i].translacaoXYZ(0, 0, Tab.WIDTH + AVOID_CONFLICT_FACTOR);
        }
      }
      tabs[0] = new Tab(0, 0, INITIAL_POSITION, notes);
    }
    public void Desenha()
    {
      for (var i = 0; i < tabs.Length; i++)
      {
        tabs[i].Desenha();
      }
    }
  }
}
