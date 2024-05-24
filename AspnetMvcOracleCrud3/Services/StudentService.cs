using AspnetMvcOracleCrud3.Interfaces;
using AspnetMvcOracleCrud3.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace AspnetMvcOracleCrud3.Services
{
    public class StudentService:IStudentService
    {
        private readonly string _connectionString;

        public StudentService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["OracleConnectionString"]].ConnectionString;
        }

        public IEnumerable<Student> GetAllStudents()
        { 
            List<Student> students=new List<Student>();

            using(OracleConnection conn=new OracleConnection(_connectionString))
            {
                using(OracleCommand cmd=conn.CreateCommand())
                {
                    conn.Open();
                    cmd.BindByName = true;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "pgk_student_manager.get_students";
                    cmd.Parameters.Add("pvc_studentid",OracleDbType.Varchar2,10,null,System.Data.ParameterDirection.Input);
                    cmd.Parameters.Add("pcr_students", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
                    OracleDataReader reader=cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        Student student = new Student()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = Convert.ToString(reader["name"]),
                            Email = Convert.ToString(reader["email"]),
                        };
                        students.Add(student);
                    }
                }
            }
            return students;
        }

        public Student GetStudentById(int id)
        {
            Student student = new Student();

            using (OracleConnection conn = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.BindByName = true;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "pgk_student_manager.get_students";
                    cmd.Parameters.Add("pvc_studentid", OracleDbType.Varchar2, 10, id, System.Data.ParameterDirection.Input);
                    cmd.Parameters.Add("pcr_students", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);

                    OracleDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        student.Id = Convert.ToInt32(reader["id"]);
                        student.Name = Convert.ToString(reader["name"]);
                        student.Email = Convert.ToString(reader["email"]);
                    }
                }
            }
            return student;
        }

        public void AddStudent(Student student)
        {
            try
            { 
                using(OracleConnection conn=new OracleConnection(_connectionString))
                {
                    using(OracleCommand cmd=conn.CreateCommand())
                    {
                        conn.Open();
                        cmd.CommandType=CommandType.StoredProcedure;
                        cmd.CommandText = "pgk_student_manager.set_student";
                        cmd.Parameters.Add("pvc_studentid", OracleDbType.Varchar2, 10, null, ParameterDirection.InputOutput);
                        cmd.Parameters.Add("pvc_studentname", OracleDbType.Varchar2, 200, student.Name, ParameterDirection.Input);
                        cmd.Parameters.Add("pvc_studentemail", OracleDbType.Varchar2, 200, student.Email, ParameterDirection.Input);
                        cmd.Parameters.Add("pvc_appuser", OracleDbType.Varchar2, 200, "syful", ParameterDirection.Input);
                        cmd.Parameters.Add("pvc_status", OracleDbType.Varchar2, 200, null, ParameterDirection.Output);
                        cmd.Parameters.Add("pvc_statusmsg", OracleDbType.Varchar2, 200, null, ParameterDirection.Output);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public void EditStudent(Student student)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(_connectionString))
                {
                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "pgk_student_manager.set_student";
                        cmd.Parameters.Add("pvc_studentid", OracleDbType.Varchar2, 10, student.Id, ParameterDirection.InputOutput);
                        cmd.Parameters.Add("pvc_studentname", OracleDbType.Varchar2, 200, student.Name,ParameterDirection.Input);
                        cmd.Parameters.Add("pvc_studentemail", OracleDbType.Varchar2, 200, student.Email,ParameterDirection.Input);
                        cmd.Parameters.Add("pvc_appuser", OracleDbType.Varchar2, 200, "syful",ParameterDirection.Input);
                        cmd.Parameters.Add("pvc_status", OracleDbType.Varchar2, 200, null,ParameterDirection.Output);
                        cmd.Parameters.Add("pvc_statusmsg", OracleDbType.Varchar2, 200, null, ParameterDirection.Output);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void DeleteStudent(Student student)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(_connectionString))
                {
                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "pgk_student_manager.del_student";
                        cmd.Parameters.Add("pvc_studentid", OracleDbType.Varchar2, 10, student.Id, ParameterDirection.InputOutput);
                        cmd.Parameters.Add("pvc_appuser", OracleDbType.Varchar2, 200, "syful", ParameterDirection.Input);
                        cmd.Parameters.Add("pvc_status", OracleDbType.Varchar2, 200, null, ParameterDirection.Output);
                        cmd.Parameters.Add("pvc_statusmsg", OracleDbType.Varchar2, 200, null, ParameterDirection.Output);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}