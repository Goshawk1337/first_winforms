using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using static System.Net.Mime.MediaTypeNames;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection;
using System.Xml.Linq;
namespace add_and_read_cars
{
    public partial class Form1 : Form
    {
        Dictionary<string, List<string>> brandModels = new Dictionary<string, List<string>>()
        {
            { "Audi", new List<string> { "A4", "A6", "A8" } },
            { "BMW", new List<string> { "X3", "X5", "X6" } },
            { "Mercedes", new List<string> { "C-Class", "E-Class", "S-Class" } },
            { "Volkswagen", new List<string> { "C-Class", "E-Class", "Passat B5 1.9TDI" } },

        };

        public Form1()
        {
            InitializeComponent();
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            string name = comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : "";
            string model = comboBox3.SelectedItem != null ? comboBox3.SelectedItem.ToString() : "";
            string colour = comboBox2.SelectedItem != null ? comboBox2.SelectedItem.ToString() : "";
            string plateo = plate.Text;
            Console.WriteLine(name, model);

            if (name.Length > 0 && model.Length > 0 && colour.Length > 0 && plateo.Length >= 7)
            {
                runQuery(name, model, colour, plateo);
                listBox1.Items.Add(string.Format("{0,-40} {1,-27} {2,-35} {3, -45}", name, model, colour, plateo));
                lister.Items.Add(plateo);
            }
            else if (name.Length == 0)
            {
                MessageBox.Show("Nem valasztottal markat!");
            }
            else if (model.Length == 0)
            {
                MessageBox.Show("Nem valasztottal modelt!");
            }
            else if (colour.Length == 0)
            {
                MessageBox.Show("Nem valasztottal szint!");
            }
            else if (plate.Text.Length < 7)
            {
                MessageBox.Show("Helytelen rendszam!");
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedBrand = comboBox1.SelectedItem.ToString();

            // Töröljük a ComboBox3 tartalmát
            comboBox3.Items.Clear();

            // Ellenőrizzük, hogy a kiválasztott márka benne van-e a szótárban
            if (brandModels.ContainsKey(selectedBrand))
            {
                // Modellek hozzáadása a ComboBox3-hoz
                comboBox3.Items.AddRange(brandModels[selectedBrand].ToArray());
            }
        }
        private void selectedCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void runQuery(string name, string model, string colour, string plate)
        {
            string mysqlConnect = "datasource=127.0.0.1;port=3306;username=root;password=;database=stored_cars";
            string querry = "INSERT INTO cars(`brand`, `model`, `colour`, `plate`) VALUES ('" + name + "', '" + model + "', '" + colour  +"', '" + plate +"' )";
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlConnect);
            MySqlCommand commandDatabase = new MySqlCommand(querry, mySqlConnection);
            commandDatabase.CommandTimeout = 60;

            try
            {
                mySqlConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();


                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        Console.WriteLine(myReader.GetString(0) + " - " + myReader.GetString(1) + " - " + myReader.GetString(2) + " - " + myReader.GetString(3));
                    }
                }
                else
                {
                    MessageBox.Show("Sikeresen hozzaadtad az autodat. Marka: " + name + ", modell: " + model);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MySql.Data.MySqlClient.MySqlConnection dbConn = new MySql.Data.MySqlClient.MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=stored_cars");

            MySqlCommand cmd = dbConn.CreateCommand();
            cmd.CommandText = "SELECT * from cars";

            try
            {
                dbConn.Open();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro" + erro);
                this.Close();
            }

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string brand = reader["brand"].ToString();
                string model = reader["model"].ToString();
                string colour = reader["colour"].ToString();
                string plate = reader["plate"].ToString();
                listBox1.Items.Add(string.Format("{0,-45} {1,-55} {2,-55} {3, -45}", brand, model, colour, plate));
                lister.Items.Add(plate);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
        MySql.Data.MySqlClient.MySqlConnection dbConn = new MySql.Data.MySqlClient.MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=stored_cars");

            string query = "DELETE FROM cars WHERE plate = '" + lister.SelectedItem.ToString() + "'";
            MySqlCommand cmd = dbConn.CreateCommand();
            cmd.CommandText = query;


            try
            {
                dbConn.Open();
                int rowsAffected = cmd.ExecuteNonQuery(); 

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Sikeresen törölted!");
                    lister.Items.Remove(lister.SelectedItem.ToString());
                }
                else
                {

                    MessageBox.Show("Nem található ilyen rendszám.");
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("Hiba: " + erro.Message);
            }
            finally
            {
                dbConn.Close(); 
            }

        }

    }
}
