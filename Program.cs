using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSalMngmt
{
    class Program
    {
        static void Main(string[] args)
        {
            int n1;
            Console.WriteLine("Choose Operation"+"\n"+" 1.Display"+"\n"+"" +
                " 2.Insert"+"\n"+" 3.Update"+"\n"+" 4.Delete");
            n1 = int.Parse(Console.ReadLine());
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = "IN-5CG016FP3C",
                InitialCatalog = "PayScaleDb",
                IntegratedSecurity = true
            };
            try {
                switch (n1)
                {
                    case 1:
                        {

                            using (SqlConnection con = new SqlConnection(builder.ConnectionString))
                            {
                                StringBuilder stringBuilder = new StringBuilder();
                                stringBuilder.Append("select PayBand,Basic,HRA,TA,DA,TDS");
                                stringBuilder.Append(",(Basic+HRA+TA+DA) as 'NetSalary'");
                                stringBuilder.Append(",(Basic+HRA+TA+DA-TDS) as 'InHandSalary'");
                                stringBuilder.Append("from Salary");
                                string cmdText = stringBuilder.ToString();
                                con.Open();
                                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                                {
                                    using (SqlDataReader sqlDataReader = cmd.ExecuteReader())
                                    {
                                        Console.WriteLine("PayBand" + "\t\t" + "Basic" + "\t" + "HRA" + "\t" + "TA" + "\t" + "DA" + "\t" + "TDS" + "\t"
                                            + "NetSalary" + "\t" + "InHandSalary");
                                        Console.WriteLine("\n");
                                        while (sqlDataReader.Read())
                                        {
                                            Console.Write(sqlDataReader["PayBand"] + "\t");
                                            Console.Write(sqlDataReader["Basic"] + "\t");
                                            Console.Write(sqlDataReader["HRA"] + "\t");
                                            Console.Write(sqlDataReader["TA"] + "\t");
                                            Console.Write(sqlDataReader["DA"] + "\t");
                                            Console.Write(sqlDataReader["TDS"] + "\t");
                                            Console.Write(sqlDataReader["NetSalary"] + "\t\t");
                                            Console.Write(sqlDataReader["InHandSalary"] + "\t");
                                            Console.WriteLine("\n");
                                        }
                                    }
                                }
                            }

                            break;
                        }
                    case 2:
                        {

                            using (SqlConnection con = new SqlConnection(builder.ConnectionString))
                            {
                                string Pay;
                                double BasicSalary;
                                Console.WriteLine("Enter PayBand");
                                Pay = Console.ReadLine();
                                Console.WriteLine("Enter Basic Salary");
                                BasicSalary = double.Parse(Console.ReadLine());
                                StringBuilder stringBuilder = new StringBuilder();
                                stringBuilder.Append("insert into Salary (PayBand,Basic) values");
                                stringBuilder.Append("(@payband,@basic);");
                                string cmdText = stringBuilder.ToString();
                                con.Open();
                                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                                {
                                    cmd.Parameters.AddWithValue("@payband", Pay);
                                    cmd.Parameters.AddWithValue("@basic", BasicSalary);
                                    cmd.ExecuteNonQuery();
                                }
                                Console.WriteLine("Record inserted successfully!!!");
                            }
                            break;
                        }
                    case 3:
                        {

                            using (SqlConnection con = new SqlConnection(builder.ConnectionString))
                            {
                                string Pay;
                                double BasicSalary;
                                double @pbasic;
                                Console.WriteLine("Enter Basic Salary to update Pay Band and Basic Salary");
                                pbasic = double.Parse(Console.ReadLine());
                                Console.WriteLine("Enter PayBand");
                                Pay = Console.ReadLine();
                                Console.WriteLine("Enter Basic Salary");
                                BasicSalary = double.Parse(Console.ReadLine());
                                StringBuilder stringBuilder = new StringBuilder();
                                stringBuilder.Append("update Salary set PayBand=@payband,");
                                stringBuilder.Append("Basic=@basic where Basic=@pbasic");
                                string cmdText = stringBuilder.ToString();
                                con.Open();
                                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                                {
                                   
                                    cmd.Parameters.AddWithValue("@pbasic", pbasic);
                                    cmd.Parameters.AddWithValue("@payband", Pay);
                                    cmd.Parameters.AddWithValue("@basic", BasicSalary);
                                    cmd.ExecuteNonQuery();
                                }
                                Console.WriteLine("Record updated successfully!!!");
                                break;
                            }
                        }
                case 4:
                        {


                            using (SqlConnection con = new SqlConnection(builder.ConnectionString))
                            {
                                double BasicSalary;
                                Console.WriteLine("Enter Basic Salary to Delete the Record");
                                BasicSalary = double.Parse(Console.ReadLine());
                                StringBuilder stringBuilder = new StringBuilder();
                                stringBuilder.Append("delete Salary where Basic=@basic");
                                string cmdText = stringBuilder.ToString();
                                con.Open();
                                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                                {
                                    cmd.Parameters.AddWithValue("@basic", BasicSalary);
                                    cmd.ExecuteNonQuery();
                                }
                                Console.WriteLine("Record Deleted successfully!!!");
                            }
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("No Operation exits here!!!!");
                            break;
                        }
                }

            }
             catch (Exception ex)
            {
                Console.WriteLine("Error!!!" + ex.Message);
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}




