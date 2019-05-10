using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using OpenTK.Input;
using System.Collections.Generic;
using System.Threading;

namespace gcgcg
{
    /// <summary>
    /// Referências de teclas OpenTk
    /// </summary>
    public class ExternalKey
    {

        /// <summary>
        /// Telas aceitas, que serão responsáveis por eventos do OpenTK
        /// </summary>
        public static readonly List<OpenTK.Input.Key> acceptKey = new List<OpenTK.Input.Key>() {
            OpenTK.Input.Key.B,
            OpenTK.Input.Key.P,
            OpenTK.Input.Key.R,
            OpenTK.Input.Key.G,
            OpenTK.Input.Key.A,
            OpenTK.Input.Key.BackSpace,
            OpenTK.Input.Key.Delete,
            OpenTK.Input.Key.ControlLeft,
            OpenTK.Input.Key.Space
        };

    }
}