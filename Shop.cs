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
        private int countOfClients = 0; //Н - накопитель заявок

        public Shop(int modelingTime = 100, int countClerk = 2, int step = 1)  // Количество клерков, Шаг моделирования, Время моделирование) 
        {
            clerks = new Clerk[countClerk]; //К1, K2, K3 - каналы обслуживания - клерки
            for (int j = 0; j < clerks.Length; j++) clerks[j] = new Clerk(); // создание клерков

            int timeArrivalNewClient = -1, timeArrivalLastClient = 0; //Время появления следующего клиента относительно прошлого, время появление прошлого клиента относительно нулевой точки времени
            int timeMakeOrder; //Время выполнения заказа


            //Моделируемые величины
            int[] clerksRestTime = new int[countClerk]; //Время отдыха клерка
            List<int> clientsTimeInStore = new List<int>(); //Время каждого клиента проведенного в магазине
            int numberOfWarehouseExits = 0; //Количество выходов в склад клерками
            int numberOfClients = 0;

            //Вспомогательные
            int numberOfLastClient = 0; //Номер последнего обслуженного клиента


            for (int t = 0; t < modelingTime; t += step)
            {
                //Генерация времени появления клиента
                if (timeArrivalNewClient == -1)
                {
                    timeArrivalNewClient = (int)Math.Round(Distribution.ExpGenerator(120)); //Генерация времени появления клиента

                    while (timeArrivalNewClient == 0) timeArrivalNewClient = (int)Math.Round(Distribution.ExpGenerator(120));
 
                }
                if(timeArrivalNewClient + timeArrivalLastClient == t) {
                    timeArrivalLastClient += timeArrivalNewClient;
                    timeArrivalNewClient = -1;
                    countOfClients++;
                    clientsTimeInStore.Add(0);

                }


                //Если клерки свободны и клиенты есть, то нагружаем клерков
                for (int j = 0; j < clerks.Length; j++)
                {
                    if (countOfClients != 0 && clerks[j].Status == StatusOfClerk.waitClients)
                    {

                        //Определяем кол-во клиентов которых обсуживает клерк
                        if (countOfClients >= 6)
                        {
                            clerks[j].CountOfClients = 6;
                            countOfClients -= 6; 
                        }
                        else
                        {
                            clerks[j].CountOfClients = countOfClients;
                            countOfClients = 0;   
                        }

                        Console.WriteLine(" {0}-ый клерк взял {1} заказов", j + 1, clerks[j].CountOfClients);
                        numberOfClients += clerks[j].CountOfClients;
                        numberOfWarehouseExits++;
                        Console.WriteLine(" {0}/{1} заказов", numberOfClients, numberOfWarehouseExits);


                        //Расчёт времени нужное для обсуживания
                        timeMakeOrder = (int)Math.Round(Distribution.UniformGenerator(30, 90)); //Время хождения на склад 1 +- 0,5 мин
                        timeMakeOrder += (int)Math.Round(Distribution.NormalGenerator(clerks[j].CountOfClients * 0.2, clerks[j].CountOfClients * 3)); //Время поисков товаров
                        timeMakeOrder += (int)Math.Round(Distribution.UniformGenerator(30, 90)); //Время возвращения со склада 1 +- 0,5 мин
                        for (int k = numberOfLastClient; k < numberOfLastClient + clerks[j].CountOfClients; k++)//Время рассчета кадого клиента 2 +- 1 мин
                        {  
                            timeMakeOrder += (int)Math.Round(Distribution.UniformGenerator(60, 180));
                            clientsTimeInStore[k] += timeMakeOrder;
                        }
                        clerks[j].WorkCompletionTime = timeMakeOrder;
                        //Console.WriteLine("время исполнения заказа {0}",timeMakeOrder);

                        numberOfLastClient += clerks[j].CountOfClients;
                        
                    }
                    else if (clerks[j].Status == StatusOfClerk.waitClients)
                    {
                        clerksRestTime[j]++;
                    }

                    clerks[j].Update();
                    //Console.WriteLine("клерк {0} - статус {1}, время выполнения заказа{2}, сколько уже выполняет {3}", j, (int)clerks[j].Status, clerks[j].WorkCompletionTime, clerks[j].PassedTime);
                }

                //Считаем время клиентов, стоящих в очереди
                for (int j = numberOfLastClient; j < clientsTimeInStore.Count; j++) clientsTimeInStore[j]++;
            }

            

            Console.WriteLine("1. Загрузка клерков:");
            for (int j = 0; j < clerks.Length; j++)
            {
                Console.WriteLine("  {0} - ый клерк работал {1}% от моделируемого времени ({2}/{3} сек)", j+1, (modelingTime - clerksRestTime[j]) * 100/ modelingTime, modelingTime - clerksRestTime[j], modelingTime);
            }
            
            Console.WriteLine("2. Среднее время прибывания клиента в магазине: {0}", clientsTimeInStore.Sum() / (double)clientsTimeInStore.Count);
                       
            Console.WriteLine("3. Среднее число заявок, которые удовлетворяются за один выход в склад: {0}", (double)numberOfClients / (double)numberOfWarehouseExits);


        }
    }
}
