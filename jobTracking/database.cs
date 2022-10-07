using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace jobTracking
{


    class database
    {
        SQLiteConnection conn;
        SQLiteCommand cmd;
        SQLiteDataReader dr;


        public static void Create_db()
        {
            string path = "deneme.db";
            string cs = @"URI=file:"+Application.StartupPath+"\\deneme.db";

            if (!System.IO.File.Exists(path))
            {
                SQLiteConnection.CreateFile(path);
                using (var sqlite = new SQLiteConnection(@"Data Source="+ path))
                {
                    sqlite.Open();
                    string sql = "CREATE TABLE data (id INTEGER, data1 TEXT,  data2 TEXT, data3 TEXT, data4 TEXT, data5 TEXT, PRIMARY KEY(id AUTOINCREMENT))";
                    SQLiteCommand command = new SQLiteCommand(sql, sqlite);
                    command.ExecuteNonQuery();



                }

            }

        }

        public static void add(string data1, string data2 , string data3  )
        {
            try
            {
                string path = "deneme.db";
                string cs = @"URI=file:"+Application.StartupPath+"\\deneme.db";


               
                DateTime bugun = DateTime.Now;

                string data4 = DateTime.Now.Date.ToString("dd.MM.yyyy");
                string data5 = "0";
                var con = new SQLiteConnection(cs);
                con.Open();
                var cmd = new SQLiteCommand(con);




                //"server=localhost;username=root;password=;database=follow";
                //string sql2 = "CREATE TABLE passwords (id INTEGER, info TEXT , username TEXT, password TEXT,  PRIMARY KEY(id AUTOINCREMENT))";
                cmd.CommandText = "INSERT INTO data(data1,data2,data3,data4,data5) VALUES(@data1,@data2,@data3,@data4,@data5)";

                cmd.Parameters.AddWithValue("@data1", data1);
                cmd.Parameters.AddWithValue("@data2", data2);
                cmd.Parameters.AddWithValue("@data3", data3);
                cmd.Parameters.AddWithValue("@data4", data4);
                cmd.Parameters.AddWithValue("@data5", data5);


                cmd.ExecuteNonQuery();

                System.IO.Directory.CreateDirectory(@data3);
            }
            catch (Exception e)
            {


            }




        }

        public static void update(string data1, string data2, string data3,string id)
        {
            try
            {
                string path = "deneme.db";
                string cs = @"URI=file:"+Application.StartupPath+"\\deneme.db";



                DateTime bugun = DateTime.Now;

            
                var con = new SQLiteConnection(cs);
                con.Open();
                var cmd = new SQLiteCommand(con);



                string sql = "UPDATE data set data1='"+data1+"' ,data2='"+data2+"'   where id ="+id;

                cmd.CommandText = sql;
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Ok.");




            }
            catch (Exception e)
            {


            }




        }





    }
}
