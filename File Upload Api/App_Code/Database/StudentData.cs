using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StudentData
/// </summary>
public class StudentData
{
    private int _Id;
    private string _Name;
    private string _Email;
    private string _Mobile;
    private string _Password;
    private string _Age;
    private int sid;

    public StudentData()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //Create Student 
    public void CreateStudent()
    {
        List<MySqlParameter> param = new List<MySqlParameter>();
        param.Add(new MySqlParameter("@name", Name));
        param.Add(new MySqlParameter("@email", Email));
        param.Add(new MySqlParameter("@mobile", Mobile));
        param.Add(new MySqlParameter("@password", Password));
        param.Add(new MySqlParameter("@age", Age));
        param.Add(new MySqlParameter("@image", Image));
        Connection connect = new Connection();
        connect.ExecStatement("INSERT INTO student(name,email,mobile,password,age,image) VALUES(@name,@email,@mobile,@password,@age,@image)", param);
        connect.Dispose();
        connect = null;
    }

    public StudentData(int Id)
    {
        Connection connect = new Connection();
        List<MySqlParameter> param = new List<MySqlParameter>();
        param.Add(new MySqlParameter("@id", Id));
        using (DataSet ds = connect.GetDataset("select * from student where id=@id", param))
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                HasValue = true;
                _Id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                _Name = ds.Tables[0].Rows[0]["name"].ToString();
                _Email = ds.Tables[0].Rows[0]["email"].ToString();
                _Mobile = ds.Tables[0].Rows[0]["mobile"].ToString();
                _Password = ds.Tables[0].Rows[0]["password"].ToString();
                _Age = ds.Tables[0].Rows[0]["age"].ToString();
                Image = ds.Tables[0].Rows[0]["image"].ToString();
            }
        }
    }


    public void  UpdateStudent(int Id)
    {
        Connection connect = new Connection();
        List<MySqlParameter> param = new List<MySqlParameter>();
        param.Add(new MySqlParameter("@name", Name));
        param.Add(new MySqlParameter("@email", Email));
        param.Add(new MySqlParameter("@mobile", Mobile));
        param.Add(new MySqlParameter("@password", Password));
        param.Add(new MySqlParameter("@age", Age));
        connect.ExecStatement("update student SET name=@name,email=@email,mobile=@mobile,password=@password,age=@age where id=" + Id, param);
        connect.Dispose();
        connect = null;  
    }


    public void DeleteStudent(string query)
    {
        Connection connect = new Connection();
        connect.ExecStatement(query);
        connect.Dispose();
        connect = null;
    }


    public DataSet GetStudent(String query)
    {
        Connection connect = new Connection();
        List<MySqlParameter> param = new List<MySqlParameter>();
        DataSet ds = connect.GetDataset(query);
        return ds;
    }









    public int Id { get { return _Id; } set { _Id = value; } }
    public string Name { get { return _Name; } set { _Name = value; } }
    public string Email { get { return _Email; } set { _Email = value; } }
    public string Mobile { get { return _Mobile; } set { _Mobile = value; } }
    public string Password { get { return _Password; } set { _Password = value; } }
    public string Age { get { return _Age; } set { _Age = value; } }
    public string Image { get; set; }
    public bool HasValue
    {
        get;
        set;
    }
}