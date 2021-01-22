using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;


/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 

[System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{

    public WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }



    [WebMethod]
    public void CreateStudent(string name, string email, string mobile, string password, string age, string image)
    {
        string responce = "";
        try
        {
            StudentData sd = new StudentData();
            sd.Name = name;
            sd.Email = email;
            sd.Mobile = mobile;
            sd.Password = password;
            sd.Age = age;
            sd.Image = image;
            sd.CreateStudent();
            ResponseData rs = new ResponseData();
            rs.Message = "Student Created";
            rs.Description = "successfully";
            JavaScriptSerializer js = new JavaScriptSerializer();
            responce = js.Serialize(rs);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.AddHeader("content-length", responce.Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(responce);
        }
        catch (Exception ex)
        {

        }
    }

    [WebMethod]
    public void UpdateStudent(int id, string name, string email, string mobile, string password, string age)
    {
        string response = "";
        try
        {
            StudentData sd = new StudentData();
            sd.Name = name;
            sd.Email = email;
            sd.Mobile = mobile;
            sd.Password = password;
            sd.Age = age;
            sd.UpdateStudent(id);

        }
        catch (Exception ex)
        {

        }
        ResponseData rs = new ResponseData();
        rs.Message = "Student Updated";
        rs.Description = "successfully";
        JavaScriptSerializer js = new JavaScriptSerializer();
        response = js.Serialize(rs);
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.AddHeader("content-length", response.Length.ToString());
        Context.Response.Flush();
        Context.Response.Write(response);
    }

    [WebMethod]
    public void GetStudent()
    {
        string responce = "";
        ResponseData rsData = new ResponseData();
        List<StudentData> slist = new List<StudentData>();
        try
        {
            StudentData sdata = new StudentData();
            DataSet ds = sdata.GetStudent("select * from student");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                int sid = int.Parse(ds.Tables[0].Rows[i][0].ToString());
                StudentData sidData = new StudentData(sid);
                slist.Add(sidData);
            }
        }
        catch (Exception ex)
        {

        }
        rsData.Message = "Student Data Fatched ";
        rsData.Description = "successfully";
        rsData.Data = slist;
        JavaScriptSerializer js = new JavaScriptSerializer();
        responce = js.Serialize(rsData);
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.AddHeader("content-length", responce.Length.ToString());
        Context.Response.Flush();
        Context.Response.Write(responce);
    }

    [WebMethod]
    public void DeleteStudent(int Id)
    {
        string responce = "";
        ResponseData rsData = new ResponseData();
        List<StudentData> slist = new List<StudentData>();
        try
        {
            StudentData sd = new StudentData();
            sd.GetStudent("delete from student where id=" + Id);
        }
        catch (Exception ex)
        {

        }
        rsData.Message = "Student Data deleted ";
        rsData.Description = "successfully";
        rsData.Data = slist;
        JavaScriptSerializer js = new JavaScriptSerializer();
        responce = js.Serialize(rsData);
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.AddHeader("content-length", responce.Length.ToString());
        Context.Response.Flush();
        Context.Response.Write(responce);
    }

    [WebMethod]
    public void CreateUser(string name, string email, string password)
    {
        //DataSet ds=null;
        string res = "";
        string responce = "";
        ResponseData rsData = new ResponseData();
        // List<UsersData> clist = new List<UsersData>();
        try
        {

            SingUpData udata = new SingUpData(email);
            if (udata.HasValue)
            {
                rsData.Message = "This Email Already Exist";
                rsData.Description = "Data Not Inserted";
                JavaScriptSerializer j = new JavaScriptSerializer();
                responce = j.Serialize(rsData);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.AddHeader("content-length", responce.Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(responce);
            }
            else
            {
                udata.Name = name;
                udata.Email = email;
                udata.Password = password;
                udata.SaveUser();


                DataSet dsuid = udata.GetStudent("select max(id) as uid from signup");
                SingUpData usdata = new SingUpData();
                DataSet udetail = usdata.GetStudent("select * from signup where id=" + int.Parse(dsuid.Tables[0].Rows[0]["uid"].ToString()));
                usdata.Id = int.Parse(udetail.Tables[0].Rows[0]["id"].ToString());
                usdata.Name = udetail.Tables[0].Rows[0]["name"].ToString();
                usdata.Password = udetail.Tables[0].Rows[0]["password"].ToString();
                usdata.Email = udetail.Tables[0].Rows[0]["email"].ToString();


                rsData.Message = "User inserted successfully";
                rsData.Description = "successfully";
                rsData.Data = usdata;

                JavaScriptSerializer js = new JavaScriptSerializer();
                responce = js.Serialize(rsData);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.AddHeader("content-length", responce.Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(responce);
            }
        }
        catch (Exception ex)
        {

        }
    }

    [WebMethod]
    public void UpdateUser(int id, string name, string email, string password)
    {
        //DataSet ds=null;
        string res = "";
        string responce = "";
        ResponseData rsData = new ResponseData();
        // List<UsersData> clist = new List<UsersData>();
        try
        {
            SingUpData udata = new SingUpData();
            udata.Name = name;
            udata.Email = email;
            udata.Password = password;
            udata.UpdateUser(id);
            rsData.Message = "User Updated";
            rsData.Description = "successfully";

            JavaScriptSerializer js = new JavaScriptSerializer();
            responce = js.Serialize(rsData);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.AddHeader("content-length", responce.Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(responce);
        }
        catch (Exception ex)
        {

        }
    }

    [WebMethod]

    public void userLogin(string email, string password)
    {
        try
        {
            ResponseData rdata = new ResponseData();
            SingUpData udata = new SingUpData();
            udata.checkUserStatus(email, password);
            if (udata.HasValue)
            {

                SingUpData usdata = new SingUpData();
                DataSet dsUserInfo = usdata.GetStudent("select * from signup where email='" + email + "' and password='" + password + "'  ");
                //DataSet dsUserInfo = usdata.getUsers("select * from signup where  (name='" + name  + "' or mobile='" + mobile + "' or email='" + email + "' or password='" + password + "' ) and password='" + password + "'");
                usdata.Id = int.Parse(dsUserInfo.Tables[0].Rows[0]["id"].ToString());
                usdata.Name = dsUserInfo.Tables[0].Rows[0]["name"].ToString();
                usdata.Email = dsUserInfo.Tables[0].Rows[0]["email"].ToString();
                usdata.Password = dsUserInfo.Tables[0].Rows[0]["password"].ToString();
                rdata.Message = "Success";
                rdata.Description = "User SuccessFully Login";
                rdata.Data = usdata;
            }
            else
            {
                rdata.Message = "Not Found";
                rdata.Description = "User Not Found In The List";
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            string str = js.Serialize(rdata);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.AddHeader("content-length", str.Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(str);
        }
        catch (Exception ex)
        {

        }
    }

    [WebMethod]
    public void DeleteUser(int Id)
    {
        string responce = "";
        ResponseData rsData = new ResponseData();
        List<SingUpData> slist = new List<SingUpData>();
        try
        {
            SingUpData sd = new SingUpData();
            sd.GetStudent("update signup set status=0 where id=" + Id);
        }
        catch (Exception ex)
        {

        }
        rsData.Message = "User Deleted ";
        rsData.Description = "successfully";
        rsData.Data = slist;
        JavaScriptSerializer js = new JavaScriptSerializer();
        responce = js.Serialize(rsData);
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.AddHeader("content-length", responce.Length.ToString());
        Context.Response.Flush();
        Context.Response.Write(responce);
    }

    [WebMethod]
    public void UploadImage(string name,string imageName, string imagebase64)
    {
        ResponseData rdata = new ResponseData();
        try
        {
            String path = HttpContext.Current.Server.MapPath("~/Storage");
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            string imgAdharPath = Path.Combine(path, imageName);
            byte[] imageBytes = Convert.FromBase64String(imagebase64);
            File.WriteAllBytes(imgAdharPath, imageBytes);         
            ImageData mpData = new ImageData();
            mpData.Name = name;
            mpData.imageName = imageName;
            mpData.image_file = imagebase64;
            mpData.Save();

            rdata.Message = "Image Uploaded";
            rdata.Description = "User Image Uploaded";

            JavaScriptSerializer js = new JavaScriptSerializer();
            string str = js.Serialize(rdata);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.AddHeader("content-length", str.Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(str);
        }
        catch (Exception ex)
        {

        }
    }

 
}
