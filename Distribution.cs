using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace lab3_mod
{
    internal class Distribution
    {
        static Random r = new Random();

        /// <summary>
        /// Генерация чисел с нормальным законом распределения и сортировка
        /// </summary>
        /// <param name="N">Количество чисел</param>
        /// <param name="Sigma">Среднеквадратичное отклонение</param>
        /// <param name="M">Математическое ожидание</param>
        /// <returns></returns>
        static public double[] NormalGenerator(int N, double Sigma, double M)
        {
            double[] A = new double[N];
            double V = 0, z, n = 20;
            for (int i = 0; i < N; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    V += r.NextDouble();
                }
                z = (V - n / 2) / Math.Sqrt(n / 12);
                A[i] = z * Sigma + M;
                V = 0;
            }
            Array.Sort(A);
            return A;
        }
        /// <summary>
        /// Генерация числа с нормальным законом распределения
        /// </summary>
        /// <param name="Sigma">Среднеквадратичное отклонение</param>
        /// <param name="M">Математическое ожидание</param>
        /// <returns></returns>
        static public double NormalGenerator(double Sigma, double M)
        {
            double A;
            double V = 0, z, n = 20;
            for (int j = 1; j <= n; j++) V += r.NextDouble();
            
            z = (V - n / 2) / Math.Sqrt(n / 12);
            A = z * Sigma + M;
            return A;
        }

        /// <summary>
        /// Генерация числа с экспоненциальным законом распределения
        /// </summary>
        /// <param name="M">Математическое ожидание</param>
        /// <returns></returns>
        static public double ExpGenerator(double M)
        {
            double A, Lambda = 1 / M, x = r.NextDouble();
            A = 1 - Math.Pow(Math.E, -Lambda * x);
            return A;
        }
        /// <summary>
        /// Генерация числа с равномерным законом распределения
        /// </summary>
        /// <param name="delta">Смещение</param>
        /// <returns>Случайное число в пределах [a + dev; b + dev]</returns>
        static public double UniformGenerator(double delta = 0) => r.NextDouble() + delta;
    }
}
