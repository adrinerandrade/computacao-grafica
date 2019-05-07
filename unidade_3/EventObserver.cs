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
    /// Classe responsável por controlar os eventos externos
    /// </summary>
    public class EventObserver
    {
        /// <summary>
        /// Lista de teclas clicadas no momento
        /// </summary>
        private List<Key> keys = new List<Key>();

        /// <summary>
        /// Estado do teclado no momento do loop
        /// </summary>
        private OpenTK.Input.KeyboardState keyboardState;

        /// <summary>
        /// O Construtor inicia uma Thread que verifica o teclado
        /// </summary>
        public EventObserver()
        {
            Thread t = new Thread(ObserverEvents);
            t.Start();
        }

        /// <summary>
        /// Verifica se existem novas teclas sendo pressionadas.
        /// Quando não existem mais teclas pressionadas, emita um evento.
        /// </summary>
        private void ObserverEvents()
        {
            while (true)
            {
                if (IsAnyKeyDown())
                {
                    keyboardState = OpenTK.Input.Keyboard.GetState();
                    onKeyDown();
                }
                else
                {
                    emitCapturedEvent();
                }
            }
        }

        /// <summary>
        /// Emita um evento quando não existem teclas pressionadas e a lista de keys não for vazia
        /// </summary>
        public void emitCapturedEvent()
        {
            if (keys.Count > 0)
            {
                foreach (var item in keys)
                {
                    Console.WriteLine(item);
                }
                keys.Clear();
            }
        }

        /// <summary>
        /// Verifica qual tecla está sendo segurada
        /// </summary>
        private void onKeyDown()
        {
            foreach (var value in ExternalKey.acceptKey)
            {
                if (keyboardState.IsKeyDown(value))
                {
                    if (value.Equals(OpenTK.Input.Key.B))
                    {
                        addKey(Key.B);
                    }
                    if (value.Equals(OpenTK.Input.Key.P))
                    {
                        addKey(Key.P);
                    }
                    if (value.Equals(OpenTK.Input.Key.R))
                    {
                        addKey(Key.R);
                    }
                    if (value.Equals(OpenTK.Input.Key.G))
                    {
                        addKey(Key.G);
                    }
                    if (value.Equals(OpenTK.Input.Key.A))
                    {
                        addKey(Key.A);
                    }
                    if (value.Equals(OpenTK.Input.Key.ControlLeft))
                    {
                        addKey(Key.ControlLeft);
                    }
                    if (value.Equals(OpenTK.Input.Key.Space))
                    {
                        addKey(Key.Space);
                    }
                }
            }
        }

        /// <summary>
        /// Adiciona uma nova tecla na lista keys.
        /// </summary>
        /// <param name="key"></param>
        public void addKey(Key key)
        {
            if (!keys.Contains(key))
            {
                keys.Add(key);
            }
        }

        /// <summary>
        /// Verifica se existe alguma tecla pressionada
        /// </summary>
        /// <returns>bool</returns>
        private bool IsAnyKeyDown()
        {
            return
                OpenTK.Input.Keyboard.GetState().IsAnyKeyDown ||
                OpenTK.Input.Mouse.GetState().IsAnyButtonDown;
        }
    }
}