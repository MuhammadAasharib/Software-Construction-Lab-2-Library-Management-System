//Done By Aasharib
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

namespace Library_Management_System
{
    public partial class Form1 : Form
    {
        String sqlConName = "datasource=localhost;port=3306;username=root;password=";
        //String queryToExecute = "insert into library_system.studentinfo(idStudentInfo,Name,Father_Name,Age,Semester) values('" + this.IdTextBox.Text + "','" + this.NameTextBox.Text + "','" + this.FnameTextBox.Text + "','" + this.AgeTextBox.Text + "','" + this.SemesterTextBox.Text + "');";

        public Form1()
        {
            InitializeComponent();
            initInputBox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String publishDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            String status = comboBox1.Text.ToString();
            String batchNo = textBox3.Text.ToString();
            String price = textBox4.Text.ToString();
            String mode = comboBox2.Text.ToString();
            String title = comboBox5.Text.ToString();
            String author = textBox7.Text.ToString();
            String artifact = comboBox3.Text.ToString();
            String quantity = textBox9.Text.ToString();
            String genre = comboBox4.Text.ToString();
            
            int quantityB = Convert.ToInt32(quantity);
            int titleCheck = checkForeignKeyValid("library_system", "name", "id", "title",title);
            int artifactCheck = checkForeignKeyValid("library_system", "name", "id", "artifact",artifact);
            int genreCheck = checkForeignKeyValid("library_system", "name", "id", "genre", genre);
            int authorCheck = checkForeignKeyValid("library_system", "name", "id", "author", author);

            //if title is not available in title table therefore foreign key cannot be referenced
            if(titleCheck == 0) {
                MySqlConnection con1 = connectToDb();
                String query1 = "insert into " + "library_system" + "." + "title (" + "name" + ")" + "values ('"
                            + title + "');";
                MySqlCommand command1 = new MySqlCommand(query1, con1);
                try
                {
                    MySqlDataReader sqd = command1.ExecuteReader();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                closeDbConnection(con1);
                titleCheck = checkForeignKeyValid("library_system", "name", "id", "title", title);
            }

            //if artifact is not available in artifact table therefore foreign key cannot be referenced
            if (artifactCheck == 0) {
                MySqlConnection con2 = connectToDb();
                String query2 = "insert into " + "library_system" + "." + "artifact (" + "name" + ")" + "values ('"
                            + artifact + "');";
                MySqlCommand command2 = new MySqlCommand(query2, con2);
                try
                {
                    MySqlDataReader sqd = command2.ExecuteReader();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                closeDbConnection(con2);
                artifactCheck = checkForeignKeyValid("library_system", "name", "id", "artifact", artifact);
            }

            //if genre is not available in genre table therefore foreign key cannot be referenced
            if (genreCheck == 0) {
                MySqlConnection con3 = connectToDb();
                String query3 = "insert into " + "library_system" + "." + "genre (" + "name" + ")" + "values ('"
                            + genre + "');";
                MySqlCommand command3 = new MySqlCommand(query3, con3);
                try
                {
                    MySqlDataReader sqd = command3.ExecuteReader();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                closeDbConnection(con3);
                genreCheck = checkForeignKeyValid("library_system", "name", "id", "genre", genre);
            }

            //if author is not available in author table therefore foreign key cannot be referenced
            if (authorCheck == 0) {
                MySqlConnection con4 = connectToDb();
                String query4 = "insert into " + "library_system" + "." + "author (" + "name" + ")" + "values ('"
                            + author + "');";
                MySqlCommand command4 = new MySqlCommand(query4, con4);
                try
                {
                    MySqlDataReader sqd = command4.ExecuteReader();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                closeDbConnection(con4);
                authorCheck = checkForeignKeyValid("library_system", "name", "id", "Author", author);
            }

            //Now performing the insertion operations for addition
            MySqlConnection con = connectToDb();
            MySqlDataReader sqd1;
            for (int i = 0; i < quantityB; i++)
            {
                String query = "insert into " + "library_system" + "." + "book (" + "publish_date" + "," + "status" +
                                "," + "batch_number" + "," + "price" + "," + "mode" + "," + "title_id" + "," + "author_id" +
                                "," + "genre_id" + "," + "artifact_id )" + "values ('"
                                + publishDate + "','" + status + "','" + batchNo + "','" + price + "','" + mode + "','" + titleCheck + "','"
                                + authorCheck + "','" + genreCheck + "','" + artifactCheck + "');";
                MySqlCommand command = new MySqlCommand(query, con);
                try
                {
                   sqd1 = command.ExecuteReader();
                   sqd1.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            closeDbConnection(con);
            MessageBox.Show("Data Saved Succesfully!");
        }

        private void label3_Click(object sender, EventArgs e){}

        private void initInputBox()
        {
            loadCombo(comboBox3, "library_system", "artifact", "name", "id");
            loadCombo(comboBox4, "library_system", "genre", "name", "id");
            loadCombo(comboBox5, "library_system", "title", "name", "id");
        }

        private int checkForeignKeyValid(String dbName, String keyColumn,String valColumn, String tableName, String toFind) {
            MySqlConnection con = connectToDb();
            String query = "select " + keyColumn + "," + valColumn + " from " + dbName + "." + tableName + ";";
            MySqlCommand command = new MySqlCommand(query, con);
            int found = 0;
            try
            {
                MySqlDataReader sqd = command.ExecuteReader();
                while (sqd.Read())
                {
                    int gues = Convert.ToInt32(sqd[valColumn].ToString());
                    if (sqd[keyColumn].ToString() == toFind) {
                        found = gues;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                closeDbConnection(con);
                return -1;
            }
            closeDbConnection(con);
            return found;
        }

        // give combobox name , database name, db table name, name of attribute to show and value to bind to attribute
        private void loadCombo(ComboBox comboName, String dbName, String tableName, String keyColumn, String valColumn) {
            MySqlConnection con = connectToDb();
            String query = "select " + keyColumn + "," + valColumn + " from " + dbName + "." + tableName + ";";
            MySqlCommand command = new MySqlCommand(query, con);
            try
            {
                MySqlDataReader sqd = command.ExecuteReader();
                Dictionary<String, String> dtSource = new Dictionary<String, String>();
                while (sqd.Read())
                {
                    dtSource.Add(sqd[keyColumn].ToString(), sqd[valColumn].ToString());
                }
                comboName.DataSource = new BindingSource(dtSource, null);
                comboName.DisplayMember = "Key";
                comboName.ValueMember = "Value";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            closeDbConnection(con);
        }

        private MySqlConnection connectToDb() {
            try
            {
                MySqlConnection con = new MySqlConnection(sqlConName);
                con.Open();
                if (con.State == ConnectionState.Open) {
                    
                }
                return con;
            }

            catch (Exception ex){
                MessageBox.Show(ex.Message);               
                return null;
            }
        }

        private void closeDbConnection(MySqlConnection con){
            con.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
