using Compass.Shared.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compass.Shared.Dtos;
/// <summary>
/// 客户
/// </summary>
public class CustomerDto:BaseDto
{
    private string customerName;
    public string CustomerName
    {
        get { return customerName; }
        set { customerName = value; OnPropertyChanged(); }
    }
    private CustomerType_e customerType;
    public CustomerType_e CustomerType
    {
        get { return customerType; }
        set { customerType = value; OnPropertyChanged(); }
    }
}
