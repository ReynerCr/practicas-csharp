/*
     Nombres y apellidos: Reyner David Contreras Rojas.
     C.I: V.-26934400
     Materia: Programacion II
*/

using System;

namespace Practica_2
{
    class Program
    {
        private static ulong calcularFactorial(int n)
        {
            ulong factorial = 1;
            for (uint i = 1; i <= n; i++)
            {
                factorial *= i;
            }//for i

            return factorial;
        }//calcularFactorial
        
        private static void generarTriangulo(int nMAX)
        {
            nMAX--;
            int n = 0; //n: nivel actual
            for (int esp = nMAX; esp >= 0; esp--)
            {
                for (int k = 0; k < esp; k++)
                    Console.Write("  ");

                for (int c = 0; c <= n; c++)
                {
                    ulong var = (calcularFactorial(n)) / (calcularFactorial(c) * calcularFactorial(n - c));
                    Console.Write(var + "  ");
                }
                Console.WriteLine();
                n++;
            }//for esp
        }//void generarTriangulo(int nMAX)

        static void Main(string[] args)
        {
            Console.Write("Ingrese el numero de filas a generar: ");
            string texto = Console.ReadLine();
            int nMAX = 0;
            try
            {
                nMAX = Convert.ToInt32(texto);
                if (nMAX < 1)
                    throw new OverflowException();
            } catch(FormatException e)
            {
                Console.WriteLine("No es un numero, se saldra del programa.");
                nMAX = -1;
            } catch(OverflowException e)
            {
                Console.WriteLine("No es un numero valido, se saldra del programa.");
                nMAX = -1;
            }

            if (nMAX != -1)
            {
                generarTriangulo(nMAX);
            }
            Console.WriteLine("Presione cualquier tecla para salir.");
            Console.ReadKey();
        }//static void Main()
    }//class Program
}//namespace PRactica_2
