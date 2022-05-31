using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Gate_In
{
    public partial class getting_car_details : Form
    {
        public getting_car_details()
        {
            InitializeComponent();
        }

        private MySqlConnection connect_to_db()
        {
            MySqlConnection db = new MySqlConnection();
            db.ConnectionString = @"host=10.1.70.58; port=3306; uid=root; database=manheim_database; pwd=root;";
            return db;
        }
        private bool validation()
        {
            try
            {
                if ((Convert.ToDouble(fuel_capacity_txt.Text)) != 0.25 && (Convert.ToDouble(fuel_capacity_txt.Text)) != 0.5 &&
                (Convert.ToDouble(fuel_capacity_txt.Text)) != 0.75 && (Convert.ToDouble(fuel_capacity_txt.Text)) != 1)
                {
                    MessageBox.Show("Capacity can either be 0.25, 0.5, 0.75 or 1!");
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("Fuel capacity must be a number!");
                return false;
            }
            try
            {
                Convert.ToInt32(odometer_txt.Text);
            }
            catch
            {
                MessageBox.Show("The odometer reading must be a number!");
                return false;
            }
            return true;
        }

        private void add_vehicle()
        {
            MySqlConnection db = connect_to_db();
            string command = "INSERT INTO gatein(vrm, odometer, fuelcapacity, vendor, barcode) VALUES(@VRM, @Odometer, @FTCapacity, @Vendor, @Barcode)";
            using (db)
            {
                MySqlCommand add_data = new MySqlCommand(command, db);

                add_data.Parameters.Add("@VRM", MySqlDbType.VarChar); // ideally you should validate and check the data you insert into your DB.
                add_data.Parameters["@VRM"].Value = vehicle_reg_txt.Text; // adding variables by parameters is a safer way to handle these things.
                add_data.Parameters.Add("@Odometer", MySqlDbType.Int32);
                add_data.Parameters["@Odometer"].Value = Convert.ToInt32(odometer_txt.Text);
                add_data.Parameters.Add("@FTCapacity", MySqlDbType.VarChar);
                add_data.Parameters["@FTCapacity"].Value = fuel_capacity_txt.Text;
                add_data.Parameters.Add("@Vendor", MySqlDbType.VarChar);
                add_data.Parameters["@Vendor"].Value = vendor_txt.Text;
                add_data.Parameters.Add("@Barcode", MySqlDbType.VarChar);
                add_data.Parameters["@Barcode"].Value = barcode_txt.Text;

                try // tries to run the query, any exceptions or issues will be caught.
                {
                    db.Open();
                    add_data.ExecuteReader();
                    MessageBox.Show("Complete!");
                    db.Close();
                    // Close connection when done... keep things neat and tidy.
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(Convert.ToString(ex.Number));
                }
                finally
                {
                    if (db == null)
                    {
                        MessageBox.Show("Connection Failed!");
                    }
                }
            }
        }

        private void submit_btn_Click(object sender, EventArgs e)
        {
            bool valid = validation();
            if (valid) add_vehicle();
        }

        private void scan_barcode_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form form = new barcode_scanner();
            form.Show();
        }
    }
}
