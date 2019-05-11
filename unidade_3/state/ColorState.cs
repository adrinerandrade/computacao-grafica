using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;

namespace gcgcg
{
  public class ColorState : IState
  {
    public IState Perform(Command command, Mundo mundo)
    {
      if (command.Equals(Command.CHANGE_COLOR_RED)) {
        mundo.polygonSelected.color = Color.Red;
      } else if (command.Equals(Command.CHANGE_BLUE)) {
        mundo.polygonSelected.color = Color.Blue;
      } else if (command.Equals(Command.CHANGE_GREEN)) {
        mundo.polygonSelected.color = Color.Green;
      }
      return new MainState();
    }
  }
}