using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Configuration;
using System.Data.OleDb;

namespace Till_System
{
    public partial class Page5 : Form
    {
        OleDbConnection connectsource = new OleDbConnection(); // This initializes a new oledbconnection and saves it into a variable called connectsource
        public Page5()
        {
            InitializeComponent();            
        }
        private void Page5_Load(object sender, EventArgs e)
        {
            connectsource.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\44730\OneDrive\Documents\Computer Science Project\System Information.accdb;Persist Security Info=False;"; // This gets the string used to get access to the data source and assigns it to the variable connectsource
            grid_update(); // This call the function grid_update
            label4.Text = DateTime.Now.ToShortDateString(); // Textbox5 is assigned the date of when the form is loaded up
        }
        private void button1_Click(object sender, EventArgs e)
        {
            connectsource.Open(); // This opens the connection between the form and the data source
            string A = ""; // I have created a new string variable called A which is empty
            try // The program tries this set of code first 
            {
                if ((textBox1.Text != "" && Convert.ToDouble(textBox2.Text) != 0) || (textBox3.Text != "" && Convert.ToDouble(textBox4.Text) != 0))
                {
                    OleDbCommand SQLcommand = new OleDbCommand(); // This intializes a new oledbcommand and saves it into the variable SQLcommand  
                    SQLcommand.Connection = connectsource; // Here the command is linked to the connection string to a specific data source
                    if (Convert.ToDouble(textBox2.Text) >= 0 && Convert.ToDouble(textBox4.Text) >= 0)
                    {
                        SQLcommand.CommandText = "INSERT INTO Earning (Expenses,OutAmount,Income,InAmount,TransactionDate) VALUES('" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "' , '" + label4.Text + "')"; // The sql query inserts all of the values typed in the text boxes into the given fields inside the datasource and this query is then assigned to the variable SQLcommand
                        SQLcommand.ExecuteNonQuery(); // This executes the sql query saved in the variable SQLcommand 
                        A = "completed"; // The variable A is set a string value
                        grid_update(); // This calls the function grid_update
                        MessageBox.Show(A, "Transaction Recorded"); // The message box displays the variable A
                    }
                    else
                    {
                        MessageBox.Show("Amount is not within the parameters", "ERROR!!!!");
                    }
                }
                else
                {
                    MessageBox.Show("Please fill in both amount and name text box to continue","ERROR!!!");
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

        void profitcalulator()
        {
            try
            {
                int income = 0;
                int expense = 0;
                for (int a = 0; a < dataGridView1.RowCount; a++)
                {
                    income = income + Convert.ToInt16(dataGridView1.Rows[a].Cells[4].Value);
                    expense = expense + Convert.ToInt16(dataGridView1.Rows[a].Cells[2].Value);
                }
                // It adds the values that aren't null to the variables
                if (income > expense)
                {
                    label5.Text = "Profit";
                    label5.BackColor = System.Drawing.Color.LightGreen;
                }
                else
                {
                    label5.Text = "Loss";
                    label5.BackColor = System.Drawing.Color.Red;
                }
                // this checks which varauble is greater and edits the label accordingly
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void grid_update() // I have created a new procedure called grid_update
        {
            string date = dateTimePicker1.Value.ToShortDateString(); // The string date is assigned the value of the date chosen at the date and time picker
            OleDbDataAdapter update = new OleDbDataAdapter("SELECT * FROM Earning WHERE TransactionDate = '" + date + "' ", connectsource); // the variable update is assigned a new data adapter which is used to get everything from the earning table using the varaible connectsource to update the grid view
            DataSet Datas = new DataSet(); // I have created a new dataset which is assigned to the variable Datas
            update.Fill(Datas, "Transfer"); // This gets the updated version of the data source specifically the earniing table from the oledbconnection and fills it into the dataset Datas
            dataGridView1.DataSource = Datas; // Datgrid is set to show all of the information saved inside the the dataset Datas
            dataGridView1.DataMember = "Transfer"; // This sets the data source table that needs to be displayed in gridview1
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Page7 employeepage = new Page7(); // The varaible employeepage is assigned a new link to page7
            employeepage.Show(); // I am calling the variable employeepage and asking it to show the data assigned to it            
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            grid_update(); // The function called grid_update is called
            profitcalulator();   
        }
    }
}
