using Ecommerce.Components;
using System;
using System.ComponentModel;

namespace Ecommerce.Model
{
  public class CustomerModel
  {
    public int ID { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public float Price { get; set; }

    public string ProcessType { get; set; }
  }
}
