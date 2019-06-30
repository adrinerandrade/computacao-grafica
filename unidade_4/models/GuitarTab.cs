using System;

namespace gcgcg
{
  public class GuitarTab
  {
    public static readonly int TABS_SIZE = 14;
    private readonly float INITIAL_POSITION;
    private Tab[] tabs = new Tab[TABS_SIZE];
    private byte random = 0;
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
          tabs[i].translacaoXYZ(0, 0, Tab.WIDTH);
        }
      }
      random = random == 0 ? (byte) 1: (byte) 0; 
      tabs[0] = new Tab(0, 0, INITIAL_POSITION, notes, random);
    }
    public void TranslateTabs(float translateX, float translateY, float translateZ)
    {
      for (var i = 0; i < tabs.Length; i++)
      {
        if (tabs[i] != null)
        {
          tabs[i].translacaoXYZ(translateX, translateY, translateZ);
        }
      }
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
