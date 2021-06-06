using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSkeleton.Models
{
    public class CreateVM
    {
        [Required(ErrorMessage = "*This field is required")]
        public string ColumnOne { get; set; }
        [Required(ErrorMessage = "*This field is required")]
        public string ColumnTwo { get; set; }
        [Required(ErrorMessage = "*This field is required")]
        public string ColumnThree { get; set; }
        [Required(ErrorMessage = "*This field is required")]
        public string ColumnFour { get; set; }
    }
}
