using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Server
{
    class Program
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        //string connectionString = "Data Source=LAPTOP-CMFBOBNE\\SQLEXPRESS;Database=Verification;Integrated Security=SSPI";
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand insertCommand;
        SqlDataAdapter adapter = new SqlDataAdapter();
        int sqlRows = 0;

        public SqlDbType EmployeeName { get; private set; }

        public void ToAdd()
        {
            connection.Open();
            Console.WriteLine("How much rows you to insert?");
            int size = int.Parse(Console.ReadLine());
            for (int i = 0; i < size; i++)
            {

                insertCommand = new SqlCommand("insert into Aspire(employeeName,employeeId,employeeGender,employeeSalary) VALUES (@personName, @personId,@personGender, @personSalary)", connection);
                Console.WriteLine("Enter a programmer name");
                string employeeName = Console.ReadLine();
                insertCommand.Parameters.Add(new SqlParameter("@personName", employeeName));
                Console.WriteLine("Enter a identity ");
                int employeeId = int.Parse(Console.ReadLine());
                insertCommand.Parameters.Add(new SqlParameter("@personId",employeeId));
                Console.WriteLine("Enter a Gender of programmer");
                string employeeGender = Console.ReadLine();
                insertCommand.Parameters.Add(new SqlParameter("@personGender", employeeGender));
                Console.WriteLine("Enter a salary ");
                int employeeSalary = int.Parse(Console.ReadLine());
                insertCommand.Parameters.Add(new SqlParameter("@personSalary", employeeSalary));
                adapter.InsertCommand = insertCommand;
                sqlRows = adapter.InsertCommand.ExecuteNonQuery();
            }
            if (sqlRows >= 1)
            {
                Console.WriteLine("Details are added");
            }
            else
            {
                Console.WriteLine("Details are not added");
            }
            connection.Close();
            insertCommand.Dispose();
        }
        public void ToDelete()
        {
            connection.Open();
            insertCommand = new SqlCommand("delete Aspire where employeeId=@personId", connection);
            Console.WriteLine("Enter a Identity");
            insertCommand.Parameters.AddWithValue("@personId", int.Parse(Console.ReadLine()));
            adapter.DeleteCommand = insertCommand;
            sqlRows = adapter.DeleteCommand.ExecuteNonQuery();
            if (sqlRows >= 1)
            {
                Console.WriteLine("Successfully Deleted");
            }
            else
            {
                Console.WriteLine("Detail not Deleted");
            }
            insertCommand.Dispose();
            connection.Close();
        }
        public void ToUpdate()
        {
            connection.Open();
            insertCommand = new SqlCommand("update Aspire set employeeName=@personName where employeeId=@personId", connection);
            Console.WriteLine("Enter a Identity");
            insertCommand.Parameters.AddWithValue("@personId", int.Parse(Console.ReadLine()));
            Console.WriteLine("Enter a name for updating");
            insertCommand.Parameters.AddWithValue("@personName", Console.ReadLine());
            adapter.UpdateCommand = insertCommand;
            sqlRows = adapter.UpdateCommand.ExecuteNonQuery();
            if (sqlRows >= 1)
            {
                Console.WriteLine("Successfully updated");
            }
            else
            {
                Console.WriteLine("Details not updated");
            }
            insertCommand.Dispose();
            connection.Close();
        }
        public void ToDisplay()
        {
            connection.Open();
            insertCommand = new SqlCommand("select * from Aspire ", connection);
            SqlDataAdapter display = new SqlDataAdapter("SELECT * FROM Aspire", connection);
            DataSet dataSet = new DataSet();
            display.Fill(dataSet, "Aspire");
            foreach (DataRow row in dataSet.Tables["Aspire"].Rows)
            {
                Console.WriteLine("EmployeeName:" + row["employeeName"] + "\n" + "identity:" + row["employeeId"] + "\n" + "Gender:" + row["employeeGender"] + "\n" + "Salary:" + row["EmployeeSalary"]);
                Console.WriteLine();
            }
            insertCommand.Dispose();
            connection.Close();
        }


        public enum selectChoice
        { Insert = 1, Select, Update, Drop, Exit };
        static void Main(string[] args)
        {
            Program value = new Program();            
            do
            {
                Console.WriteLine("Enter Your Choice [1. Add 2.Display 3. Update 4.Delete 5.Exit] : ");
                int readChoice = Int32.Parse(Console.ReadLine());
                selectChoice myChoice = (selectChoice)readChoice;
                switch (myChoice)
                {
                    case selectChoice.Insert:
                        value.ToAdd();
                        break;
                    case selectChoice.Select:
                        value.ToDisplay();
                        break;
                    case selectChoice.Update:
                        value.ToUpdate();
                        
                        break;
                    case selectChoice.Drop:
                        value.ToDelete();
                        
                        break;
                    case selectChoice.Exit:
                        System.Environment.Exit(0);
                        
                        break;
                    default:
                        break;
                }
            } while (true);
        }
    }
}                  
            
    
