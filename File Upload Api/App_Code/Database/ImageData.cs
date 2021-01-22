using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ImageData
/// </summary>
public class ImageData
{
    public ImageData()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public void Save()
    {
        List<MySqlParameter> param = new List<MySqlParameter>();
        param.Add(new MySqlParameter("@name", Name));
        param.Add(new MySqlParameter("@imageName", imageName));
        param.Add(new MySqlParameter("@image_file", image_file));
        Connection connect = new Connection();
        connect.ExecStatement("INSERT INTO image(name,imageName,image_file) VALUES(@name,@imageName,@image_file)", param);
        connect.Dispose();
        connect = null;
    }

    public ImageData(int Id)
    {
        Connection connect = new Connection();
        List<MySqlParameter> param = new List<MySqlParameter>();
        param.Add(new MySqlParameter("@id", Id));
        using (DataSet ds = connect.GetDataset("select * from image where id=@id", param))
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                HasValue = true;
                Id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                Name = ds.Tables[0].Rows[0]["name"].ToString();
                imageName = ds.Tables[0].Rows[0]["image"].ToString();
               
            }
        }
    }


    public void UpdateUser(int Id)
    {
        Connection connect = new Connection();
        List<MySqlParameter> param = new List<MySqlParameter>();
        param.Add(new MySqlParameter("@name", Name));
        param.Add(new MySqlParameter("@image", imageName));
     
        connect.ExecStatement("update image SET name=@name,image=@image  where id=" + Id, param);
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
    public string Name { get; set; }
    public string imageName { get; set; }
    public string image_file { get; set; }
    public bool HasValue
    {
        get;
        set;
    }
}