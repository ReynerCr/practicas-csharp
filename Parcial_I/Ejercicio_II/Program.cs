/*
     Nombres y apellidos: Reyner David Contreras Rojas.
     C.I: V.-26934400
     Materia: Programacion II
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ejercicio_II
{
    class Program
    {
        //para leer el archivo y guardar los datos
        private static bool LeerArchivo(string nombre, ref Queue<Hangar> CasosPrueba)
        {
            string linea;
            bool correcto = false;
            try
            {
                using (StreamReader sr = new StreamReader(nombre, Encoding.GetEncoding("iso-8859-1")))
                {
                    CasosPrueba = new Queue<Hangar>();
                    int casos = 0;
                    if ((linea = sr.ReadLine()) != null)
                        casos = Int32.Parse(linea);
                    if (casos < 1 || casos > 50)
                        throw new Exception("Numero de casos de prueba no validos.");

                    int cont = 0;
                    int N, D;
                    string[] datos;
                    while (cont < casos && (linea = sr.ReadLine()) != null)
                    {
                        datos = linea.Split();
                        N = Int32.Parse(datos[0]);
                        D = Int32.Parse(datos[1]);
                        if ((N < 1 || N > 100) && (D < 1 || D > 1000000))
                            throw new Exception("N y/o D no son validos.");

                        int contN = 0;
                        int Vi, Fi, Ci;
                        List<Nave> naves = new List<Nave>();
                        while (contN < N && (linea = sr.ReadLine()) != null)
                        {
                            datos = linea.Split();
                            Vi = Int32.Parse(datos[0]);
                            Fi = Int32.Parse(datos[1]);
                            Ci = Int32.Parse(datos[2]);
                            if (Vi < 1 || Vi > 1000 ||
                                Fi < 1 || Fi > 1000 ||
                                Ci < 1 || Ci > 1000)
                                throw new Exception("Vi, Fi, o Ci de caso " + cont + " no son validos.");

                            Nave nave = new Nave(Vi, Fi, Ci);
                            naves.Add(nave);
                            contN++;
                        }//while para leer los grupos de naves

                        if (contN < N)
                        {
                            Console.WriteLine("Numero de naves en caso " + casos + " es menor al indicado.");
                            N = naves.Count;
                        }

                        Hangar h = new Hangar(N, D, naves);
                        CasosPrueba.Enqueue(h);
                        cont++;
                    }//while para N y D

                    if (cont < casos)
                    {
                        throw new Exception("Numero de casos de prueba mayor al contenido real.");
                    }
                }//using StreamReader
                correcto = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("\n\n\n");
            return correcto;
        }//private static bool LeerArchivo()

        /*hace una regla de 3 simple para determinar cual es la distancia maxima a la que 
        puede llegar una nave, y si esa distancia es mayor o igual a la de la espedición (D)*/
        private static bool Llegara(Nave nave, int D)
        {
            int recorridoPosible = (nave.Combustible * nave.VelocidadMaxima) / nave.ConsumoDeCombustible;
            if (recorridoPosible >= D)
                return true;
            else
                return false;
        }//private static bool Llegara(Nave nave, int D)

        //calcular si la nave llega a la expedicion y luego suma al contador para imprimir
        private static void ProcesarEImprimir(ref Queue<Hangar> CasosPrueba)
        {
            int D;
            int llegan;
            foreach(Hangar h in CasosPrueba)
            {
                llegan = 0;
                D = h.DistanciaExpedicion;
                List<Nave> naves = h.Naves;
                foreach (Nave n in naves)
                {
                    if (Llegara(n, D))
                        ++llegan;
                }//foreach
                Console.WriteLine(llegan);
            }//foreach
        }//private static void ProcesarEImprimir(ref Queue<Hangar> CasosPrueba)

        static void Main(string[] args)
        {
            Queue<Hangar> CasosPrueba = null;
            bool correcto = LeerArchivo("misiones.in", ref CasosPrueba);
            if (!correcto || CasosPrueba.Count == 0)
                Console.WriteLine("El archivo no se encontro o el formato de los datos esta mal.");
            else
                ProcesarEImprimir(ref CasosPrueba);

            //salida de programa y pausa para que se pueda leer
            Console.Write("\nPresione una tecla para salir.");
            Console.ReadKey();
            return;
        }//static void Main(string[] args)
    }//class Program
}
