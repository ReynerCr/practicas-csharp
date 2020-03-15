using System.Collections.Generic;

namespace Ejercicio_II
{
    class Hangar
    {
        public int NumNaves
        {
            get;
            private set;
        }

        public int DistanciaExpedicion
        {
            get;
            private set;
        }

        public List<Nave> Naves
        {
            get;
            private set;
        }

        public Hangar(int N, int D, List<Nave>naves)
        {
            NumNaves = N;
            DistanciaExpedicion = D;
            Naves = naves;
        }
    }
}
