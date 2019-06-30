/*
  Autor: Dalton Solano dos Reis
*/
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;

namespace gcgcg
{
  /// <summary>
  /// Classe que define o mundo virtual
  /// Padrão Singleton
  /// </summary>
  /// 

  class Mundo
  {
    public static Mundo instance = null;
    private MusicExecution musicExecution;
    private GuitarTab guitarTab;
    public static Mundo getInstance()
    {
      if (instance == null)
        instance = new Mundo();
      return instance;
    }
    public void Desenha()
    {
      if (guitarTab != null)
      {
        guitarTab.Desenha();
      }
      SRU3D();
    }
    public void NewMusicExecution(MusicExecution musicExecution)
    {
      this.musicExecution = musicExecution;
      musicExecution.OnBeforeStart(() => {
        this.guitarTab = new GuitarTab();
      });
      musicExecution.OnNote(note => {
        this.guitarTab.NewTab(note);
      });
      musicExecution.onUpdateRendering(progress => {
        
      });
    }
    private void SRU3D()
    {
      GL.LineWidth(1);
      GL.Begin(PrimitiveType.Lines);
      GL.Color3(Color.Red);
      GL.Vertex3(0, 0, 0); GL.Vertex3(200, 0, 0);
      GL.Color3(Color.Green);
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, 200, 0);
      GL.Color3(Color.Blue);
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, 0, 200);
      GL.End();
    }
  }

}