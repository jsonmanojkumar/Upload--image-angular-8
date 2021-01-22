using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ResponseData
/// </summary>
public class ResponseData
{

    public string _Message;
    public string _Description;
    public Object _Data;
    public ResponseData()
    {
        //
        // TODO: Add constructor logic here
        //
    }

     
    public string Message
    {
        get { return _Message; }
        set { _Message = value; }
    }
    public string Description
    {
        get { return _Description; }
        set { _Description = value; }
    }
    public Object Data
    {
        get { return _Data; }
        set { _Data = value; }
    }
}