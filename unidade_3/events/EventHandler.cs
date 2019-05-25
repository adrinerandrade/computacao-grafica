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
    /// Classe responsável executar os comandos recebidos
    /// </summary>
    public class EventHandler
    {
        /// <summary>
        /// Pilha sincrona de comandos a serem executados
        /// </summary>
        public static Queue<List<Key>> keys { get; set; } = new Queue<List<Key>>();
        /// <summary>
        /// Empilha as referencias de mouse muve
        /// </summary>
        public static Queue<List<Key>> MouseMuve { get; set; } = new Queue<List<Key>>();
        private Mundo mundo;
        public IState state { get; set; } = new MainState();

        /// <summary>
        /// O Construtor inicia uma Thread que verifica o teclado
        /// </summary>
        public EventHandler(Mundo mundo)
        {
            this.mundo = mundo;
            Thread t = new Thread(EmitCapturedEvent);
            t.Start();
        }

        /// <summary>
        /// Metodo responsável por iniciar a logica do que será feito com o evento atual dos perifericos
        /// </summary>
        public void EmitCapturedEvent()
        {
            while (true) {
                if (EventHandler.keys.Count > 0) {
                    var command = Command.GetCommand(EventHandler.keys.Dequeue());
                    if (!command.Equals(Command.NONE)) {
                        this.state = state.Perform(command, this.mundo);
                    }
                }
                if (EventHandler.MouseMuve.Count > 0) {
                    var command = Command.GetCommand(EventHandler.MouseMuve.Dequeue());
                    if (!command.Equals(Command.NONE)) {
                        this.state = state.Perform(command, this.mundo);
                    }
                }
            }
        }
    }
}