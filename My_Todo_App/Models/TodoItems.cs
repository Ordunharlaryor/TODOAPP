using Microsoft.EntityFrameworkCore;
using My_Todo_App.Models;
using My_Todo_App.MyTodoContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Todo_App.Models
{
    public class TodoItems
    {
       public int Id { get; set; } 
       public string TodoItem { get; set; }
       public DateTime Created_At  { get; set; }
       
        public TodoItems()
        {
            Created_At = DateTime.Now;
        }
    }
}

