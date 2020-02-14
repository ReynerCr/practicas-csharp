/*
     Nombres y apellidos: Reyner David Contreras Rojas.
     C.I: V.-26934400
     Materia: Programacion II
 */

using System;
using System.IO;
using System.Collections.Generic;

namespace Practica_1
{
    class Program
    {
        private static void ordenarD(string l, ref List<string> diccio)
        {
            string[] palabras = l.Split(' ');
            foreach (string palabra in palabras)
            {
                diccio.Add(palabra);
            }
            diccio.Sort();
        }//void ordenarD(string l, ref List<string>diccio)

        private static string ordenarP(string l, ref List<string> diccio)
        {
            string[] palabras = l.Split(' ');
            l = "";
            foreach (string palabra in palabras)
            {
                if (palabra.Length <= 3) //no tiene sentido ordenar si ya esta ordenado
                {
                    l += palabra + " ";
                    continue;
                }

                string copia = palabra;
                for (int i = 0; i < diccio.Count; i++)
                {
                    string aux = diccio[i];
                    if (aux.Length == palabra.Length)
                    {
                        if ((aux[0] == palabra[0]) && (aux[aux.Length - 1] == palabra[aux.Length - 1]))
                        {
                            int coincidencias = 2;
                            aux = aux.Substring(1, aux.Length - 2);
                            char[] auxC = aux.ToCharArray();

                            for (int j = 1; j < palabra.Length - 2; j++)
                            {
                                int encontrado = Array.IndexOf(auxC, palabra[j]);
                                if (encontrado != -1 && encontrado < auxC.Length)
                                {
                                    coincidencias++;
                                    auxC[encontrado] = ' ';
                                }//if encontrado
                                else
                                {
                                    coincidencias = 0;
                                    break;
                                }//no encontrado y por tanto no es la misma palabra pero desordenada
                            }//for j
                            if (coincidencias != 0)
                            {
                                copia = diccio[i];
                                break;
                            }//if coincidencias != 0 es la misma palabra
                        }//if primera y ultima letras iguales
                    }//if igual tamanyo
                }//for i
                l += copia + " ";
            }//foreach palabra
            return l;
        }//string ordenarP(string l, ref List<string>diccio)

        private static int procesar()
        {
            if (!File.Exists("reordenando.in")) //verifica si existe el archivo
                return 1;

            int cp = 0;
            using (StreamReader sr = File.OpenText("reordenando.in"))
            {
                string l = sr.ReadLine();
                bool esNumero = int.TryParse(l, out cp);
                if (!esNumero)
                    return 2;

                for (int c = 0; c < cp; c++)
                {
                    l = sr.ReadLine(); //l contiene la lista del diccionario
                    l = l.Trim();
                    List<string> diccionario = new List<string>();
                    ordenarD(l, ref diccionario); //ordena las palabras que estan en el diccionario

                    l = sr.ReadLine(); //ahora l contiene la lista de palabras para ordenar
                    l = l.Trim();
                    l = ordenarP(l, ref diccionario); //ordena las palabras de la lista de desordenadas
                    System.Console.WriteLine(l);
                }//for c
            }//using StreamReader

            return 0;
        }//int procesar()

        static void Main(string[] args)
        {
            int estado; //casos de prueba
            estado = procesar();
            System.Console.WriteLine();
            switch (estado)
            {
                case 0:
                    System.Console.Write("Programa finalizado correctamente, ");
                    break;
                case 1:
                    System.Console.Write("No se encontro el archivo reordenando.in, ");
                    break;
                case 2:
                    System.Console.Write("La primera linea no es un numero entero, ");
                    break;
            }//switch()
            System.Console.WriteLine("presione cualquier tecla para salir.");
            System.Console.ReadKey();
        }//static void Main()
    }//class Program
}//namespace Practica_1
