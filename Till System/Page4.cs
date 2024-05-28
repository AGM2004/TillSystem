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
using System.IO;

namespace Till_System
{
    public partial class Page4 : Form
    {
        OleDbConnection connectsource = new OleDbConnection(); // This initializes a new oledbconnection and saves it into a variable called connectsource
        double total; // A new variable with data type double called total is created
        bool manage, own; // A new bool variable called manage and own is created
        string employee; // A new string varaible called employeeid is intialized
        public Page4(bool manager, string employeeid , bool owner)
        {
            InitializeComponent();
            manage = manager; // The variable manage is assigned the value of manager that was passed from page2
            employee = employeeid; // The variable employee is assigned the value of employeeid that was passed from page2
            own = owner; // The variable own is assigned the value of owner that was passed from page2 
        }
        private void Page4_Load(object sender, EventArgs e)
        {            
            connectsource.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\44730\OneDrive\Documents\Computer Science Project\System Information.accdb;"; // This gets the string used to get access to the data source and assigns it to the variable connectsource           
            comboBox1.Text = " --- SELECT --- "; // Combobox1 is set to display a text
            comboBox2.Text = " --- SELECT --- "; // Combobox2 is set to display a text
            if (manage == true) // This checks if the variable manage is true
            {                
                button4.Visible = true; // If is auth is true then button2 will be visisble to the user
            }
            if (own == true) // This checks if the variable own is true
            {
                button2.Visible = true; // If is auth is true then button2 will be visisble to the user
                button4.Visible = true; // If is auth is true then button4 will be visisble to the user
            }            
            productlistfill(); // The function productlistfill is called            
        }
        void productlistfill() // A procedure called productlistfill is created 
        {            
            string A = ""; // A new sstring called A is created which is empty
            connectsource.Open(); // This opens the connection between the form and the data source  
            OleDbCommand SQLcommand = new OleDbCommand(); // This intializes a new oledbcommand and saves it into the variable SQLcommand
            SQLcommand.Connection = connectsource; // Here the command is linked to the connection string to a specific data source 
            SQLcommand.CommandText = "select * from Products"; // The sql query selects all of the information from the employee table which contains the value typed in textbox1 and and hashed value of the password entered in textbox2 and is then assigned to the variable SQLcommand
            OleDbDataReader reader = SQLcommand.ExecuteReader(); // All of the information that is read from the query that was saved in the variable SQLcommand will be saved into the variable reader
            string alert = null; // A new string called null is created which is then assigned the value null
            try// The program tries this set of code first
            {
                while (reader.Read()) // While the reader is reading through the table
                {
                    if (Convert.ToInt16(reader.GetString(2)) < 10) // If the second colum in the table the reader is reading is < 10
                    {
                        alert += reader.GetString(1) + "\n"; // The name of the product from column 1 is assigned to the variable alert 
                    }
                    string P = reader.GetString(1); // A new string is created which is assigned the value of all the items present in the first column of the table the reader is reading thorugh
                    comboBox1.Items.Add(P); // The string is then added to combobox1
                }
                MessageBox.Show(" THE FOLLOWING ITEMS NEED RESTOCKING " + "\n" + "\n" + alert, "ALERT !!!!!!"); // The message box shows the names of all the names saved in the variable alert 
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
        class Blist // An class called Blist is created 
        {
            public string PName { get; set; } // a string list called PName is created which sets the value that it recieves in a new index
            public string Quantity { get; set; } // a string list called Quantity is created which sets the value that it recieves in a new index 
            public string Price { get; set; } // a string list called Price is created which sets the value that it recieves in a new index 
        }
        private List<Blist> GetBasketList(int Price, int quant) // This creates a list objects called GetBasketList 
        {
            int price = Price;
            int Quant = quant;
            double Quantity;  // A new variable called quantbuffer is introduced
            if ( comboBox2.SelectedIndex < 9) // This checks if the selected index in combobox2 is less than 9
            {
                Quantity = comboBox2.SelectedIndex + 1;  // The value of quantbuffer is set to the index of the combo box + 1
            }
            else
            {
                Quantity = double.Parse(textBox3.Text); // A new float variable called quanbuffer is created which saves the value enetred in textbox3
            }
            double singleprice = double.Parse(textBox2.Text); // A new float varaible called singleprice is created which takes the float of the value displayed in textbox2           
            double Tprice = singleprice * Quantity + price; // A float variable called priceBuffer is created which saves the value of product of the 2 variables (singleprice and quantbuffer)
            total = total + Tprice - price;
            textBox6.Text = total.ToString(); // Text Box6 displays the variable total converted to a string
            var Columlist = new List<Blist>(); // A variable called Columlist is now binded to lisviewbasket and the colomns inside it 
            Columlist.Add(new Blist() { PName = textBox4.Text, Quantity = Quant.ToString(), Price = Tprice.ToString() }); // This adds new values to the varaible Columlist and the list that it is binded to, It adds the value in textbox4 to the column called PName, Coloum Quantity saves the value in varaible quantity and Coloumn Price svaes the value in Tprice 
            return Columlist; // This returns the varaible Columlist
        }
        void addlisttolistview(int price, int quantity) // A new procedure called addlisttolistview is created 
        {
            int Price = price;
            int quant = quantity; 
            var products = GetBasketList(Price, quant); // The variable products gets assigned the list called getbaskterlist 
            foreach (var product in products) // A froeach loop goes through all the items present in the variable called products
            {
                var row = new string[] { product.PName, product.Quantity, product.Price }; // A varaible called row is assigned a new string array which contains 3 indexis, the first index saves the product name
                var add = new ListViewItem(row); // a new variable called add is assigned the value of a new lisitviewitem with the varaible row being passed through it
                add.Tag = product; // a reference to list is stored inside the varaible add
                listViewBasket.Items.Add(add); // The varaible add is then added to the listview as new items
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string A = null; // A new string varaible A is created
            int quantity = 0;
            int price = 0;
            int index = 0;
            bool checkexist = false;
            try
            {
                if (textBox2.Text == "" || comboBox2.SelectedIndex == 9 && textBox3.Text == "") // This checks if the textboxes are empty
                {
                    MessageBox.Show("Please choose the product you want to purchase or enter the quantity", "ALERT !!!!!"); // A messagebox is sued to alert the user
                }
                else
                {                    
                    string[] quanti = this.listViewBasket.Items.Cast<ListViewItem>().Select(items => items.SubItems[1].Text).ToArray();
                    string[] Prices = this.listViewBasket.Items.Cast<ListViewItem>().Select(items => items.SubItems[2].Text).ToArray();
                    string[] ProductsName = this.listViewBasket.Items.Cast<ListViewItem>().Select(items => items.SubItems[0].Text).ToArray(); // A new string array called ProductsName is created which saves all of the items in the second column of the listview converted into an array
                    for (int i = 0; i < ProductsName.Length; i++)
                    {
                        if (ProductsName[i] == comboBox1.SelectedItem.ToString())
                        {
                            checkexist = true;
                            index = i;
                            quantity = Convert.ToInt32(quanti[i]);
                            price = Convert.ToInt32(Prices[i]);
                        }
                    }
                    if (comboBox2.SelectedIndex < 9) // This checks if the selected index in combobox2 is less than 9 
                    {
                        quantity = quantity + comboBox2.SelectedIndex + 1; // A new integer variable called quantity saves the selected index of combox2 plus 1 
                        stockcompare(quantity,price,index,checkexist);
                    }
                    else
                    {
                        if (Convert.ToInt32(textBox3.Text) > 0)
                        {
                            quantity = quantity + int.Parse(textBox3.Text);  // A new integer variable called quantity saves the value in textbox3 converted to an integer
                            stockcompare(quantity, price, index, checkexist);
                        }
                        else
                        {
                            MessageBox.Show(" Quantity entered is out of parameters ","ERROR!!!");
                        }
                    }
                    comboBox2.SelectedIndex = 0; // The index selected in combobox is set to 0
                    comboBox2.Text = " --- SELECT --- "; // Combobox2 is set to display a text
                    textBox3.Text = "";
                }
            }
            catch (Exception ex)
            {
                A = "Exception in DBHandler" + ex; // A is assigned a value plus the error that has occured in the set of code that it had tryed 
                MessageBox.Show(A, "ERROR !!!!"); // The message box then displays the variable A
            }
        }
        void stockcompare(int quantity, int price, int index, bool checkexist)
        {
            int stock = int.Parse(textBox1.Text); // A new integer variable called stock saves the value in textbox1 converted to an integer 
            if (stock > quantity) // This checks if the variable stock is greater than or equal to quantity 
            {
                if (checkexist == true)
                {
                    listViewBasket.Items.RemoveAt(index);
                }
                addlisttolistview(price, quantity); // addlisttolistview function is called 
            }
            else
            {
                MessageBox.Show("Quantity in basket is greater than or equal to stock available", "Error!!!"); // A messagebox is shown if the if statement is incorrect
            }
        }
        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
            if (comboBox2.SelectedIndex == 9) // If the selected index in combobox1 is = to 9 then
            {
                textBox3.Visible = true; // textbox3 visibilty is set to true
                label8.Visible = true; // label8 visibilty is set to true
            }
            else
            {
                textBox3.Visible = false; // textbox3 visibilty is set to false
                label8.Visible = false; // label8 visibilty is set to false
            }           
            label10.Text = DateTime.Now.ToLongDateString(); // Label is assigned the date of when there is any change in panel2
            label11.Text = DateTime.Now.ToLongTimeString(); // Label is assigned the time of when there is any change in panel2
        }       
        private void button2_Click(object sender, EventArgs e)
        {
            Page5 earning = new Page5(); // The varaible earning is assigned a new link to page5
            earning.Show(); // I am calling the variable earning and asking it to show the data assigned to it
        }        
        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {}
        private void button3_Click(object sender, EventArgs e)
        {
            int length = listViewBasket.Items.Count; // A new integer varaible called length is created which is assigned the value of the number of rows present inside the listview
            double VAttax = Convert.ToInt32(textBox6.Text) * 0.2;
            double total = Convert.ToInt32(textBox6.Text) + VAttax;
            string join = null;  // A new string varaible called quantity is created and is empty            
            string[] Quantity = this.listViewBasket.Items.Cast<ListViewItem>().Select(items => items.SubItems[1].Text).ToArray(); // A new string array called Quantity is created which saves all of the items in the second column of the listview converted into an array
            string[] ProductsName = this.listViewBasket.Items.Cast<ListViewItem>().Select(items => items.SubItems[0].Text).ToArray(); // A new string array called ProductsName is created which saves all of the items in the second column of the listview converted into an array
            string[] Prices = this.listViewBasket.Items.Cast<ListViewItem>().Select(items => items.SubItems[2].Text).ToArray(); // A new string array called Prices is created which saves all of the items in the second column of the listview converted into an array
            for (int i = 0; i < length; i++) // A for loop is started until it reaches the lneght of the list view
            {
                join += "Item:  " + ProductsName[i] + "\n" + "Price:  £" + Prices[i] + "\n" + "Quantity:  " + Quantity[i] + "\n" + "\n";  // All of the varaibles are concatinated into the vaaraible join
            }

            // The messageboxes result is saved into the variable Confirm, it uses the function dialogresult which returns the response value of the messagebox
            DialogResult Confirm = MessageBox.Show("                     ---- THANK YOU FOR ORDERING AT THE STORE ----"
                + "\n" + "\n" + "-----------------------------------------------------------------------------------------"
                + "\n" + join + "                                    YOUR ORDER TOTAL IS : £" + textBox6.Text
                + "\n" + "                                    VAT TAX APPLIED IS : £" + VAttax + "\n" 
                + "                                    YOUR GROSS TOTAL IS : £" + total
                + "\n" + "\n" + "-----------------------------------------------------------------------------------------"
                + "\n" + label10.Text + "                                                                    " + label11.Text, "Confirm Order", MessageBoxButtons.OKCancel); // This messagebox acts like a reciept which shows all of the items inside the basket and the total, and at the end it shows the time and date of when the button was clicked

            if (Confirm == DialogResult.OK) // This checks if the varaible confirm is equal to OK
            {
                agerestriction(); // The procedure agerestriction is called
            }
        }
        void Purchase() // A procedure called purchase is created
        {
            connectsource.Open(); // Connection is opened with the datasource 
            string itemsbought = listViewBasket.Items.Count.ToString(); // This saves the items present in the listview converted to a string in the a new string variable called itemsbought            
            string A = ""; // I have created a new string variable called A which is empty
            OleDbCommand SQLcommand = new OleDbCommand(); // This intializes a new oledbcommand and saves it into the variable SQLcommand  
            SQLcommand.Connection = connectsource; // Here the command is linked to the connection string to a specific data source
                                  
            try // The program tries this set of code first 
            {
                SQLcommand.CommandText = "INSERT INTO Sales (ItemsSold,Total,TDate,EmployeeID) VALUES('" + itemsbought + "', '" + textBox6.Text + "', '" + label10.Text + "', '" + employee + "')"; // The sql query inserts all of the values typed in the text boxes into the given fields inside the datasource and this query is then assigned to the variable SQLcommand
                SQLcommand.ExecuteNonQuery();
                SQLcommand.CommandText = "INSERT INTO Earning (Income,InAmount,TransactionDate) VALUES( 'SALES', '" + textBox6.Text + "', '" + label10.Text + "')"; // The sql query inserts sales into the earning page, it saves the amount and date with the name SALES 
                SQLcommand.ExecuteNonQuery();
                A = " --- THANK YOU FOR PURCHASING FROM OUR STORE ---"; // The variable A is set a string value                    
                stock_update(); // The procedure stock_update is called
                receiptrecord(); // The procedure receiptrecord is called
                MessageBox.Show(A, "Purchase Complete"); // The message box displays the variable 
            }
            catch (Exception ex) // If code doesnt work then program trys this set of code 
            {
                A = "Exception in DBHandler" + ex; // A is assigned a value plus the error that has occured in the set of code that it had tryed 
                MessageBox.Show(A, "ERROR !!!!"); // The message box then displays the variable A
            }
            finally
            {
                connectsource.Close(); // Connection is closed with the datasource
            }         
        }
        private void stock_update() // A procedure called stock_update is created 
        {            
            string A = ""; // I have created a new string variable called A which is empty
            OleDbCommand SQLcommand = new OleDbCommand(); // This intializes a new oledbcommand and saves it into the variable SQLcommand  
            SQLcommand.Connection = connectsource; // Here the command is linked to the connection string to a specific data source            
            string[] Quantity = this.listViewBasket.Items.Cast<ListViewItem>().Select(products => products.SubItems[1].Text).ToArray(); // A new string array called Quantity is created which saves all of the items in the second column of the listview converted into an array
            string[] ProductsName = this.listViewBasket.Items.Cast<ListViewItem>().Select(products => products.SubItems[0].Text).ToArray(); // A new string array called ProductsName is created which saves all of the items in the second column of the listview converted into an array
            int quantstock = 0; // Here new variable double called quantstock is created and set to 0
            int quantbought = 0; ; // Here new variable double called quantbought is created and set to 0
            int itemsbought = listViewBasket.Items.Count; // An integer called itemsbought is assigned the length of the listview
            for (int i = 0; i < itemsbought; i++) // A loop from the index 0 to the length of the string - 1 is started
            {
                SQLcommand.CommandText = "SELECT stock FROM Products WHERE Product_name = '" + ProductsName[i] + "'"; // An SQLcommand is used to select all of the stock assigned to the products that are being bought
                OleDbDataReader datareader = SQLcommand.ExecuteReader(); // All of the information that is read from the query that was saved in the variable SQLcommand will be saved into the variable datareader
                datareader.Read(); // The datareader is asked to read through the data source
                quantstock = Convert.ToInt16(datareader[0]); // quantstock is assigned the value is the first column of the table that it is reading through
                datareader.Close(); // The datareader is asked to stop reading and close
                quantbought = Convert.ToInt16(Quantity[i]); // quantitybought will be assigned the value of the converted value of the postion i in the array quantity 
                int newquant = quantstock - quantbought; // A new double variable called newquant is created which saves the answer of quantbought taken away from quantstock
                SQLcommand.CommandText = "Update Products SET stock = '" + newquant.ToString() + "' WHERE Product_name = '" + ProductsName[i] + "'"; // An sql command is used to update the products table and save the variable newquant to the value of the product in psotion i in the array productname 
                try // The program tries this set of code first 
                {
                    SQLcommand.ExecuteNonQuery(); // This executes the sql query saved in the variable SQLcommand
                }
                catch (Exception ex) // If code doesnt work then program trys this set of code 
                {
                    A = "Exception in DBHandler" + ex; // A is assigned a value plus the error that has occured in the set of code that it had tryed 
                    MessageBox.Show(A, "ERROR !!!!"); // The message box then displays the variable A
                }                
            }
        }        
        private void button4_Click(object sender, EventArgs e)
        {
            Page6 stock = new Page6(); // The varaible earning is assigned a new link to page5
            stock.Show(); // I am calling the variable earning and asking it to show the data assigned to it
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Page2 login = new Page2(); // The varaible login is assigned a new link to page2
            login.Show(); // I am calling the variable login and asking it to show the data assigned to it
            this.Hide(); // This is used to hide the current page
        }
        void updatetextboxes() // A new procedure called updatetextboxes is created
        {
            connectsource.Open(); // This opens the connection between the form and the data source
            string selected = this.comboBox1.GetItemText(this.comboBox1.SelectedItem); // A new string called selected saves the value being shown in the selected index of the combobox2
            string A = ""; // I have created a new string variable called A which is empty
            OleDbCommand SQLcommand = new OleDbCommand(); // This intializes a new oledbcommand and saves it into the variable SQLcommand  
            SQLcommand.Connection = connectsource; // Here the command is linked to the connection string to a specific data source
            SQLcommand.CommandText = "SELECT * FROM Products WHERE Product_name LIKE '%" + selected + "%'"; // A new SQL query is used to select all of the data from the product table where the product name is the name in the selected index of the combobox 
            OleDbDataReader reader = SQLcommand.ExecuteReader(); // All of the information that is read from the query that was saved in the variable SQLcommand will be saved into the variable reader            
            try // The program tries this set of code first 
            {
                reader.Read(); // The reader is asked to read through the data source
                textBox5.Text = Convert.ToString(reader.GetInt32(0)); // textbox5 is assigned the value that the reader gets from the first column of the table that it was reading converted to a string
                textBox4.Text = reader.GetString(1); // textbox4 is assigned the value that the reader gets from the second column of the table that it was reading
                textBox1.Text = reader.GetString(2); // textbox1 is assigned the value that the reader gets from the third column of the table that it was reading 
                textBox2.Text = Convert.ToString(reader.GetInt32(3)); // textbox2 is assigned the value that the reader gets from the forth column of the table that it was reading converted to a string
            }
            catch (Exception ex) // If code doesnt work then program trys this set of code 
            {
                A = "Exception in DBHandler" + ex; // A is assigned a value plus the error that has occured in the set of code that it had tryed 
                MessageBox.Show(A, "ERROR !!!!"); // The message box then displays the variable A
            }
            connectsource.Close(); // This closes the connection string between the form and the data source
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            updatetextboxes(); // The procedure updatetextboxes is called when the selected index of the combobox changes
            comboBox2.SelectedIndex = 0; // The index selected in combobox is set to 0
            comboBox2.Text = " --- SELECT --- "; // Combobox2 is set to display a text
        }
        void agerestriction() // A new procedure called agedrestriction is created
        {   
            string[] ProductsName = this.listViewBasket.Items.Cast<ListViewItem>().Select(items => items.SubItems[0].Text).ToArray(); // A new string array called ProductsName is created which saves all of the items in the second column of the listview converted into an array
            bool agerestricted = false; // I have created a new bool varaible agerestricted and assigned it to be false
            for (int i = 0; i<ProductsName.Length; i++) // A for loop is started and it terminates when it reaches the length of the ProductsName list
            {
                if (ProductsName[i].Contains("Wine") || ProductsName[i].Contains("Liqueur")) // This checks if the item in the ProductsName list contains any of the following text
                {
                    agerestricted = true; // The bool variable agerestricted is now asigned to be true
                }                
            }
            if (agerestricted == true) // This checks if agerestricted is true
            {
                DialogResult age = MessageBox.Show("ITEM IS AGE RESTRICTED" + "\n" + "\n" + "Check if the customer is over 18 !!!!" + "\n" + "\n" + "If customer is over age then press OK only !!!", "Alert!!!!", MessageBoxButtons.OKCancel); // The dialog of the message box is stored in the variable age, the message box buttons are changed to ok and cancel
                if (age == DialogResult.OK) // This checks if the result of age is equal to true
                {
                    Purchase(); // The procdeure purchase is called                    
                }
            }
            else
            {
                Purchase(); // The procdeure purchase is called
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                int Amount = 0;
                string amount = listViewBasket.SelectedItems[0].SubItems[2].ToString();
                if (amount.Length == 21)
                {
                    Amount = Convert.ToInt32(amount[18] + amount[19].ToString());
                }
                else if (amount.Length == 20)
                {
                    Amount = Convert.ToInt32(amount[18]);
                }
                else
                {
                    Amount = Convert.ToInt32(amount[18].ToString() + amount[19].ToString() + amount[20].ToString());
                }
                total = Convert.ToInt32(textBox6.Text) - Amount;
                //MessageBox.Show(total.ToString());
                textBox6.Text = total.ToString();
                listViewBasket.SelectedItems[0].Remove();
                // this would calculate the new amount of the total
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error!");
                // Error would be displayed to user
            }
        }
        void receiptrecord()
        {
            int length = listViewBasket.Items.Count; // A new integer varaible called length is created which is assigned the value of the number of rows present inside the listview           
            string join = null;  // A new string varaible called quantity is created and is empty            
            string[] Quantity = this.listViewBasket.Items.Cast<ListViewItem>().Select(items => items.SubItems[1].Text).ToArray(); // A new string array called Quantity is created which saves all of the items in the second column of the listview converted into an array
            string[] ProductsName = this.listViewBasket.Items.Cast<ListViewItem>().Select(items => items.SubItems[0].Text).ToArray(); // A new string array called ProductsName is created which saves all of the items in the second column of the listview converted into an array
            string[] Prices = this.listViewBasket.Items.Cast<ListViewItem>().Select(items => items.SubItems[2].Text).ToArray(); // A new string array called Prices is created which saves all of the items in the second column of the listview converted into an array 
            for (int i = 0; i < length; i++) // A for loop is started until it reaches the lneght of the list view
            {
                join += "Item:  " + ProductsName[i] + "\n" + "Price:  £" + Prices[i] + "\n" + "Quantity:  " + Quantity[i] + "\n" + "\n";  // All of the varaibles are concatinated into the vaaraible join
            }

            OleDbCommand SQLcommand = new OleDbCommand(); // This intializes a new oledbcommand and saves it into the variable SQLcommand
            SQLcommand.Connection = connectsource; // Here the command is linked to the connection string to a specific data source 
            SQLcommand.CommandText = "select * from Sales "; // The SQL command selects everything from the sales table
            OleDbDataReader reader = SQLcommand.ExecuteReader(); // All of the information that is read from the query that was saved in the variable SQLcommand will be saved into the variable reader
            DataTable Sale = new DataTable(); // A new data table called Sale is created
            Sale.Load(reader); // The data table sale is loaded with the values returned by reader
            int rows = Sale.Rows.Count; // A new integer varaible called rows is created which saves the number of rows inside the data table
            string filename = "SaleID" + rows.ToString(); // A new string varaible called Filename is created and is assigned a text

            string location = @"C:\Users\44730\OneDrive\Documents\Computer Science Project\" + filename + ""; // A new string called location is created and is assigned the location of the file
            if (!File.Exists(location)) // This checks if the file in the location string exists
            {
                using (StreamWriter write = File.CreateText(location)) // This is used to create and write to a file using the path in the varaible location
                {
                    write.WriteLine("This order contained the following items" + "\n"); // A text is written to the file
                    write.WriteLine(join);  // A text is written to the file
                    write.WriteLine("Total was: £" + textBox6.Text + "\n");  // A text is written to the file
                    write.WriteLine("The date of this order and the time was" + "\n" + label10.Text + "                " + label11.Text);  // A text is written to the file
                }
            }
            else
            {
                MessageBox.Show("A file with this name exists", "ERROR !!!!"); // A message box is shown to alert the user
            }
        }
    }
}
