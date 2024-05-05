using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3_mod
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int countClerk = 2, step = 1, modelingTime = 100; // Количество клерков, Шаг моделирования, Время моделирование
            Clerk[] clerks = new Clerk[countClerk];
            for (int i = 0; i < modelingTime; i+=step) { 
                for (int j = 0; j < countClerk; j++) clerks[j].Update();
            }
        }
    }
}
