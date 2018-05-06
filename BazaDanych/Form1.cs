using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BazaDanych
{
    public partial class Form1 : Form
    {
         
        SqlCommand sqlCommand;
        static string connectionString = "Server=KAROL;Database=TEST_CS;Trusted_Connection=true";
        SqlConnection connection = new SqlConnection(connectionString);

        private void openCloseDBconnection(string value)
        {
            using (sqlCommand = new SqlCommand(value, connection))
            {
                connection.Open();
                sqlCommand.ExecuteNonQuery();
                connection.Close();
            }

        }

        public Form1()
        {
            InitializeComponent();


        }

        private void buttonDopiszDoBazy_Click(object sender, EventArgs e)
        {

            try
            {
                if (!(String.IsNullOrWhiteSpace(textBoxNewUser.Text) && String.IsNullOrWhiteSpace(inputPassword.Text)))
                {


                    // wspisywanie imienia i hasla do bazy danych
                    string inputText = textBoxNewUser.Text;
                    string password = inputPassword.Text;

                    string newNameAndPassword = string.Format("INSERT INTO dbo.Users(Name,MyPassword) VALUES ('" + inputText + "','" + password + "')");
                    openCloseDBconnection(newNameAndPassword);
                    MessageBox.Show("Imie i hasło zapisane do bazy !");

                }

                else
                {
                    return;
                }

                
            }
             
            catch(InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }         
        }

            // usuwanie wszystkich danych z tabeli
        private void buttonDeleteTable_Click(object sender, EventArgs e)
        {
            string deleteFromDB = "DELETE FROM dbo.Users";
            openCloseDBconnection(deleteFromDB);
            string setMainKey = "DBCC CHECKIDENT('dbo.Users', RESEED, 0)";
            openCloseDBconnection(setMainKey);
            MessageBox.Show("Wszystkie dane z tabeli usunięte !");

        }

        private void buttonDeleteUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(textUserToDelete.Text))
                {
                    string inputUserToDelete = textUserToDelete.Text;
                    string userToDelete = string.Format("DELETE FROM dbo.Users WHERE Name = '"+ inputUserToDelete +"' ");
                    openCloseDBconnection(userToDelete);
                    MessageBox.Show("Użytkownik usunięty z bazy danych !");

                    string setMainKey = "DBCC CHECKIDENT('dbo.Users', RESEED, 0)";
                    openCloseDBconnection(setMainKey);

                }
                else
                {
                    MessageBox.Show("Pole uzytkownika do usuniecia nie moze byc puste!");
                    return;
                }

            }

            catch(InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

}
