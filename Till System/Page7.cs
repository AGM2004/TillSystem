using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Till_System
{
    public partial class Page7 : Form
    {
        OleDbConnection connectsource = new OleDbConnection(); // This initializes a new oledbconnection and saves it into a variable called connectsource
        public Page7()
        {
            InitializeComponent();
        }
        private void Page7_Load(object sender, EventArgs e)
        {
            connectsource.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\44730\OneDrive\Documents\Computer Science Project\System Information.accdb;Persist Security Info=False;"; // This gets the string used to get access to the data source and assigns it to the variable connectsource
            employeefill(); // The procedure called employeefill is called
            comboBox1.Text = " --- SELECT ---"; // The text of the combobox is set to a text
        }
        void employeefill() // A procedure called employeefill is created 
        {
            string A = ""; // A new sstring called A is created which is empty
            connectsource.Open(); // This opens the connection between the form and the data source
            OleDbCommand SQLcommand = new OleDbCommand(); // This intializes a new oledbcommand and saves it into the variable SQLcommand
            SQLcommand.Connection = connectsource; // Here the command is linked to the connection string to a specific data source
            SQLcommand.CommandText = "select * from Employee"; // The sql query selects all of the information from the employee table which contains the value typed in textbox1 and and hashed value of the password entered in textbox2 and is then assigned to the variable SQLcommand
            OleDbDataReader reader = SQLcommand.ExecuteReader(); // All of the information that is read from the query that was saved in the variable SQLcommand will be saved into the variable reader
            try// The program tries this set of code first
            {
                while (reader.Read()) // While the reader is reading through the table
                {
                    string P = reader.GetString(1); // A new string is created which is assigned the value of all the items present in the first column of the table the reader is reading thorugh
                    comboBox1.Items.Add(P); // The string is then added to combobox1
                }
            }
            catch (Exception ex) // If code doesnt work then program trys this set of code 
            {
                A = "Exception in DBHandler" + ex; // A is assigned a value plus the error that has occured in the set of code that it had tryed 
                MessageBox.Show(A, "ERROR !!!!"); // The message box then displays the variable A
            }
            finally // The program then at the end does this set of code
            {
                connectsource.Close(); // This closes the connection string between the form and the data source
            }            
        }
        void updatetextboxes() // A procedure called update is created
        {
            connectsource.Open(); // This opens the connection between the form and the data source            
            string A = ""; // I have created a new string variable called A which is empty
            OleDbCommand SQLcommand = new OleDbCommand(); // This intializes a new oledbcommand and saves it into the variable SQLcommand  
            SQLcommand.Connection = connectsource; // Here the command is linked to the connection string to a specific data source
            SQLcommand.CommandText = "SELECT * FROM Employee WHERE Firstname LIKE '%" + comboBox1.SelectedItem + "%'"; // A new SQL query is used to select all of the data from the Employee table where the firstname is the name in the selected index of the combobox 
            OleDbDataReader reader = SQLcommand.ExecuteReader(); // All of the information that is read from the query that was saved in the variable SQLcommand will be saved into the variable reader            
            try // The program tries this set of code first 
            {
                reader.Read(); // The reader is asked to read through the data source
                label1.Text = Convert.ToString(reader.GetInt32(0)); // label1 is assigned the value that the reader gets from the first column of the table that it was reading converted to a string
                textBox1.Text = reader.GetString(1); // textbox1 is assigned the value that the reader gets from the second column of the table that it was reading
                textBox2.Text = reader.GetString(3); // textbox2 is assigned the value that the reader gets from the forth column of the table that it was reading
                textBox3.Text = reader.GetString(2); // textbox3 is assigned the value that the reader gets from the third column of the table that it was reading
                textBox4.Text = reader.GetString(5); // textbox4 is assigned the value that the reader gets from the Seventh column of the table that it was reading
                textBox5.Text = reader.GetString(7); // textbox4 is assigned the value that the reader gets from the Seventh column of the table that it was reading
            }
            catch (Exception ex) // If code doesnt work then program trys this set of code 
            {
                A = "Exception in DBHandler" + ex; // A is assigned a value plus the error that has occured in the set of code that it had tryed 
                MessageBox.Show(A, "ERROR !!!!"); // The message box then displays the variable A
            }
            connectsource.Close(); // This closes the connection string between the form and the data source
        }
        void gridupdate() // A procedure called gridupdate is created
        {            
            OleDbDataAdapter update = new OleDbDataAdapter("SELECT * FROM Sales WHERE EmployeeID = '" + label1.Text + "'", connectsource); // the variable update is assigned a new data adapter which is used to get everything from the Sales table using the varaible connectsource to update the grid view
            DataSet Datas = new DataSet(); // I have created a new dataset which is assigned to the variable Datas            
            update.Fill(Datas, "Sales"); // This gets the updated version of the data source specifically the Sales table from the oledbconnection and fills it into the dataset Datas
            dataGridView1.DataSource = Datas; // Datgrid is set to show all of the information saved inside the the dataset Datas
            dataGridView1.DataMember = "Sales"; // This sets the data source table that needs to be displayed in gridview1
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            updatetextboxes(); // The procedure called updatetextboxes is called 
            gridupdate(); // The procedure called gridupdate is called
            label3.Text = Convert.ToString(dataGridView1.Rows.Count - 1); // This saves the number of rows inside the table minus 1 into the label
        }
        private void button1_Click(object sender, EventArgs e)
        {            
            string A = ""; // I have created a new string variable called A which is empty
            connectsource.Open(); // This opens the connection between the form and the data source
            OleDbCommand SQLcommand = new OleDbCommand(); // This intializes a new oledbcommand and saves it into the variable SQLcommand  
            SQLcommand.Connection = connectsource; // Here the command is linked to the connection string to a specific data source
            try // The program tries this set of code first 
            {
                string phoneno = textBox2.Text;
                char first = phoneno[0];
                string ni = textBox5.Text;
                if (Convert.ToInt16(textBox4.Text) < 3 && Convert.ToInt16(textBox4.Text) >= 0)
                {
                    if (char.IsLetter(ni, 0) == true && ni[0] == ni[1] && char.IsNumber(ni, 2) == true && char.IsNumber(ni, 3) == true && char.IsNumber(ni, 4) == true && char.IsNumber(ni, 5) == true && char.IsNumber(ni, 6) == true && char.IsNumber(ni, 7) == true && char.IsLetter(ni, 8) == true && ni.Length == 9)
                    {
                        if (first.Equals('0') && phoneno.Length == 11)
                        {                            
                            SQLcommand.CommandText = "Update Employee SET Firstname = '" + textBox1.Text + "', Surname = '" + textBox3.Text + "', PhoneNo = '" + textBox2.Text + "', NI_No = '" + textBox5.Text + "', Authorise = '" + textBox4.Text + "'  WHERE Username = '" + comboBox1.SelectedItem + "'"; // A new SQLcommand is created which updates the authorization of an employee of the selected employee
                            SQLcommand.ExecuteNonQuery(); // This executes the sql query saved in the variable SQLcommand
                            A = "SAVED"; // The variable A is set a string value
                            MessageBox.Show(A); // The message box displays the variable A
                        }
                        else
                        {
                            MessageBox.Show("Phone number is not in correct format", "ERROR!!!!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ni Number is not in correct format","ERROR!!!!");
                    }
                }
                else
                {
                    MessageBox.Show("Authorization not within paramters", "ERROR!!!!");
                }
                // This is validating the format of the authorization, phone number and NI number
            }
            catch (Exception ex) // If code doesnt work then program trys this set of code 
            {
                A = "Exception in DBHandler" + ex; // A is assigned a value plus the error that has occured in the set of code that it had tried 
                MessageBox.Show(A, "ERROR !!!!"); // The message box then displays the variable A
            }
            finally // The program then at the end does this set of code
            {
                connectsource.Close(); // This closes the connection string between the form and the data source
            }

            textBox1.ReadOnly = true; // The readonly attribute is set to true
            textBox2.ReadOnly = true; // The readonly attribute is set to true
            textBox3.ReadOnly = true; // The readonly attribute is set to true
            textBox4.ReadOnly = true; // The readonly attribute is set to true
            textBox5.ReadOnly = true; // The readonly attribute is set to true
        }
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.ReadOnly = false; // The readonly attribute is set to false
            textBox2.ReadOnly = false; // The readonly attribute is set to false
            textBox3.ReadOnly = false; // The readonly attribute is set to false
            textBox4.ReadOnly = false; // The readonly attribute is set to false
            textBox5.ReadOnly = false; // The readonly attribute is set to false
        }
    }
}
