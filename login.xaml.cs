using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Navigation;

namespace stanceup_new
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Window
    {
        MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
        MySqlCommand command;
        MySqlDataReader mdr;

        public login()
        {
            InitializeComponent();
        }

        public static string name;
        public static string dashname
        {
            get { return name; }
            set { name = value; }
        }

        private void btnclick_LogIn(object sender, RoutedEventArgs e)
        {
            conn.Open();
            string SelectQuery = "SELECT * FROM stanceup.user WHERE name = '" + txtName.Text + "' AND password = '" + txtPassword.Text + "';";
            command = new MySqlCommand(SelectQuery, conn);
            mdr = command.ExecuteReader();
            if (mdr.Read())
            {
                string MyConnection2 = "datasource=localhost;port=3306;username=root;password=";
                string Query = "update stanceup.user set lastlogin='" + DatePicker1.Text + "' where name= '" + this.txtName.Text + "';";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);

                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                while (MyReader2.Read())
                {
                }
                MyConn2.Close();

                dashname = txtName.Text;
                MessageBox.Show("Login Successful");
                this.Hide();
                MainWindow mw = new MainWindow();
                mw.Show();
            }
            else
            {
                MessageBox.Show("Incorrect login information. Please try again.");
            }
            conn.Close();

            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Please input name and password.", "Error");
            }

        }

        private void btnclick_SignUp(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Please input name and password", "Error");
            }
            else
            {
                conn.Open();
                string selectQuery = "SELECT * FROM stanceup.user WHERE name = '" + txtName.Text + "';";
                command = new MySqlCommand(selectQuery, conn);
                mdr = command.ExecuteReader();
                if (mdr.Read())
                {
                    MessageBox.Show("Name not available!");

                }

                else
                {

                    string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=stanceup;";
                    string iquery = "INSERT INTO user(`id`,`name`, `password`, `datecreated`,`lastlogin`) VALUES (NULL, '" + txtName.Text + "', '" + txtPassword.Text + "', '" + DatePicker1.Text + "', '" + DatePicker1.Text + "')";

                    MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                    MySqlCommand commandDatabase = new MySqlCommand(iquery, databaseConnection);
                    commandDatabase.CommandTimeout = 60;

                    try
                    {
                        databaseConnection.Open();
                        MySqlDataReader myReader = commandDatabase.ExecuteReader();
                        databaseConnection.Close();
                    }
                    catch (Exception ex)
                    {
                        // Show any error message.
                        MessageBox.Show(ex.Message);
                    }

                    MessageBox.Show("Account Successfully Created!");
                }

                conn.Close();
            }
        }

        private void btnclick_LIClose(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

