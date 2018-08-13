using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassINI;
using System.Data;
using System.Data.SqlClient;

namespace GetNAVChinese
{
    class Program
    {
        static InitialINI ini = new InitialINI(@"C:\TeleFtp\FTP.ini");
        static void Main(string[] args)
        {
            GetChineseNameByJBnumTesting();
        }
        static String GetChineseNameByJBnumTesting()
        {
            String constr = ini.IniReadValue("PDF", "connectionstring");
            SqlConnection sqlcon = new SqlConnection(constr);
            SqlCommand sqlcmd;
            SqlDataReader dr;
            String sqlstr = "";
            String cn = "";
            String cn2 = "";
            try
            {
                sqlcon.Open();
                //sqlstr = "SELECT * FROM [Celki_Dev2].[dbo].[Celki International Limited$Customer]  where upper([No_]) = upper('JB112121')";
                sqlstr = "SELECT * FROM [Celki_Dev2].[dbo].[Celki International Limited$Customer]  where upper([No_]) in ('JB030642', 'JB030644')";
                sqlcmd = new SqlCommand(sqlstr, sqlcon);
                dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    //Encoding wind1252 = Encoding.GetEncoding(1252);
                    //Encoding utf8 = Encoding.UTF8;

                    byte[] wb = null;
                    byte[] wb2 = null;

                    wb = System.Text.Encoding.GetEncoding("windows-1252").GetBytes(dr["Chinese Name"].ToString());
                    cn = System.Text.Encoding.Default.GetString(wb, 0, wb.Length);
                    //cn = dr["Chinese Name"].ToString();
                    System.Console.WriteLine(cn);

                    //wb2 = Encoding.Convert(wind1252, utf8, wb);
                    //cn2 = Encoding.UTF8.GetString(wb);
                    //System.Console.WriteLine(cn2);

                    //wb2 = System.Text.Encoding.GetEncoding("utf-8").GetBytes(cn);
                    //cn2 = System.Text.Encoding.Default.GetString(wb2, 0, wb2.Length);
                    //System.Console.WriteLine(cn2);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                //File.AppendAllText("Log.txt", ex.ToString() + Environment.NewLine);
                //Writelog(ex.ToString() + Environment.NewLine);
            }
            finally
            {
                sqlcon.Close();
                sqlcon.Dispose();
                System.Console.ReadKey();
            }
            return cn;
        }
    }
}
