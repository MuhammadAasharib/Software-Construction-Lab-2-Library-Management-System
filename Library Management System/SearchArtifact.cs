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
    public partial class SearchArtifact : Form
    {

        String sqlConName = "datasource=localhost;port=3306;username=root;password=";
        public SearchArtifact()
        {
            InitializeComponent();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String authorName = textBox8.Text;
            MySqlConnection con = connectToDb();

            /*String tbName = "author";
            String dbName = "library_system";
            String valColumn = "id";*/
            //String keyColumn = "name";
            //String toFind = "";
            //select title.name, author.name from book join author join title on book.title_id = title.id and book.author_id = author.id
            //String query = "select " + "*" + " from " + dbName + "." + tbName +" " +"where " + "name= " + "\'"+ authorName+"\'" + ";";

            String query = "select library_system.book.id,library_system.title.name as title, library_system.author.name as author,library_system.book.publish_date, library_system.book.batch_number, library_system.book.price,library_system.book.mode as mode, library_system.genre.name as genre, library_system.artifact.name as artifact from library_system.book join library_system.author join library_system.title join library_system.genre join library_system.artifact on book.title_id = title.id and book.author_id = author.id and book.genre_id = genre.id and book.artifact_id = artifact.id where author.name = " + "\'" + authorName + "\'" + ";";

           // MySqlCommand command = new MySqlCommand(query, con);
            try
            {
              //  MySqlDataReader sqd = command.ExecuteReader();
              //  sqd.Read();
              //  int gotId = Convert.ToInt32(sqd[valColumn].ToString());
              //  sqd.Close();
              //  String query2 = "select " + "*" + " from " + dbName + "." + "book" + " " + "where " + "author_id = " + gotId;

                MySqlDataAdapter command2 = new MySqlDataAdapter(query, con);
                DataSet gatherData = new DataSet();
                command2.Fill(gatherData);
                dataGridView1.DataSource = gatherData.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                closeDbConnection(con);
            }
            closeDbConnection(con);
        }

        private MySqlConnection connectToDb()
        {
            try
            {
                MySqlConnection con = new MySqlConnection(sqlConName);
                con.Open();
                if (con.State == ConnectionState.Open)
                {
                    
                }
                return con;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private void closeDbConnection(MySqlConnection con)
        {
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String titleName = textBox1.Text;
            MySqlConnection con = connectToDb();

            //String tbName = "title";
            //String dbName = "library_system";
            //String valColumn = "id";

            //String keyColumn = "name";
            //String toFind = "";
            //String query = "select " + "*" + " from " + dbName + "." + tbName + " " + "where " + "name= " + "\'" + titleName + "\'" + ";";

            //MySqlCommand command = new MySqlCommand(query, con);
            try
            {
                //MySqlDataReader sqd = command.ExecuteReader();
                //sqd.Read();
                //int gotId = Convert.ToInt32(sqd[valColumn].ToString());
                //sqd.Close();
                String query2 = "select library_system.book.id,library_system.title.name as title, library_system.author.name as author,library_system.book.publish_date, library_system.book.batch_number, library_system.book.price,library_system.book.mode as mode, library_system.genre.name as genre, library_system.artifact.name as artifact from library_system.book join library_system.author join library_system.title join library_system.genre join library_system.artifact on book.title_id = title.id and book.author_id = author.id and book.genre_id = genre.id and book.artifact_id = artifact.id where title.name = " + "\'" + titleName + "\'" + ";";
                MySqlDataAdapter command2 = new MySqlDataAdapter(query2, con);
                DataSet gatherData = new DataSet();
                command2.Fill(gatherData);
                dataGridView1.DataSource = gatherData.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                closeDbConnection(con);
            }
            closeDbConnection(con);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String genreName = textBox2.Text;
            MySqlConnection con = connectToDb();

            //String tbName = "genre";
            //String dbName = "library_system";
            //String valColumn = "id";

            //String keyColumn = "name";
            //String toFind = "";
            //String query = "select " + "*" + " from " + dbName + "." + tbName + " " + "where " + "name= " + "\'" + genreName + "\'" + ";";

            //MySqlCommand command = new MySqlCommand(query, con);
            try
            {
              //  MySqlDataReader sqd = command.ExecuteReader();
              //  sqd.Read();
              //  int gotId = Convert.ToInt32(sqd[valColumn].ToString());
              //  sqd.Close();
                String query2 = "select library_system.book.id,library_system.title.name as title, library_system.author.name as author,library_system.book.publish_date, library_system.book.batch_number, library_system.book.price,library_system.book.mode as mode, library_system.genre.name as genre, library_system.artifact.name as artifact from library_system.book join library_system.author join library_system.title join library_system.genre join library_system.artifact on book.title_id = title.id and book.author_id = author.id and book.genre_id = genre.id and book.artifact_id = artifact.id where genre.name = " + "\'" + genreName + "\'" + ";";
                MySqlDataAdapter command2 = new MySqlDataAdapter(query2, con);
                DataSet gatherData = new DataSet();
                command2.Fill(gatherData);
                dataGridView1.DataSource = gatherData.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                closeDbConnection(con);
            }
            closeDbConnection(con);
        }
    }
}
