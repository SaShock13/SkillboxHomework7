using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace SkllboxHomework7
{
    struct Worker

    {
        #region Поля
        public uint id;
        public DateTime dateOfReg;
        public string fullName;
        public uint age;
        public uint height;
        public DateTime birthday;
        public string hometown;
        #endregion

        #region Конструктор
        public Worker(uint id, DateTime dateOfReg, string fullName, uint age, uint height, DateTime birthday, string hometown)
        {
            this.id = id;
            this.dateOfReg = dateOfReg;
            this.fullName = fullName;
            this.age = age;
            this.height = height;
            this.birthday = birthday;
            this.hometown = hometown;
        }
        #endregion

        #region Методы
        public override string ToString()
        {
            return $"{id,3} {dateOfReg.ToShortDateString(),16} {fullName,35} {age,8} {height,4} {birthday.ToShortDateString(),13} {hometown,20}";
        }
       
        public string StringToSave()
        {
            return $"{id}#{dateOfReg.ToShortDateString()}#{fullName}#{age}#{height}#{birthday.ToShortDateString()}#{hometown}";
        }
        #endregion
    }
}
