namespace Ejercicio_II
{
    class Nave
    {
        public int VelocidadMaxima //por hora
        {
            get;
            private set;
        }
        public int Combustible //en kilos
        {
            get;
            private set;
        }
        public int ConsumoDeCombustible //en kilos por hora
        {
            get;
            private set;
        }

        public Nave(int Vi, int Fi, int Ci)
        {
            VelocidadMaxima = Vi;
            Combustible = Fi;
            ConsumoDeCombustible = Ci;
        }

        public override string ToString()
        {
            return VelocidadMaxima + " " + Combustible + " " + ConsumoDeCombustible;
        }
    }
}
