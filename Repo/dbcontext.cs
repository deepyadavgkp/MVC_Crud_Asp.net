using MVC_Demo.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVC_Demo.Repo
{
    public class dbcontext
    {
        private SqlConnection con;

        
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["conn"].ToString();
            con = new SqlConnection(constr);
        }

        // To Add Employee details
        public bool AddEmployee(CustomerModel obj)
        {


            connection();
            SqlCommand com = new SqlCommand("insert_C", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Firstname", obj.Firstname);
            com.Parameters.AddWithValue("@Lastname", obj.Lastname);
            com.Parameters.AddWithValue("@Age", obj.Age);
            com.Parameters.AddWithValue("@Dob", obj.Birthdate);
            com.Parameters.AddWithValue("@State", obj.State);
            com.Parameters.AddWithValue("@Gender", obj.Gender);
            com.Parameters.AddWithValue("@Photo", obj.Photo);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
           
            int i = com.ExecuteNonQuery();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // To view employee details with generic list
        public List<CustomerModel> GetAllEmployees()
        {
            connection();
            List<CustomerModel> EmpList = new List<CustomerModel>();

            SqlCommand com = new SqlCommand("select_C", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            // Bind EmpModel generic list using dataRow
            foreach (DataRow dr in dt.Rows)
            {
                EmpList.Add(
                    new CustomerModel
                    {
                        ID = Convert.ToInt32(dr["id"]),
                        Firstname = Convert.ToString(dr["Firstname"]),
                        Lastname = Convert.ToString(dr["Lastname"]),
                        Age = Convert.ToInt32(dr["Age"]),
                        Birthdate = Convert.ToDateTime(dr["Dob"]),
                        State = Convert.ToString(dr["State"]),
                        Gender = Convert.ToString(dr["Gender"]),
                        Photo = Convert.ToString(dr["Photo"]),

                    }
                );
            }

            return EmpList;
        }


        public bool UpdateEmployee(CustomerModel obj)
        {
            connection();
            SqlCommand com = new SqlCommand("UpdateC", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@id", obj.ID);
            com.Parameters.AddWithValue("@Firstname", obj.Firstname);
            com.Parameters.AddWithValue("@Lastname", obj.Lastname);
            com.Parameters.AddWithValue("@Age", obj.Age);
            com.Parameters.AddWithValue("@Dob", obj.Birthdate);
            com.Parameters.AddWithValue("@Age", obj.State);
            com.Parameters.AddWithValue("@Dob", obj.Gender);

            con.Open();
            int i = com.ExecuteNonQuery();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteEmployee(int Id)
        {
            connection();
            SqlCommand com = new SqlCommand("Delete_C_Id", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@id", Id);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            int i = com.ExecuteNonQuery();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}