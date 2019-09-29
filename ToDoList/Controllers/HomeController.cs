using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Helpers;
using ToDoList.Models;
using ToDoList.ViewModels;
using Dapper.Contrib.Extensions;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ToDoListViewModel viewModel = new ToDoListViewModel();
            return View("Index", viewModel);
        }

        public IActionResult Edit(int id)
        {
            ToDoListViewModel viewModel = new ToDoListViewModel();
            viewModel.EditableItem = viewModel.ToDoItems.FirstOrDefault(x => x.Id == id);
            return View("Index", viewModel);
        }

        public IActionResult Delete(int id)
        {
            using(var db = DbHelper.GetConnection())
            {
                ToDoListItem item = db.Get<ToDoListItem>(id);
                if(item != null)
                {
                    db.Delete(item);
                }
                return RedirectToAction("Index");
            }
        }

        public IActionResult CreateUpdate(ToDoListViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                using(var db = DbHelper.GetConnection())
                {
                    if(viewModel.EditableItem.Id <= 0)
                    {
                        viewModel.EditableItem.AddDate = DateTime.Now;
                        db.Insert<ToDoListItem>(viewModel.EditableItem);
                    }
                    else
                    {
                        ToDoListItem dbItem = db.Get<ToDoListItem>(viewModel.EditableItem.Id);
                        var result = TryUpdateModelAsync<ToDoListItem>(dbItem, "EditableItem");
                        db.Update<ToDoListItem>(dbItem);
                    }
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View("Index", new ToDoListViewModel());
            }
        }

        public  IActionResult ToggleIsDone(int id)
        {
            using(var db = DbHelper.GetConnection())
            {
                ToDoListItem item = db.Get<ToDoListItem>(id);
                if(item != null)
                {
                    item.IsDone = !item.IsDone;
                    db.Update<ToDoListItem>(item);
                }
                return RedirectToAction("Index");
            }
        }
    }
}