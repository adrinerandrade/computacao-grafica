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

        private bool isMouseDown = false;

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
            bool isOldClick = false;
            while (true)
            {
                if (isMouseDown)
                {
                    SetMouseDown(false);
                    keyboardState = OpenTK.Input.Keyboard.GetState();
                    OnKeyDown();
                    EmitCapturedEvent();
                    isOldClick = true;
                } else if (IsAnyKeyDown()) {
                    keyboardState = OpenTK.Input.Keyboard.GetState();
                    OnKeyDown();
                    if (isOldClick) {
                        isOldClick = false;
                        EventBlock();
                    }
                }
                else
                {
                    EmitCapturedEvent();
                }
            }
        }

        private void EventBlock() {
            while (true) {
                if (!IsAnyKeyDown()) {
                    keys.Clear();
                    break;
                } else if (isMouseDown) {
                    break;
                }
            }
        }

        /// <summary>
        /// Emita um evento quando não existem teclas pressionadas e a lista de keys não for vazia
        /// </summary>
        public void EmitCapturedEvent()
        {
            if (keys.Count > 0)
            {
                ShowEmitedCapturedEvent();
                keys.Clear();
            }
        }

        private void ShowEmitedCapturedEvent() {
            if (keys.Count > 0)
            {
                foreach (var item in keys)
                {
                    Console.WriteLine(item);
                }
            }
        }

        /// <summary>
        /// Verifica qual tecla está sendo segurada
        /// </summary>
        private void OnKeyDown()
        {
            foreach (var value in ExternalKey.acceptKey)
            {
                if (keyboardState.IsKeyDown(value))
                {
                    if (value.Equals(OpenTK.Input.Key.B))
                    {
                        AddKey(Key.B);
                    }
                    if (value.Equals(OpenTK.Input.Key.P))
                    {
                        AddKey(Key.P);
                    }
                    if (value.Equals(OpenTK.Input.Key.R))
                    {
                        AddKey(Key.R);
                    }
                    if (value.Equals(OpenTK.Input.Key.G))
                    {
                        AddKey(Key.G);
                    }
                    if (value.Equals(OpenTK.Input.Key.A))
                    {
                        AddKey(Key.A);
                    }
                    if (value.Equals(OpenTK.Input.Key.ControlLeft))
                    {
                        AddKey(Key.ControlLeft);
                    }
                    if (value.Equals(OpenTK.Input.Key.Space))
                    {
                        AddKey(Key.Space);
                    }
                }
            }
        }

        /// <summary>
        /// Adiciona uma nova tecla na lista keys.
        /// </summary>
        public void AddKey(Key key)
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
            return OpenTK.Input.Keyboard.GetState().IsAnyKeyDown;
        }

        public void SetMouseDown(bool isMouseDown) {
            this.isMouseDown = isMouseDown;
        }


    }
}