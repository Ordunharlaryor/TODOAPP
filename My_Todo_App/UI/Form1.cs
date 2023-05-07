using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Threading;
using My_Todo_App.Abstraction.Interfaces;
using My_Todo_App.Utility;
using My_Todo_App.MyTodoContext;
using My_Todo_App.Models;
using Microsoft.EntityFrameworkCore;
//using System.Threading.Task;

namespace My_Todo_App
{
    public partial class Form1 : Form
    {

      
        private readonly DataTable dataTable = new DataTable();
        private readonly IUtility _Utility;
        private readonly MyTodoContext1 _MyTodoContext;


        public Form1()
        {
            
            InitializeComponent();
            _Utility = new Utility1();
            _MyTodoContext = new MyTodoContext1();
           
            
        }
        //LOAD DATA FROM DATABASE

        public async Task LoadDataAsync()
        {
            try
            {
                string searchTerm = textBox2.Text.Trim();

                // Load all data from the TodoTable asynchronously
                var todoItems = await _MyTodoContext.TodoItems1.ToListAsync();

                // Filter the data based on the search term
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    todoItems = todoItems.Where(i => i.TodoItem.StartsWith(searchTerm)).ToList();
                }

                // Update the UI with the filtered data
                Invoke(new Action(() =>
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Clear();
                    dataTable.Merge(_Utility.ConvertToDataTable(todoItems));
                    dataGridView1.DataSource = dataTable;
                }));
            }
            catch (Exception ex)
            {
                // Handle the exception
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
      
        public async void Form1_Load(object sender, EventArgs e)
        {
            await Task.Run(() => LoadDataAsync());
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private async void dataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int id = Convert.ToInt32(e.Row.Cells["Id"].Value);

            using (MyTodoContext1 context = new MyTodoContext1())
            {
                TodoItems todoItem = await context.TodoItems1.FindAsync(id);

                context.TodoItems1.Remove(todoItem);

                await context.SaveChangesAsync();
            }
        }




        private void label3_Click(object sender, EventArgs e)
        {

        }


        //ADD ITEMS
        private async void button2_Click(object sender, EventArgs e)
        {
            string todoItem = textBox1.Text.Trim();

            if (!string.IsNullOrEmpty(todoItem))
            {
                try
                {
                    using (var context = new MyTodoContext1())
                    {
                        var newTodoItem = new TodoItems
                        {
                            TodoItem = todoItem,
                            Created_At = DateTime.Now,
                        };

                        context.TodoItems1.Add(newTodoItem);
                        await context.SaveChangesAsync();
                    }

                    await LoadDataAsync();
                }
                catch (Exception ex)
                {
                    // Handle exception here
                    MessageBox.Show("An error occurred while saving the todo item: " + ex.Message);
                }
            }
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


        //DELETE ITEMS

        private async void dataGridView1_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
                {
                    int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);

                    using (var context = new MyTodoContext1())
                    {
                        var todoItem = await context.TodoItems1.FindAsync(id);

                        if (todoItem != null)
                        {
                            context.TodoItems1.Remove(todoItem);
                            await context.SaveChangesAsync();

                            dataGridView1.Rows.RemoveAt(e.RowIndex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //SEARCH
        private async void textBox2_TextChanged_1(object sender, EventArgs e)
        {
          await  LoadDataAsync();
        }

        //MAKE WHATEVER YOU WANT TO EDIT POP UP IN TEXTBOX1

        private async void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                    // Get the selected TodoItem from the database
                    int id = int.Parse(row.Cells["Id"].Value.ToString());
                    TodoItems todoItem = await _MyTodoContext.TodoItems1.FindAsync(id);

                    // Display the selected TodoItem in a textbox for editing
                    textBox1.Text = todoItem.TodoItem;
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string todoItem = textBox1.Text.Trim();

            if (!string.IsNullOrEmpty(todoItem))
            {
                try
                {
                    using (var context = new MyTodoContext1())
                    {
                        var newTodoItem = new TodoItems
                        {
                            TodoItem = todoItem,
                            Created_At = DateTime.Now,
                        };

                        context.TodoItems1.Add(newTodoItem);
                        await context.SaveChangesAsync();
                    }

                    await LoadDataAsync();
                }
                catch (Exception ex)
                {
                    // Handle exception here
                    MessageBox.Show("An error occurred while saving the todo item: " + ex.Message);
                }
            }
        }

        private void dataGridView1_AllowUserToDeleteRowsChanged(object sender, EventArgs e)
        {
            // Check if the last row is empty
            int lastIndex = dataGridView1.Rows.Count - 1;
            DataGridViewRow lastRow = dataGridView1.Rows[lastIndex];
            if (lastRow.Cells[0].Value == DBNull.Value || lastRow.Cells[0].Value == null)
            {
                // Remove the last row
                dataGridView1.Rows.RemoveAt(lastIndex);
            }
        }
    }
    }

       
  
   

