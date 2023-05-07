using My_Todo_App.Abstraction.Interfaces;
using My_Todo_App.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Todo_App.Utility
{
    public class Utility1 : IUtility
    {
       
        public Utility1()
        {
         
        }
    
        public DataTable ConvertToDataTable(List<TodoItems> todoItems)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("TodoItem", typeof(string));
            dataTable.Columns.Add("Created_At", typeof(DateTime));

            foreach (var todoItem in todoItems)
            {
                DataRow row = dataTable.NewRow();
                row["Id"] = todoItem.Id;
                row["TodoItem"] = todoItem.TodoItem;
                row["Created_At"] = todoItem.Created_At;
                dataTable.Rows.Add(row);

            }
          
            return dataTable;
        }

    }
}
