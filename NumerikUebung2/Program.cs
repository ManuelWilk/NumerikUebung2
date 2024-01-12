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
            //Auswahl zwischen Multiplikation, LR-Zerlegung, Inverse
            Console.WriteLine("Was möchten Sie tun?"+ '\n'+ "1.Multiplikation" + '\n' + "2.LR-Zerlegung" + '\n' + "3.Inverse");
            int choice = int.Parse(Console.ReadLine());

            //Multiplikation
            if (choice == 1)
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

            //LR-Zerlegung
            if (choice == 2)
            {
                Console.WriteLine("Dimension der Matrix eingeben:");
                int dim = int.Parse(Console.ReadLine());
                double[,] matrix = CreateMatrix(dim,dim);
                double[,] Lm = new double[dim, dim];
                double[,] Rm = new double[dim, dim];

                for (int i = 0; i < dim; i++)
                {
                    //R-Matrix
                    for (int j = i; j < dim; j++)
                    {
                        double sum = 0;
                        for (int k = 0; k < i; k++)
                        {
                            sum += Lm[i, k] * Rm[k, j];
                        }
                        Rm[i, j] = matrix[i, j] - sum;
                    }

                    //L-Matrix
                    for (int k = i; k < dim; k++)
                    {
                        if (i == k)
                        {
                            Lm[i, i] = 1;
                        }
                        else
                        {
                            double sum = 0;
                            for (int j = 0; j < i; j++)
                            {
                                sum += Lm[k, j] * Rm[j, i];
                            }
                            Lm[k, i] = (matrix[k, i] - sum) / Rm[i, i];
                        }
                    }
                }
                Console.WriteLine("Ihre Matrix:");
                PrintMatrix(matrix);
                Console.WriteLine("L-Matrix:");
                PrintMatrix(Lm);
                Console.WriteLine("R-Matrix");
                PrintMatrix(Rm);
                Console.WriteLine("Zum beenden beliebige Taste drücken...");
                Console.ReadLine();
            }

            //Inverse
            if (choice==3)
            {
                Console.WriteLine("Dimension der Matrix eingeben:");
                int dim = int.Parse(Console.ReadLine());
                double[,] matrix = CreateMatrix(dim, dim);
                Console.WriteLine("Ihre Matrix:");
                PrintMatrix(matrix);
                double[,] matrixInverse = InvertMatrix(matrix, dim);
                bool invPoss = CheckInverse(matrixInverse, dim);

                if (invPoss == true)
                {
                    Console.WriteLine("Inverse:");
                    PrintMatrix(matrixInverse);
                }
                else
                {
                    Console.WriteLine("Diese Matrix besitzt keine Inverse");
                }
                Console.WriteLine("Zum beenden beliebige Taste drücken...");
                Console.ReadLine();
            }

            else
            {
                Console.WriteLine("Ungültige Eingabe");
                Console.ReadLine();
            }
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

        //Matrizen erstellen
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

        //Inverse berechnen
        static double[,] InvertMatrix(double[,] matrix, int dim)
        {
            double[,] temp;
            for (int i = 0; i < dim; i++)
            {
                temp = new double[dim, dim];
                double y_temp = 1;
                double x_temp;
                x_temp = -matrix[i, i];
                matrix[i, i] = -y_temp;

                for (int j = 0; j < dim; j++)
                {
                    matrix[i, j] /= x_temp;
                    temp[i, j] = matrix[i, j];
                }

                for (int z = 0; z < dim; z++)
                {
                    if (z == i)
                    {
                        continue;
                    }
                    else
                    {
                        for (int s = 0; s < dim; s++)
                        {
                            if (s == i)
                            {
                                temp[z, s] = matrix[z, s] * temp[i, s];
                            }
                            else
                            {
                                temp[z, s] = matrix[z, i] * temp[i, s] + matrix[z, s];
                            }
                        }
                    }
                }
                matrix = temp;
            }
            return matrix;
        }

        //Prüfen ob Inverse existiert
        static bool CheckInverse(double[,] matrix, int dim)
        {
            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    if (double.IsNaN(matrix[i, j]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }
}
