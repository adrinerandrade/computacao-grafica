using System;
namespace gcgcg
{
  internal class Ponto4D
    {
        private double x;
        private double y;
        private double z;
        private double w;

        private double raio;
        private double angulo;

        private Ponto4D center;


        public Ponto4D(double x = 0.0, double y = 0.0, double z = 0.0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = 1;
        }

        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }
        public double Z { get => z; set => z = value; }

        public static Ponto4D InstanceFrom(double angulo, double raio, Ponto4D center){
            Ponto4D ponto = InstanceFrom(angulo, raio);
            ponto.center = center;
            return ponto;
        }

        public static Ponto4D InstanceFrom(double angulo, double raio)
        {
            Ponto4D ponto = new Ponto4D(raio, raio, raio);
            ponto.raio = raio;
            ponto.UpdateAngulo(angulo);

            return ponto;
        }

        public void UpdateAngulo(double angulo)
        {
            this.angulo = angulo;

            double x = 0d, y = 0d;

            if(this.center != null){
                x = center.X;
                y = center.Y;
            }

            this.X = x + (this.raio * Math.Cos(Math.PI * this.angulo / 180.0));
            this.Y = y + (this.raio * Math.Sin(Math.PI * this.angulo / 180.0));
            this.Z = 0;
        }

        public void UpdateRaio(double raio)
        {
            this.raio = raio;
            this.UpdateAngulo(this.angulo);
        }

        public override string ToString()
        {
            return string.Format("[Ponto4D => x: {0}, y:{1}, z:{2}, angulo:{3}, raio:{4}]", this.X, this.Y, this.Z, this.angulo, this.raio);
        }

    }
}