/*
     Nombres y apellidos: Reyner David Contreras Rojas.
     C.I: V.-26934400
     Materia: Programacion II
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ejercicio_I
{
    class Program
    {
        private static int CompararPesoPerdido(Persona x, Persona y)
        {
            int final1, final2;
            final1 = x.PesoFinal();
            final2 = y.PesoFinal();

            if (final1 < final2)
                return 1;
            else if (final1 > final2)
                return -1;

            return 0;
        }//private static int CompararPesoPerdido(Persona x, Persona y)

        private static bool LeerArchivo(ref Queue<List<Persona>> grupos, string nombre)
        {
            string Nombre;
            int DiasDeDieta;
            int PesoAlInicio;
            bool correcto = true;
            try
            {
                using (StreamReader sr = new StreamReader(nombre, Encoding.GetEncoding("iso-8859-1")))
                {
                    grupos = new Queue<List<Persona>>();
                    string linea;
                    string[] datos;
                    while ((linea = sr.ReadLine()) != null) //no sea null
                    {
                        //verifico si la entrada inicia con "START"
                        if (!linea.Equals("START", StringComparison.Ordinal))
                            continue;

                        List<Persona> gp = new List<Persona>();
                        while ((linea = sr.ReadLine()) != null) //no sea null
                        {
                            if (linea.Equals("END"))
                                break;

                            datos = linea.Trim().Split();
                            try //verificar que los datos sean correctos
                            {
                                Nombre = datos[0];
                                DiasDeDieta = Int32.Parse(datos[1]);
                                PesoAlInicio = Int32.Parse(datos[2]);
                                correcto = ValidarDatos(Nombre, DiasDeDieta, PesoAlInicio);
                                Persona p = new Persona(Nombre, DiasDeDieta, PesoAlInicio);
                                gp.Add(p);
                            }
                            catch (Exception) //no importa el error, 
                            {
                                correcto = false;
                            }
                        }//while para leer el grupo, sale con break si linea es "END"

                        if (gp.Count > 0) //ordenar para la salida final
                        {
                            gp.Sort(CompararPesoPerdido);
                            grupos.Enqueue(gp);
                        }
                    }//while para leer la primera linea de cada grupo
                }//using StreamReader
            }
            catch(Exception e)
            {
                correcto = false;
                Console.WriteLine(e.Message);
            }

            return correcto;
        }//LeerArchivo(ref Queue<GrupoPersonas> grupos)

        private static bool ValidarDatos(string Nombre, int DiasDeDieta, int PesoAlInicio)
        {
            if (Nombre.Length <= 20
                && DiasDeDieta >= 0
                && DiasDeDieta <= 999
                && PesoAlInicio >= 0
                && PesoAlInicio <= 10000)
                return true;
            return false;
        }//public static bool ValidarDatos(string Nombre, int DiasDeDieta, int PesoAlInicio)

        private static void ImprimirResultados(ref Queue<List<Persona>>grupos)
        {
            foreach (List<Persona>lp in grupos)
            {
                foreach(Persona p in lp)
                {
                    Console.WriteLine(p.Nombre);
                }
                Console.WriteLine();
            }
        }//public static void ImprimirResultados(ref Queue<List<Persona>>grupos)

        static void Main(string[] args)
        {
            Queue<List<Persona>> grupos = null;
            bool correcto = LeerArchivo(ref grupos, "dieta.in");

            if (!correcto || grupos.Count == 0)
                Console.WriteLine("El archivo no se encontro o el formato de los datos esta mal.");
            else
            {
                ImprimirResultados(ref grupos);
            }

            //salida de programa y pausa para que se pueda leer
            Console.Write("\nPresione una tecla para salir.");
            Console.ReadKey();
            return;
        }//Main()
    }//class Program
}
