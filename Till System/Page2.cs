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
    public partial class Page2 : Form
    {
        OleDbConnection connectsource = new OleDbConnection(); // This initializes a new oledbconnection and saves it into a variable called connectsource
        public bool manager, owner; // I have created a public bool called manager and owner
        bool buttonccheckclick = false; // A new bool varaible called buttonccheckclick is created and is set to false
        public Page2()
        {
            InitializeComponent();
            connectsource.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\44730\OneDrive\Documents\Computer Science Project\System Information.accdb;Persist Security Info=False;"; // This gets the string used to get access to the data source and assigns it to the variable connectsource         
        }
        private void button1_Click(object sender, EventArgs e)
        {
            connectsource.Open(); // This opens the connection between the form and the data source  
            OleDbCommand SQLcommand = new OleDbCommand(); // This intializes a new oledbcommand and saves it into the variable SQLcommand
            SQLcommand.Connection = connectsource; // Here the command is linked to the connection string to a specific data source 
            SQLcommand.CommandText = "select * from Employee where Username = '" + textBox1.Text + "' and HashPass ='" + textBox2.Text.GetHashCode() + "'"; // The sql query selects all of the information from the employee table which contains the value typed in textbox1 and and hashed value of the password entered in textbox2 and is then assigned to the variable SQLcommand
            OleDbDataReader reader = SQLcommand.ExecuteReader(); // All of the information that is read from the query that was saved in the variable SQLcommand will be saved into the variable reader
            if (reader.Read() == true) // This checks if the text entered in the textbox is available inside the data source and if it is then it will return true 
            {                
                if ((string)reader[5] == "1" ) // This checks if the string value saved in the fifth index is = 1 
                {
                    manager = true; // the value true is stroed in the variable auth
                }
                if ((string)reader[5] == "2") // This checks if the string value saved in the fifth index is = 2
                {
                    owner = true; // the value true is stored in the variable auth
                }
                int employeeid = (int)reader[0]; // A new int variable called employee id saves the value in the first column that the reader reads from the employee table 
                Page4 main = new Page4(manager, employeeid.ToString() , owner); // The varaible main is assigned a new link to page4 and variable auth and employeeid is passed through to page4
                main.Show(); // I am calling the variable main and asking it to show the data assigned to it
                this.Hide(); // This hides the current page (Page2) from the user
            }
            else
            {
                MessageBox.Show("Username or password is incorrect" , "Error!!!"); // If the reader doesnt return the value 1 than this message box is showed
            }
            connectsource.Close(); // This closes the connection string between the form and the data source
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Page8 forgot = new Page8(); // The varaible forgot is assigned a new link to page8
            forgot.Show(); // I am calling the variable forgot and asking it to show the data assigned to it
        }        
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (buttonccheckclick == true) // This checks is the variable buttonccheckclick is true
            {
                textBox2.PasswordChar = '*'; // This sets the property passwordchar of the textbox to *                
            }
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            buttonccheckclick = true; // this sets the bool variable buttonccheckclick to true 
            textBox2.PasswordChar = '\0'; // This sets the property passwordchar of the textbox to null (empty) 
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Page3 sign_up = new Page3(); // The varaible sign_up is assigned a new link to page2
            sign_up.Show(); // I am calling the variable sign_up and asking it to show the data assigned to it
            this.Hide(); // This hides the current page (Page1) from the user
        }
        private void Page2_Load(object sender, EventArgs e)
        {}
    }
}
