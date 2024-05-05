using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3_mod
{
    internal class Shop
    {
        Clerk[] clerks;
        private int countOfCliets = 0; //Н - накопитель заявок

        public Shop(int modelingTime = 100, int countClerk = 2, int step = 1)  // Количество клерков, Шаг моделирования, Время моделирование) 
        {
            clerks = new Clerk[countClerk]; //К1, K2, K3 - каналы обслуживания - клерки
            for (int j = 0; j < clerks.Length; j++) clerks[j] = new Clerk(); // создание клерков

            int timeArrivalNewCliet = 0, timeArrivalLastCliet = 0; //Время появления следующего клиента относительно прошлого, время появление прошлого клиента относительно нулевой точки времени
            int timeMakeOrder;
            for (int t = 0; t < modelingTime; t += step)
            {
                //Генерация времени появления клиента
                if (timeArrivalNewCliet == 0)
                {
                    timeArrivalNewCliet = (int)Math.Round(Distribution.ExpGenerator(120)); //Генерация времени появления клиента
                }
                else if(timeArrivalNewCliet + timeArrivalLastCliet == t) {
                    timeArrivalLastCliet += timeArrivalNewCliet;
                    timeArrivalNewCliet = 0;
                    countOfCliets++;
                }


                //Если клерки свободны и клиенты есть, то нагружаем клерков
                for (int j = 0; j < clerks.Length; j++)
                {
                    if (countOfCliets != 0 && clerks[j].Status == StatusOfClerk.waitClients)
                    {
                        //Определяем кол-во клиентов которых обсуживает клерк
                        if (countOfCliets >= 6)
                        {
                            clerks[j].CountOfclients = 6;
                            countOfCliets -= 6; 
                        }
                        else
                        {
                            clerks[j].CountOfclients = countOfCliets;
                            countOfCliets = 0;   
                        }

                        //Расчёт времени нужное для обсуживания
                        timeMakeOrder = (int)Math.Round(Distribution.UniformGenerator(30, 90)); //Время хождения на склад 1 +- 0,5 мин
                        timeMakeOrder += (int)Math.Round(Distribution.NormalGenerator(clerks[j].CountOfclients * 0.2, clerks[j].CountOfclients * 3)); //Время поисков товаров
                        timeMakeOrder += (int)Math.Round(Distribution.UniformGenerator(30, 90)); //Время возвращения со склада 1 +- 0,5 мин
                        for (int k = 0; k < clerks[j].CountOfclients; k++) timeMakeOrder += (int)Math.Round(Distribution.UniformGenerator(60, 180)); //Время рассчета кадого клиента 2 +- 1 мин
                        clerks[j].WorkCompletionTime = timeMakeOrder;
                    }

                    clerks[j].Update();
                }
            }
        }
    }
}
