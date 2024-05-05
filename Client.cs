using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>Статус клиента</summary>
enum StatusOfClient { waitClerk, waitOrder, takeOrder } // Ждёт клерка, Ждёт свой заказ, Забрал свой заказ
namespace lab3_mod
{
    class Client
    {
        StatusOfClient Status { get; set; } = 0;
    }
}
