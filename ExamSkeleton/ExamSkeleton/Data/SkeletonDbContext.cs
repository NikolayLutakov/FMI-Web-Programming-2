using ExamSkeleton.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSkeleton.Data
{
    public class SkeletonDbContext : DbContext // Клас през който се осъществяба връзката с базата. В случая на
                                               // изпита от миналия път щеше да се казва StudentsDbContext
    {
        public SkeletonDbContext() // Дефолтен конструктор на класа за връзка с базата
        {

        }

        public DbSet<SomeEntity> SomeEntities { get; set; } // Колекция която държи обектите (записите
                                                            // от таблицата в базата), както се казва,
                                                            // тази колекция такова ще е името на таблицата
                                                            // в базата. Конвенцията е класа отговарящ за 
                                                            // обектите да е в единствено число, понеже
                                                            // се отнася за един обект (запис) от таблицата,
                                                            // а колекцията да е в множествено число.


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=192.168.0.105,1433;Database=ExamSkeletonDatabase;User ID=nlyutakov;Password=A123456a;");  // Тук се слага кънекшън стринга.
            }
        }
    }
}
