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
    private NoteMatcherBar noteMatcherBar;
    private Background background = new Background();
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
        this.background.Desenha();
        this.guitarTab.Desenha();
        this.noteMatcherBar.Desenha();
      }
    }
    public void NewMusicExecution(MusicExecution musicExecution)
    {
      this.musicExecution = musicExecution;
      musicExecution.OnBeforeStart(() => {
        this.guitarTab = new GuitarTab();
        this.noteMatcherBar = new NoteMatcherBar();
      });
      musicExecution.OnNote(note => {
        this.guitarTab.NewTab(note);
      });
      var translationZ = (float) Tab.WIDTH / MusicTimer.GRAPHIC_PROGRESSION_RATE;
      musicExecution.onUpdateRendering(progress => {
        this.guitarTab.TranslateTabs(0, 0, translationZ);
      });
    }
    public void notePressed(byte note)
    {
      if (this.noteMatcherBar != null)
      {
        this.noteMatcherBar.setActive(note);
      }
    }
    public void noteReleased(byte note)
    {
      if (this.noteMatcherBar != null)
      {
        this.noteMatcherBar.setInactive(note);
      }
    }
  }

}