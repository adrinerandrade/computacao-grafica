namespace gcgcg
{
    public class ChildState : IState
    {
        private Polygon parent;

        /// <summary>
        /// Contrturor salva quem é o pai de poligono
        /// </summary>
        /// <param name="mundo"></param>
        public ChildState(Mundo mundo)
        {
            this.parent = mundo.polygonSelected;
            mundo.polygonSelected = null;
        }

        /// <summary>
        /// Verifica se o comando é para criar um filho
        /// Adiciona ou removo filhos ao pai.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="mundo"></param>
        /// <returns></returns>
        public IState Perform(Command command, Mundo mundo)
        {
            if (command.Equals(Command.MOUSE_MOVE))
            {
                var hover = PolygonSelector.GetSelected(mundo.polygons, Mouse.X, Mouse.Y);
                if (hover != this.parent)
                {
                    mundo.polygonSelected = hover;
                }
            }
            else if (command.Equals(Command.CLICK))
            {
                var selected = PolygonSelector.GetSelected(mundo.polygons, Mouse.X, Mouse.Y);
                if (selected != null && selected != this.parent)
                {
                    mundo.RemovePolygon(selected);
                    this.parent.children.Add(selected);
                    mundo.polygonSelected = this.parent;
                    return new MainState();
                }
            }
            else if (command.Equals(Command.ESCAPE))
            {
                return new MainState();
            }
            return this;
        }

    }
}