using Ecommerce.Components;
using Ecommerce.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class HomeController : ControllerBase
  {
    private ICustomer _customer;
    public HomeController(ICustomer cust) {

      _customer = cust;
    }

    [HttpPost,Route("save")]
    [AllowAnonymous]
    public IActionResult InsertData([FromBody] CustomerModel customer)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      return Ok(_customer.InsertData(customer));
    }


    [HttpPost, Route("update")]
    [AllowAnonymous]
    public IActionResult UpdateData([FromBody] CustomerModel customer)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      return Ok(_customer.UpdateData(customer));
    }

    [HttpDelete, Route("delete/{ID}")]
    [AllowAnonymous]
    public IActionResult DeleteData([FromRoute] int ID)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      return Ok(_customer.DeleteData(ID));
    }

    [HttpPost, Route("getall")]
    [AllowAnonymous]
    public IActionResult Getall([FromBody] CustomerModel customer)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      return Ok(_customer.GetAll(customer));
    }

  }
}
