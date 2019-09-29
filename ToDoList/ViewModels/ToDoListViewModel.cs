using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Helpers;
using ToDoList.Models;
using Dapper;

namespace ToDoList.ViewModels
{
    public class ToDoListViewModel
    {
        public ToDoListViewModel()
        {
            using(var db = DbHelper.GetConnection())
            {
                this.EditableItem = new ToDoListItem();
                this.ToDoItems = db.Query<ToDoListItem>("SELECT * FROM ToDoListItems ORDER BY AddDate DESC").ToList();
            }
        }

        public List<ToDoListItem> ToDoItems { get; set; }
        public ToDoListItem EditableItem { get; set; }
    }
}
