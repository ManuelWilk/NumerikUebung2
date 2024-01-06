using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumerikUebung2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Matrix A erstellen:
            Console.WriteLine("Anzahl Zeilen der 1ten Matrix engeben:");
            int rows1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Anzahl Spalten der 1ten Matrix engeben:");
            int cols1 = int.Parse(Console.ReadLine());
            double[,] matrixA = CreateMatrix(rows1, cols1);

            //Matrix B erstellen:
            Console.WriteLine("Anzahl Zeilen der 2ten Matrix engeben:");
            int rows2 = int.Parse(Console.ReadLine());
            Console.WriteLine("Anzahl Spalten der 2ten Matrix engeben:");
            int cols2 = int.Parse(Console.ReadLine());
            double[,] matrixB = CreateMatrix(rows2, cols2);

            //Matrizen zum Testen ohne Nutzereingabe
            //double[,] matrixA = { { 1, 2, 3 }, { 6, 5, 4 }, { 7, 8, 9 } };
            //double[,] matrixB = { { 0, 3, 4 }, { 1, 2, 3 }, { -2, 5, 6 } };

            //Ausgeben der Matrizen mit PrintMatrix
            Console.WriteLine("Matrix A:");
            PrintMatrix(matrixA);
            Console.WriteLine("Matrix B:");
            PrintMatrix(matrixB);

            //Multiplizieren der Matrizen und Ausgabe mit MultMatrix
            Console.WriteLine("Produktmatrix:");
            PrintMatrix(MultMatrix(matrixA, matrixB));

            //Programm beenden
            Console.WriteLine("Zum beenden beliebige Taste drücken...");
            Console.ReadLine();
        }

        //Matrizen Ausgeben
        static void PrintMatrix(double[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(" {0,3} ", matrix[i, j]);
                }
                Console.Write('\n');
            }
        }

        //Matrizen Prüfen und Multiplizieren
        static double[,] MultMatrix(double[,] m, double[,] n)
        {
            double[,] produktmatrix;
            if (m.GetLength(1) == n.GetLength(0))
            {
                produktmatrix = new double[m.GetLength(0), n.GetLength(1)];
                for (int i = 0; i < m.GetLength(0); i++)
                {
                    for (int j = 0; j < n.GetLength(1); j++)
                    {
                        for (int k = 0; k < n.GetLength(0); k++)
                        {
                            produktmatrix[i, j] += m[i, k] * n[k, j];
                        }
                    }
                }
                return produktmatrix;
            }
            else
            {
                produktmatrix = new double[0, 0];
                Console.WriteLine("Fehler : Multiplikation nicht möglich");
                return produktmatrix;
            }
        }

        static double[,] CreateMatrix(int rows, int cols)
        {
            double[,] matrix = new double[rows, cols];
            double userinput;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write("Zeile {0} Spalte {1}: ", i + 1, j + 1);
                    userinput = double.Parse(Console.ReadLine());
                    matrix[i, j] = userinput;
                }
            }
            return matrix;
        }
    }
}
