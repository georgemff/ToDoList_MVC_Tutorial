using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Models
{
    public class ToDoListItem
    {
        public int Id { get; set; }
        public DateTime AddDate { get; set; }
        
        [Required]
        [MinLength(2, ErrorMessage ="Title must containt at least two characters")]
        [MaxLength(200, ErrorMessage ="Title length must be less than 200 chracters")]
        public string Title { get; set; }
        public bool IsDone { get; set; }

    }
}
