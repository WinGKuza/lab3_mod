using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>Статус клерка</summary>
enum StatusOfClerk { waitClients, makeOrder } // Ждёт клиентов на кассе, Выполняет заказ
namespace lab3_mod
{
    class Clerk
    {
        private int workCompletionTime = 0;  
        private int passedTime = 0; 
        private int countOfclients = 0; 

        // <summary>Количество клиентов, которые он обслуживает в данный момент</summary>
        public int CountOfclients { 
            get{ 
                return countOfclients;
            } 
            set {
                countOfclients = value < 0 ? throw new Exception("Значение количества меньше нуля") : value;
            } } 

        public StatusOfClerk Status { get; set; } = 0;

        /// <summary>Время нужное для выполнения процесса в секундах</summary>
        public int WorkCompletionTime
        {
            get { return workCompletionTime; }
            set { 
                workCompletionTime = value < 0 ? throw new Exception("Значение времени меньше нуля") : value;
                Status = StatusOfClerk.makeOrder;
            }
        }

        /// <summary>Время, которое клерк выполняет этот заказ в секундах</summary>
        public int PassedTime { get { return passedTime; } }

        /// <summary>
        /// Обновляет время и статус клерка
        /// </summary>
        /// <returns>true - статус поменялся, false - статус не поменялся</returns>
        public bool Update()
        {
            passedTime += 1;
            if (Status != StatusOfClerk.waitClients && passedTime >= workCompletionTime && workCompletionTime != 0)
            {
                Status = StatusOfClerk.waitClients;
                passedTime = 0;
                countOfclients = 0;
                workCompletionTime = 0;
                return true; 
            }
            return false;
        }
    }
}
