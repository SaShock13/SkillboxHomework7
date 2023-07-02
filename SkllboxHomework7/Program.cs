using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkllboxHomework7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Repository rep = new Repository();
            string choise = "1";
            int menuChoise = 1;
            rep.ReadFromFile();
            while (menuChoise != 0)

            {

                WriteLine(" Enter для выхода \n 1 , чтобы посмотреть список работников \n 2, чтобы удалить работника по идентификатору \n 3, чтобы добавить работника \n 4, чтобы посмотреть работников, зарегистрированых в определенный срок \n 5 чтобы отсортировать список по критерию  :");
                choise = ReadLine();
                WriteLine();
                if (choise == "") choise = "0";
                menuChoise = int.Parse(choise);

                switch (menuChoise)
                {
                    case 1:

                        rep.ShowAll();
                        break;

                    case 2:
                        WriteLine("Введите ID работника, которого хотите удалить : ");
                        rep.DeleteWorker(int.Parse(ReadLine()));

                        break;

                    case 3:

                        rep.AddNewWorker();
                        rep.ShowAll();
                        break;

                    case 4: // Получение от пользователя начальной и конечной даты

                        DateTime from = new DateTime();
                        DateTime to = new DateTime();
                        WriteLine("Введите дату начала интервала, День Месяц Год через пробел : ");
                        string dateFrom = ReadLine();
                        if (dateFrom != "") from = DateTime.Parse(dateFrom);
                        else from = DateTime.Parse("01, 01, 0001");

                        WriteLine("Введите дату окончания интервала, День Месяц Год через пробел : ");
                        string dateTo = ReadLine();
                        if (dateTo != "") to = DateTime.Parse(dateTo);
                        else to = DateTime.Now;
                        rep.ShowInterval(from, to);
                        break;

                    case 5:

                        rep.OrderListBy();
                        break;
                    case 0:
                        break;

                    default:
                        Clear();
                        WriteLine("Нажмите цифру из предложенного списка !!!");
                        break;
                }
            }

            while (true)
            {
                Write("Хотите сохранить файл (д/н) : ");  // запрос на сохранение базы в файл
                string saveOrNot = ReadLine().ToLower();
                if (saveOrNot == "д")
                {
                    rep.SaveToFile();
                    Write("Файл успешно сохранён!!!");
                    break;
                }
                else if (saveOrNot == "н")
                {
                    Write("Файл НЕ был сохранён !");
                    break;
                }

            }
           
        }
    }
}
