using My_Todo_App.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Todo_App.Abstraction.Interfaces
{
    public interface IUtility
    {

       DataTable ConvertToDataTable(List<TodoItems> todoItems);
    }
}
