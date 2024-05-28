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
    public partial class Page8 : Form
    {
        OleDbConnection connectsource = new OleDbConnection(); // This initializes a new oledbconnection and saves it into a variable called connectsource
        string A = ""; // A new string called A is created and is empty
        bool buttonccheckclick = false; // A new bool varaible called buttonclicked is created and is set to false
        public Page8()
        {
            InitializeComponent();
            connectsource.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\44730\OneDrive\Documents\Computer Science Project\System Information.accdb;Persist Security Info=False;"; // This gets the string used to get access to the data source and assigns it to the variable connectsource
        }
        private void Page8_Load(object sender, EventArgs e)
        {}
        private void button1_Click(object sender, EventArgs e)
        {
            connectsource.Open(); // This opens the connection between the form and the data source  
            OleDbCommand SQLcommand = new OleDbCommand(); // This intializes a new oledbcommand and saves it into the variable SQLcommand
            SQLcommand.Connection = connectsource; // Here the command is linked to the connection string to a specific data source 
            SQLcommand.CommandText = "select * from Employee where Username = '" + textBox1.Text + "' and NI_No ='" + textBox2.Text + "'"; // The sql query selects all of the information from the employee table which contains the value typed in textbox1 and and hashed value of the password entered in textbox2 and is then assigned to the variable SQLcommand
            OleDbDataReader reader = SQLcommand.ExecuteReader(); // All of the information that is read from the query that was saved in the variable SQLcommand will be saved into the variable reader
            if (reader.Read() == true) // This checks if the text entered in the textbox is available inside the data source and if it is then it will return true 
            {                
                if (textBox3.Text == textBox4.Text) // It checks if text enetred in the confirm texbox and new pass texbox are the same
                {
                    OleDbCommand SQLcommand2 = new OleDbCommand(); // This intializes a new oledbcommand and saves it into the variable SQLcommand2
                    SQLcommand2.Connection = connectsource; // Here the variable SQLcommand2 is linked to the connection string to a specific data source             
                    try // The program tries this set of code first 
                    {
                        SQLcommand2.CommandText = "UPDATE Employee SET HashPass = " + textBox4.Text.GetHashCode().ToString() + " WHERE Username = '" + textBox1.Text + "'"; // The sql query updates the hashpass of the employee trying to chnage their password in the Employee table 
                        SQLcommand2.ExecuteNonQuery(); // This executes the sql query saved in the variable SQLcommand 
                        A = "Completed"; // The variable A is set a string value
                        MessageBox.Show(A); // The message box displays the variable A
                    }
                    catch (Exception ex) // If code doesnt work then program trys this set of code 
                    {
                        A = "Exception in DBHandler" + ex; // A is assigned a value plus the error that has occured in the set of code that it had tryed 
                        MessageBox.Show(A, "ERROR !!!!"); // The message box then displays the variable A
                    }                    
                }
                else if (textBox3.Text != textBox4.Text) // This checks if the confirm pass and new pass dont match 
                {
                    MessageBox.Show("New Password Entered does not match with Confirm Password","ALERT !!!!"); // A messagebox is used to show a text
                }
            }
            else
            {
                MessageBox.Show("Username or password is incorrect", "Error!!!"); // If the reader doesnt return the value 1 than this message box is showed
            }
            connectsource.Close();
        }        
        private void button2_Click(object sender, EventArgs e)
        {
            buttonccheckclick = true; // this sets the bool variable buttonccheckclick to true 
            textBox3.PasswordChar = '\0'; // This sets the property passwordchar of the textbox to null (empty)
            textBox4.PasswordChar = '\0'; // This sets the property passwordchar of the textbox to null (empty)
        }
        private void timer1_Tick(object sender, EventArgs e)
        {            
            if (buttonccheckclick == true) // This checks is the variable buttonccheckclick is true
            {
                textBox3.PasswordChar = '*'; // This sets the property passwordchar of the textbox to *
                textBox4.PasswordChar = '*'; // This sets the property passwordchar of the textbox to *
            }                       
        }
    }
}
