using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice_C_sharp
{
    class Exercice22
    {
        public static int[,] BuildingMatrix(int[] leftVector, int[] rightVector)
        {
            int n = leftVector.Length;
            int m = rightVector.Length;
            int[,] matrice = new int[n, m];

            for (int i=0;i<n;i++)
            {
                for (int j=0; j<m;j++)
                {
                    matrice[i,j] = leftVector[i] * rightVector[j];
                }
            }
            return matrice;
        }

        public static int[,] Addition(int[,] leftMatrix, int[,] rightMatrix)
        {
            int n = leftMatrix.GetLength(0);
            int m = rightMatrix.GetLength(1);
            int[,] matrice = new int[n, m];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    matrice[i,j] = leftMatrix[i,j] + rightMatrix[i,j];
                }
            }
            return matrice;
        }

        public static int[,] Substraction(int[,] leftMatrix, int[,] rightMatrix)
        {
            int n = leftMatrix.GetLength(0);
            int m = rightMatrix.GetLength(1);
            int[,] matrice = new int[n, m];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    matrice[i, j] = leftMatrix[i, j] - rightMatrix[i, j];
                }
            }
            return matrice;
        }

        public static int[,] Multiplication(int[,] leftMatrix, int[,] rightMatrix)
        {
            int n = leftMatrix.GetLength(0);
            int m = rightMatrix.GetLength(0);
            int l = rightMatrix.GetLength(1);
            int[,] matrice = new int[n, l];

            for (int i=0;i<n;i++)
            {
                for (int k=0;k<m;k++)
                {
                    for (int j=0;j<l;j++)
                    {
                        matrice[i, j] += leftMatrix[i, k] * rightMatrix[k, j];
                    }
                }    
            }
            return matrice;
        }

        public static void DisplayMatrix(int[,] matrix)
        {
            string s = string.Empty;
            int rows = matrix.GetLength(0); 
            int cols = matrix.GetLength(1); 

            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < cols; ++j)
                {
                    s += matrix[i, j].ToString().PadLeft(5) + " ";
                }
                s += Environment.NewLine;
            }

            Console.WriteLine(s);
        }
    }
}
