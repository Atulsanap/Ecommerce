using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Components
{
   public interface ICustomer
  {
    DataTable InsertData(CustomerModel customer);

    DataSet UpdateData(CustomerModel customer);

    DataTable DeleteData(int ID);

    DataSet GetAll(CustomerModel customer);


  }
}
