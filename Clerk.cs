using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>Статус клерка</summary>
enum StatusOfClerk { waitClients, onTheWayToWarehouse, inTheWarehouse, onTheWayToCounter, acceptPayments } // Ждёт клиентов на кассе, Идёт на склад, На складе, Идёт обратно на кассу, Принимает оплату
namespace lab3_mod
{
    class Clerk
    {
        private int workCompletionTime;  
        private int passedTime;
        public StatusOfClerk Status { get; set; } = 0;

        /// <summary>Время нужное для выполнения процесса в секундах</summary>
        public int WorkCompletionTime
        {
            get { return workCompletionTime; }
            set { workCompletionTime = value < 0 ? throw new Exception("Значение времени меньше нуля") : value; }
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
            if (Status != StatusOfClerk.waitClients && passedTime >= workCompletionTime)
            {
                if (Status != StatusOfClerk.acceptPayments) Status += 1;
                else Status = 0;
                passedTime = 0;
                return true; 
            }
            return false;
        }
    }
}
