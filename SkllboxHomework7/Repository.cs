using System;
using static System.Console;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkllboxHomework7
{
    internal class Repository
    {
        #region Поля
        public Worker[] workersList;
        private string path = @"workersList.txt";
        public int count;
        int numOfLast;
        public string headers;
        //bool isCounted = false;
        #endregion

        #region Конструктор
        public Repository()
        {
           count = 0;
           workersList = new Worker[2];
            headers = $"{"ID",3} {"Дата регистрации ",16} {"Ф.И.О.",35} {"Возраст",8} " +
                 $"{"Рост",4} {"Дата рождения",13} {"Место рождения",20}"; // Заголовки для списка;
        }
        #endregion

        #region Методы

        /// <summary>
        /// Выводит в консоль список всех рабочих
        /// </summary>
        public void ShowAll()
        {
            Clear();
            if (File.Exists(path))
            {
                WriteLine(headers);

                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine(this.workersList[i]);
                }
                WriteLine("Общее количество работников : " + count);

            }
            else
            {
                WriteLine("База пустая !");
                ReadKey();
            }
        }
        /// <summary>
        /// Увеличават массив Рабочих на 10 единиц
        /// </summary>
        public void SizeIncrease()
        {
            Array.Resize(ref this.workersList, this.workersList.Length + 10);
        }
        /// <summary>
        /// Считывает базу из файла
        /// </summary>
        public void ReadFromFile()
        {
            if (File.Exists(path))
            {
                    using (StreamReader stream = new StreamReader(this.path))
                    {
                        while (!stream.EndOfStream)
                        {
                            string[] words = stream.ReadLine().Split('#');

                            AddWorker(new Worker(uint.Parse(words[0]), DateTime.Parse(words[1]), words[2], uint.Parse(words[3]), uint.Parse(words[4]), DateTime.Parse(words[5]), words[6]));

                        }
                    }
            }
        }
        /// <summary>
        /// Добавляет нового рабочего в базу
        /// </summary>
        /// <param name="worker"></param>
        public void AddWorker(Worker worker)
        {
            if (count >= this.workersList.Length)
            {
                SizeIncrease();
            }
            
            this.workersList[this.count] = worker;
            this.count++;
        }
        /// <summary>
        /// Считывает от пользователя данные нового рабочего 
        /// </summary>
        public void AddNewWorker()
        {
            Write("Введите данные нового сотрудника :\nФ.И.О.:");  // Ввод данных работника
            string фио = ReadLine();

            Write("\nВозраст : ");
            uint возраст = uint.Parse(ReadLine());

            Write("\nРост : ");
            uint рост = uint.Parse(ReadLine());

            Write("\nДата рождения : ");
            DateTime датаРождения = DateTime.Parse(ReadLine());

            Write("\nМесто рождения : ");
            string местоРождения = ReadLine();

            DateTime regDate = new DateTime();
            regDate = DateTime.Now;

            for (int i = 0; i < count; i++)
            {

                if (this.workersList[i].id > numOfLast)
                {
                    this.numOfLast = (int)workersList[i].id;
                }
            }
            this.AddWorker(new Worker((uint)this.numOfLast + 1, regDate, фио, возраст, рост, датаРождения, местоРождения));
            
            WriteLine("Добавить нового");
        }
        /// <summary>
        /// Удаляет данные о рабочем по идентификатору
        /// </summary>
        /// <param name="num"></param>
        public void DeleteWorker(int num)
        {
            Worker[] tempWorkers = new Worker[this.workersList.Length - 1];
            string deletingName = "";
            for (int i = 0, j = 0; j < count; i++, j++)
            {
                if (this.workersList[j].id == num)
                {
                    deletingName = this.workersList[j].fullName;
                    j++;
                }
                tempWorkers[i] = this.workersList[j];

            }
            for (int i = 0; i < count; i++)
            {
                this.workersList[i] = tempWorkers[i];
            }
            this.count--;
            WriteLine($"Готово! Рабочий {deletingName} больше не существует!");
            WriteLine("УдалитьРабочего");
        }
        /// <summary>
        /// Показывает список рабочих, зарегистрированных в определённом интервале времени
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public void ShowInterval(DateTime from, DateTime to)
        {
            Worker[] interval = new Worker[count];


            int num = 0;

            for (int j = 0; j < count; j++)
            {
                if (this.workersList[j].dateOfReg < from || this.workersList[j].dateOfReg > to) continue;
                else
                {
                    interval[num] = this.workersList[j];
                    num++;

                }
            }
            Array.Resize(ref interval, num);
            Clear();
            WriteLine($"Рабочие, зарегистрированые в период с {from} по {to} : ");
            WriteLine(headers);
            foreach (var item in interval)
            {
                Console.WriteLine(item);
            }
        }
        /// <summary>
        /// Записывает базу в файл
        /// </summary>
        public void SaveToFile()
        {
            using (StreamWriter stream = new StreamWriter(this.path))
            {
                for (int i = 0; i < this.count; i++)
                {
                    stream.WriteLine(this.workersList[i].StringToSave());
                }
            }
        }

        /// <summary>
        /// Сортирует список по определенному критерию
        /// </summary>
        public void OrderListBy()
        {
            bool flag = true;
            var sortedWorkers = workersList.OrderBy(Worker => Worker.dateOfReg);
            while (flag != false)

            {
                WriteLine("Для сортировки по дате регистрации нажмите 1 ,по ФИО нажмите 2 , по возрасту - 3 , по росту - 4, по дате рождения - 5, по месту рождения 6. Введите 0 , чтобы выйти ");

                string mode = ReadLine();
                int intMode;
                WriteLine();
                if (mode == "") intMode = 0;
                else intMode = int.Parse(mode);

                switch (intMode)
                {
                    case 1:
                        sortedWorkers = workersList.OrderBy(Worker => Worker.dateOfReg);
                        break;
                    case 2:
                        sortedWorkers = workersList.OrderBy(Worker => Worker.fullName);
                        break;
                    case 3:
                        sortedWorkers =workersList.OrderBy(Worker => Worker.age);
                        break;
                    case 4:
                        sortedWorkers = workersList.OrderBy(Worker => Worker.height);
                        break;
                    case 5:
                        sortedWorkers = workersList.OrderBy(Worker => Worker.birthday);
                        break;
                    case 6:
                        sortedWorkers = workersList.OrderBy(Worker => Worker.hometown);
                        break;
                    case 0:
                        flag = false;
                        break;
                    default:
                        WriteLine("Введите  цифру из списка !");
                        break;
                }
                if (flag ==false)
                {
                    break;
                }
                Clear();
                WriteLine(headers);

                foreach (var item in sortedWorkers)
                {
                    string word = item.ToString();
                    string[] words = word.Split(' ');
                    if (words[2] != "0")
                    {
                        WriteLine(word);
                    }

                }
            }

           


        }

        #endregion

    }
}
