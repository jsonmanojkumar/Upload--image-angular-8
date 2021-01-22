using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;


public class Connection
{
    string conn;
    MySqlConnection con;
    MySqlCommand cmd;

    public Connection()
    {
        conn = System.Configuration.ConfigurationManager.ConnectionStrings["mySQLconn"].ConnectionString;
        //@"server=192.168.2.101\SQLEXPRESS;user id=sa;password=sa123;database=eCommerceDB";
        //conn = ReadConnectionString();
        con = new MySqlConnection(conn);
    }

    private MySqlCommand SqlCmnd()
    {
        if (con.State == 0) con.Open();
        MySqlCommand cmd = new MySqlCommand();
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        return cmd;
    }

    public void ExecStatement(String sql)
    {
        MySqlCommand cmd = new MySqlCommand();
        cmd = SqlCmnd();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        con.Close();
    }

    public DataSet GetDataset(String sql)
    {
        con.Close();
        con.Open();
        MySqlDataAdapter Adapter = new MySqlDataAdapter();
        DataSet ds = new DataSet();
        MySqlCommand cmd;
        cmd = SqlCmnd();
        cmd.CommandText = sql;
        cmd.CommandTimeout = 12000;
        Adapter.SelectCommand = cmd;
        Adapter.Fill(ds);
        Adapter.Dispose();
        cmd.Dispose();
        con.Close();
        return ds;
    }

    public DataSet GetDataset(String sql, MySqlTransaction MyTrans)
    {
        MySqlDataAdapter Adapter = new MySqlDataAdapter();
        DataSet ds = new DataSet();
        MySqlCommand cmd;
        cmd = SqlCmnd();
        cmd.Transaction = MyTrans;
        cmd.CommandText = sql;
        Adapter.SelectCommand = cmd;
        Adapter.Fill(ds);
        Adapter.Dispose();
        cmd.Dispose();
        return ds;
    }
    public DataSet GetDataset(String sql, List<MySqlParameter> Parameters)
    {
        MySqlDataAdapter Adapter = new MySqlDataAdapter();
        DataSet ds = new DataSet();
        MySqlCommand cmd;
        cmd = SqlCmnd();
        for (int i = 0; i < Parameters.Count; i++)
            cmd.Parameters.Add(Parameters[i]);
        cmd.CommandText = sql;
        // Adapter.Fill(ds);
        Adapter.SelectCommand = cmd;
        Adapter.Fill(ds);
        Adapter.Dispose();
        cmd.Dispose();
        return ds;
    }
    public DataSet GetDataset(String sql, List<MySqlParameter> Parameters, MySqlTransaction MyTrans)
    {
        MySqlDataAdapter Adapter = new MySqlDataAdapter();
        DataSet ds = new DataSet();
        MySqlCommand cmd;
        cmd = SqlCmnd();
        cmd.Transaction = MyTrans;
        for (int i = 0; i < Parameters.Count; i++)
            cmd.Parameters.Add(Parameters[i]);
        cmd.CommandText = sql;
        Adapter.SelectCommand = cmd;
        Adapter.Fill(ds);
        Adapter.Dispose();
        cmd.Dispose();
        return ds;
    }
    public void ExecStatement(String sql, List<MySqlParameter> Parameters)
    {
        using (MySqlCommand cmd = SqlCmnd())
        {
            cmd.CommandText = sql;
            for (int i = 0; i < Parameters.Count; i++)
                cmd.Parameters.Add(Parameters[i]);
            cmd.ExecuteNonQuery();
        }
    }

    public void ExecStatement(String sql, MySqlTransaction myTrans)
    {
        MySqlCommand cmd = new MySqlCommand();
        cmd = SqlCmnd();
        cmd.Transaction = myTrans;
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
    }

    public void ExecStatement(String sql, List<MySqlParameter> Parameters, MySqlTransaction myTrans)
    {
        MySqlCommand cmd = new MySqlCommand();
        cmd = SqlCmnd();
        cmd.Transaction = myTrans;
        cmd.CommandText = sql;
        for (int i = 0; i < Parameters.Count; i++)
            cmd.Parameters.Add(Parameters[i]);
        cmd.ExecuteNonQuery();
        cmd.Dispose();
    }

    public void StartTrans(ref MySqlTransaction myTrans)
    {
        MySqlConnection clscon = con;
        clscon.Open();
        myTrans = clscon.BeginTransaction();
    }


    public void Dispose()
    {
        con.Close();
        con.Dispose();
    }

    ~Connection()
    {
        // if (con != null) con.Dispose();
    }

    //static string ReadConnectionString()
    //{
    //    string appPath = System.Windows.Forms.Application.StartupPath;
    //    ////return Convert.ToString(File.ReadAllText(new Uri(appPath+@"\connection\mycon.txt").LocalPath));
    //    return @"Data Source=.\SQLEXPRESS;AttachDbFileName=" + appPath + @"\DB\Almighty.mdf; Integrated Security=True;User Instance=True";
    //    ////return null;
    //}
}
