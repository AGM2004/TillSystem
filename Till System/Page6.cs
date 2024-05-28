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
    public partial class Page6 : Form
    {
        OleDbConnection connectsource = new OleDbConnection(); // This initializes a new oledbconnection and saves it into a variable called connectsource
        public Page6()
        {
            InitializeComponent();
        }
        private void Page6_Load(object sender, EventArgs e)
        {
            connectsource.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\44730\OneDrive\Documents\Computer Science Project\System Information.accdb;Persist Security Info=False;"; // This gets the string used to get access to the data source and assigns it to the variable connectsource            
            grid_update(); // The function called grid_update is called
        }
        public void grid_update()
        {
            OleDbDataAdapter update = new OleDbDataAdapter("SELECT * FROM Products", connectsource); // the variable update is assigned a new data adapter which is used to get everything from the products table using the varaible connectsource to update the grid view
            DataSet Datas = new DataSet(); // I have created a new dataset which is assigned to the variable Datas
            update.Fill(Datas, "Products"); // This gets the updated version of the data source specifically the earniing table from the oledbconnection and fills it into the dataset Datas
            dataGridView1.DataSource = Datas; // Datgrid is set to show all of the information saved inside the the dataset Datas
            dataGridView1.DataMember = "Products"; // This sets the data source table that needs to be displayed in gridview1        
        }
        private void button1_Click(object sender, EventArgs e)
        {
            connectsource.Open(); // This opens the connection between the form and the data source
            string A = ""; // I have created a new string variable called A which is empty
            OleDbCommand SQLcommand = new OleDbCommand(); // This intializes a new oledbcommand and saves it into the variable SQLcommand  
            SQLcommand.Connection = connectsource; // Here the command is linked to the connection string to a specific data source
            SQLcommand.CommandText = "SELECT stock FROM Products where Product_name = '" + textBox4.Text + "'"; // A new SQLcommand is created which updates the items entered in the texbox to the the given stock entred in the other textbox
            OleDbDataReader stockread = SQLcommand.ExecuteReader(); // All of the information that is read from the query that was saved in the variable SQLcommand will be saved into the variable stockread
            try // The program tries this set of code first 
            {
                stockread.Read(); // The reader is asked to read through the data source
                int oldstock = Convert.ToInt32(stockread.GetString(0)); // The infromation inside the fistcolumn of the data retrieved from the database is saved into the varaible old stock
                stockread.Close(); // The reader is asked to stop reading
                int inputstock = Convert.ToInt32(textBox2.Text); // The new varaible is assigned the value of the number entered in the text box
                int newstock = oldstock + inputstock; // A new varaible newstock saves the sum of the varaible oldstock and inputstock
                SQLcommand.CommandText = "Update Products SET stock = '" + newstock + "' where Product_name = '" + textBox4.Text + "'"; // A new SQLcommand is created which updates the items entered in the texbox to the the given stock entred in the other textbox
                SQLcommand.ExecuteNonQuery(); // This executes the sql query saved in the variable SQLcommand
                A = "SAVED"; // The variable A is set a string value
                grid_update(); // This calls the function grid_update
                MessageBox.Show(A); // The message box displays the variable A
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
            textBox1.Clear(); // Textbox1 is set to get cleared 
            textBox2.Clear(); // Textbox2 is set to get cleared
            textBox3.Clear(); // Textbox3 is set to get cleared
            textBox4.Clear(); // Textbox4 is set to get cleared
        }
        private void button2_Click(object sender, EventArgs e)
        {
            connectsource.Open(); // This opens the connection between the form and the data source
            string A = ""; // I have created a new string variable called A which is empty
            OleDbCommand SQLcommand = new OleDbCommand(); // This intializes a new oledbcommand and saves it into the variable SQLcommand  
            SQLcommand.Connection = connectsource; // Here the command is linked to the connection string to a specific data source            
            try // The program tries this set of code first 
            {
                SQLcommand.CommandText = "DELETE FROM Products WHERE Product_name = '" + textBox4.Text + "'"; // A new SQLcommand is created which which deleted the product using the product_name entered in the textbox 
                SQLcommand.ExecuteNonQuery(); // This executes the sql query saved in the variable SQLcommand 
                A = "DELETED"; // The variable A is set a string value
                grid_update(); // This calls the function grid_update
                MessageBox.Show(A); // The message box displays the variable A
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
            textBox1.Clear(); // Textbox1 is set to get cleared
            textBox2.Clear(); // Textbox2 is set to get cleared
            textBox3.Clear(); // Textbox3 is set to get cleared
            textBox4.Clear(); // Textbox4 is set to get cleared
        }
        private void button3_Click(object sender, EventArgs e)
        {
            connectsource.Open(); // This opens the connection between the form and the data source
            string A = ""; // I have created a new string variable called A which is empty
            OleDbCommand SQLcommand = new OleDbCommand(); // This intializes a new oledbcommand and saves it into the variable SQLcommand  
            SQLcommand.Connection = connectsource; // Here the command is linked to the connection string to a specific data source            
            try // The program tries this set of code first 
            {
                SQLcommand.CommandText = "INSERT INTO Products (Product_name, stock, price) VALUES ('" + textBox4.Text + "','" + textBox2.Text + "','" + int.Parse(textBox3.Text) + "')"; // An sql query is used to insert in new products into the products table from the values written in the textboxes
                SQLcommand.ExecuteNonQuery(); // This executes the sql query saved in the variable SQLcommand 
                A = "ADDED"; // The variable A is set a string value
                grid_update(); // This calls the function grid_update
                MessageBox.Show(A); // The message box displays the variable A
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
            textBox1.Clear(); // Textbox1 is set to get cleared
            textBox2.Clear(); // Textbox2 is set to get cleared
            textBox3.Clear(); // Textbox3 is set to get cleared
            textBox4.Clear(); // Textbox4 is set to get cleared
        }
    }
    
}
