using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSkeleton.Data.Entities
{
    public class SomeEntity // Класа описващ таблицата в базата,
                            // или по-точно какво трябва да притежава един обект (запис) от съответната 
                            // таблица в базата. Всяко пропърти отговаря за колона в самата таблица.
                            // В случая с изпита от миналия път класа щеше да се казва Student
    {

        // Пропъртитата отговарящи за колоните в таблицата. В случая с изпита от миналия път
        // имената им биха били ColumnOne => FacultyNumber, ColumnTwo => Major, ColumnThree => FirstName,
        // ColumnFour => LastName
        public int Id { get; set; }

        public string ColumnOne { get; set; }

        public string ColumnTwo { get; set; }

        public string ColumnThree { get; set; }

        public string ColumnFour { get; set; }


    }
}
