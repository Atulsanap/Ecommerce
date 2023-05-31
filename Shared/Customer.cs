using Ecommerce.Components;
using Ecommerce.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Ecommerce.Shared
{
  public class Customer : ICustomer
  {
    //private XMLCast xml;
    private IConfiguration configure;

    public Customer( IConfiguration _config)
    {
     // xml = _xml;
      configure = _config;
    }

    public DataTable DeleteData(int ID)
    {
      CustomerModel cust = new CustomerModel();
      cust.ID = ID;
      DataTable response = GetDataTable("SaveData", CreateXML(cust));
      return response;
    }

    public DataTable InsertData(Model.CustomerModel customer)
    {
      DataTable result = GetDataTable("SaveData", CreateXML(customer));

      return result;
    }

    public DataSet UpdateData(Model.CustomerModel customer)
    {
      DataSet response = GetDataSet("SaveData", CreateXML(customer));
      return response;
    }

    public DataSet GetAll(CustomerModel customer)
    {
      DataSet response = GetDataSet("SaveData", CreateXML(customer));
      return response;
    }


    public DataSet GetDataSet(string storedProcedure, string entityXML)
    {
      try
      {
        using (var con = new SqlConnection(DBConnectionString()))
        {
          DataSet ds = new DataSet("dtRoot");
          using (var da = new SqlDataAdapter(storedProcedure, con))
          {
            using (var cmd = new SqlCommand(storedProcedure, con))
            {
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.Parameters.AddWithValue("@xml", entityXML);
              da.SelectCommand = cmd;
              da.Fill(ds);
              return ds;
            }
          }
        }
      }
      catch (Exception ex)
      {
        return null;
      }
    }

    private  DataTable GetDataTable(string storedProcedure, string entityXML)
    {
      try
      {
        using (var con = new SqlConnection(DBConnectionString()))
        {
          DataTable dt = new DataTable("dtRoot");
          using (var da = new SqlDataAdapter(storedProcedure, con))
          {
            using (var cmd = new SqlCommand(storedProcedure, con))
            {
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.Parameters.AddWithValue("@xml", entityXML);
              da.SelectCommand = cmd;
              da.Fill(dt);
              return dt;
            }
          }
        }
      }
      catch (Exception ex)
      {
        return null;
      }
    }

    public string DBConnectionString()
    {
      var connectionString = configure.GetSection("ConnectionStrings")["DefaultConnection"];
      return connectionString;
    }

    public string CreateXML(dynamic entity)
    {
      //Create a xml object
      XmlDocument xmlDoc = new XmlDocument();
      //Create a XmlSerializer object from entity
      XmlSerializer xmlSerializer = new XmlSerializer(entity.GetType());
      //Creates a stream whose backing store is memory. 
      using (MemoryStream xmlStream = new MemoryStream())
      {
        xmlSerializer.Serialize(xmlStream, entity);
        xmlStream.Position = 0;
        //Loads the XML document from the specified string.
        xmlDoc.Load(xmlStream);
        return xmlDoc.InnerXml;
      }
    }
  }
}
