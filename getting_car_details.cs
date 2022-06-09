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
using System.Text.RegularExpressions;

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
            db.ConnectionString = @"host=192.168.137.5; port=3306; uid=root; database=manheim_database; pwd=root;";
            return db;
        }

        private Size oldSize;
        private void Form1_Load(object sender, EventArgs e) => oldSize = base.Size;

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);

            foreach (Control cnt in this.Controls)
                ResizeAll(cnt, base.Size);

            oldSize = base.Size;
        }
        private void ResizeAll(Control control, Size newSize)
        {
            int width = newSize.Width - oldSize.Width;
            control.Left += (control.Left * width) / oldSize.Width;
            control.Width += (control.Width * width) / oldSize.Width;

            int height = newSize.Height - oldSize.Height;
            control.Top += (control.Top * height) / oldSize.Height;
            control.Height += (control.Height * height) / oldSize.Height;
        }
        private bool validation()
        {
            Regex is_decimal = new Regex("[0-9]+(/.[0-9][0-9]?)?");
            Regex is_number = new Regex("^[0-9]+$");

            if (is_decimal.IsMatch(fuel_capacity_txt.Text))
            {
                if ((Convert.ToDouble(fuel_capacity_txt.Text)) != 0.25 && (Convert.ToDouble(fuel_capacity_txt.Text)) != 0.5 &&
                    (Convert.ToDouble(fuel_capacity_txt.Text)) != 0.75 && (Convert.ToDouble(fuel_capacity_txt.Text)) != 1)
                {
                    MessageBox.Show("Capacity can either be 0.25, 0.5, 0.75 or 1!");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("The fuel reading must be a number!");
                return false;
            }
            if (!is_number.IsMatch(odometer_txt.Text))
            {
                MessageBox.Show("The odometer reading must be a number!");
                return false;
            }
            if (private_reg_radbtn.Checked == false && prefix_reg_radbtn.Checked == false)
            {
                MessageBox.Show("You must tick whether the car has a private registration or not!");
                return false;
            }
            if (full_service_radbtn.Checked == false && part_service_radbtn.Checked == false && no_service_radbtn.Checked == false)
            {
                MessageBox.Show("You must select the service history of the car!");
                return false;
            }
            return true;    
        }

        private void add_vehicle()
        {
            MySqlConnection db = connect_to_db();
            string command = "INSERT INTO gatein(vrm, odometer, fuelcapacity, vendor, barcode, isprivate, servicehistory, mot, v5) " +
                "VALUES(@VRM, @Odometer, @FTCapacity, @Vendor, @Barcode, @IsPrivate, @servicehistory, @mot, @v5)";

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
                
                if (private_reg_radbtn.Checked == true)
                {
                    MessageBox.Show("You must request the vehicles V5 and remove the plates from the vehicle before submitting!");
                    add_data.Parameters.Add("@IsPrivate", MySqlDbType.VarChar);
                    add_data.Parameters["@IsPrivate"].Value = 1;
                }
                else
                {
                    add_data.Parameters.Add("@IsPrivate", MySqlDbType.VarChar);
                    add_data.Parameters["@IsPrivate"].Value = 0;
                }
                if (valid_mot_chk.Checked == true)
                {
                    add_data.Parameters.Add("@mot", MySqlDbType.VarChar);
                    add_data.Parameters["@mot"].Value = 1;
                }
                else
                {
                    add_data.Parameters.Add("@mot", MySqlDbType.VarChar);
                    add_data.Parameters["@mot"].Value = 0;
                }
                if (has_v5_chk.Checked == true)
                {
                    add_data.Parameters.Add("@v5", MySqlDbType.VarChar);
                    add_data.Parameters["@v5"].Value = 1;
                }
                else
                {
                    add_data.Parameters.Add("@v5", MySqlDbType.VarChar);
                    add_data.Parameters["@v5"].Value = 0;
                }
                if (full_service_radbtn.Checked == true)
                {
                    add_data.Parameters.Add("@servicehistory", MySqlDbType.VarChar);
                    add_data.Parameters["@servicehistory"].Value = "Full Service";
                }
                else if (part_service_radbtn.Checked == true)
                {
                    add_data.Parameters.Add("@servicehistory", MySqlDbType.VarChar);
                    add_data.Parameters["@servicehistory"].Value = "Part Service";
                }
                else
                {
                    add_data.Parameters.Add("@servicehistory", MySqlDbType.VarChar);
                    add_data.Parameters["@servicehistory"].Value = "No Service";
                }

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
                    if (ex.Number == 1130)
                    {
                        MessageBox.Show("Error!");
                    }
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
            barcode_scanner barcodescannerpage = new barcode_scanner();
            this.Hide();
            barcodescannerpage.Show();
        }

        private void private_reg_radbtn_CheckedChanged(object sender, EventArgs e)
        {
            if (private_reg_radbtn.Checked == true)
            {
                MessageBox.Show("Request the vehicles V5 and remove the plates from the vehicle!");
            }
        }

        private void getting_car_details_Load(object sender, EventArgs e)
        {

        }

        private void submit_btn_decor_pnl_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
