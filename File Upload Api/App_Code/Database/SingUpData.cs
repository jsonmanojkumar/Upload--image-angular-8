using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SingUpData
/// </summary>
public class SingUpData
{
    public SingUpData()
    {
        //
        // TODO: Add constructor logic here
        //
    }
 
    //Create Student 
    public void SaveUser()
    {
        List<MySqlParameter> param = new List<MySqlParameter>();
        param.Add(new MySqlParameter("@name", Name));
        param.Add(new MySqlParameter("@email", Email));
        param.Add(new MySqlParameter("@password", Password));
        Connection connect = new Connection();
        connect.ExecStatement("INSERT INTO signup(name,email,password) VALUES(@name,@email,@password)", param);
        connect.Dispose();
        connect = null;
    }

    public SingUpData(int Id)
    {
        Connection connect = new Connection();
        List<MySqlParameter> param = new List<MySqlParameter>();
        param.Add(new MySqlParameter("@id", Id));
        using (DataSet ds = connect.GetDataset("select * from signup where id=@id", param))
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                HasValue = true;
                Id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                Name = ds.Tables[0].Rows[0]["name"].ToString();
                Email = ds.Tables[0].Rows[0]["email"].ToString();
                Password = ds.Tables[0].Rows[0]["password"].ToString();
            }
        }
    }

    public SingUpData(string email)
    {
        Connection connect = new Connection();
        List<MySqlParameter> param = new List<MySqlParameter>();
        param.Add(new MySqlParameter("@email", email));
        using (DataSet ds = connect.GetDataset("SELECT * FROM signup WHERE email=@email", param))
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                HasValue = true;
                Id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                Name = ds.Tables[0].Rows[0]["name"].ToString();
                Email = ds.Tables[0].Rows[0]["email"].ToString();
                Password = ds.Tables[0].Rows[0]["password"].ToString();
            }
        }
    }

    public void UpdateUser(int Id)
    {
        Connection connect = new Connection();
        List<MySqlParameter> param = new List<MySqlParameter>();
        param.Add(new MySqlParameter("@name", Name));
        param.Add(new MySqlParameter("@email", Email));
        param.Add(new MySqlParameter("@password", Password));
        connect.ExecStatement("update signup SET name=@name,email=@email,password=@password where id=" + Id, param);
        connect.Dispose();
        connect = null;
    }

    public void checkUserStatus(string email, string password)
    {
        Connection connect = new Connection();
        List<MySqlParameter> param = new List<MySqlParameter>();
        param.Add(new MySqlParameter("@email", email));
        param.Add(new MySqlParameter("@password", password));
        using (DataSet ds = connect.GetDataset("SELECT * FROM signup WHERE email=@email and password=@password", param))
        //using (DataSet ds = connect.GetDataset("select * from signup where (mobile=@mobile) and aor password=@password  ", param))
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                HasValue = true;
                Id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                Name = ds.Tables[0].Rows[0]["name"].ToString();
                Email = ds.Tables[0].Rows[0]["email"].ToString();
                Password = ds.Tables[0].Rows[0]["password"].ToString();
            }
            else
            {
                HasValue = false;
            }
        }
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

    public int Id { get; set; }
    public string  Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool HasValue
    {
        get;
        set;
    }
}