using Microsoft.EntityFrameworkCore;
using My_Todo_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Todo_App.MyTodoContext
{
    public class MyTodoContext1 : DbContext
    {
        
        public DbSet<TodoItems> TodoItems1 { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-NDP7GE2;Initial Catalog=MyTodoApp1;Integrated Security=True");
        }

       
        }
    }
