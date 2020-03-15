namespace Ejercicio_I
{
    class Persona
    {
        public string Nombre
        {
            get;
            private set;
        }

        public int DiasDeDieta
        {
            get;
            private set;
        }

        public int PesoAlInicio
        {
            get;
            private set;
        }

        public Persona(string nombre, int diasdieta, int pesoinicio)
        {
            Nombre = nombre;
            DiasDeDieta = diasdieta;
            PesoAlInicio = pesoinicio;
        }

        public int PesoFinal()
        {
            return PesoAlInicio - DiasDeDieta;
        }

        public override string ToString()
        {
            return Nombre + " " + DiasDeDieta + " " + PesoAlInicio;
        }
    }//class Persona
}
