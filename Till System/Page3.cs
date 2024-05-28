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
    public partial class Page3 : Form
    {        
        OleDbConnection connectsource = new OleDbConnection(); // This initializes a new oledbconnection and saves it into a variable called connectsource
        string A = ""; // I have created a new string variable called A which is empty  
        bool buttonccheckclick = false; // A new bool varaible called buttonccheckclick is created and is set to false
        public Page3()
        {
            InitializeComponent();            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Page2 login = new Page2(); // The varaible login is assigned a new link to page2
            login.Show(); // I am calling the variable login and asking it to show the data assigned to it
            this.Hide(); // This hides the current page (Page1) from the user
        }       
        private void Page3_Load(object sender, EventArgs e)
        {
            connectsource.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\44730\OneDrive\Documents\Computer Science Project\System Information.accdb;Persist Security Info=False;"; // This gets the string used to get access to the data source and assigns it to the variable connectsource
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "") // This checks is any of the texboxes are empty
            {
                MessageBox.Show("Please Fill in all of the textboxes to proceed", "ERROR!!!!!"); // A messagebox is used to show a textmessage to alert the user
            }
            else
            {
                try
                {
                    string ni = textBox6.Text;
                    string phoneno = textBox3.Text;
                    char first = phoneno[0];
                    bool fnameint = textBox1.Text.All(char.IsLetter);
                    bool snameint = textBox2.Text.All(char.IsLetter);
                    if (char.IsLetter(ni, 0) == true && ni[0] == ni[1] && char.IsNumber(ni, 2) == true && char.IsNumber(ni, 3) == true && char.IsNumber(ni, 4) == true && char.IsNumber(ni, 5) == true && char.IsNumber(ni, 6) == true && char.IsNumber(ni, 7) == true && char.IsLetter(ni, 8) == true && ni.Length == 9)
                    {
                        if (first.Equals('0') && phoneno.Length == 11)
                        {
                            if (fnameint == true && snameint == true)
                            {
                                connectsource.Open(); // This opens the connection between the form and the data source
                                OleDbCommand SQLcommand = new OleDbCommand(); // This intializes a new oledbcommand and saves it into the variable SQLcommand
                                SQLcommand.Connection = connectsource; // Here the variable SQLcommand is linked to the connection string to a specific data source             
                                try // The program tries this set of code first 
                                {
                                    SQLcommand.CommandText = "INSERT INTO Employee (Firstname,Surname,PhoneNo,Username,Authorise,HashPass,NI_No) VALUES('" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "',' 0 ','" + textBox5.GetHashCode().ToString() + "','" + textBox6.Text + "')"; // The sql query inserts all of the values typed in the text boxes into the given fields inside the datasource and this query is then assigned to the variable SQLcommand
                                    SQLcommand.ExecuteNonQuery(); // This executes the sql query saved in the variable SQLcommand 
                                    A = "completed"; // The variable A is set a string value
                                    MessageBox.Show(A); // The message box displays the variable A
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
                            else
                            {
                                MessageBox.Show("First name or Surname is not in the right format","ERROR!!!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Phone number is not in correct format", "ERROR!!!!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ni Number is not in correct format", "ERROR!!!!");
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString(),"ERROR!!");
                }
            }            
        }        
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (buttonccheckclick == true) // This checks is the variable buttonccheckclick is true
            {
                textBox5.PasswordChar = '*'; // This sets the property passwordchar of the textbox to *                
            }
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            buttonccheckclick = true; // this sets the bool variable buttonccheckclick to true 
            textBox5.PasswordChar = '\0'; // This sets the property passwordchar of the textbox to null (empty)   
        }
    }
}
