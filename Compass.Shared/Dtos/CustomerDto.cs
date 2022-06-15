using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compass.Shared.Dtos;

public class CustomerDto:BaseDto
{
    private string customerName;
    public string CustomerName
    {
        get { return customerName; }
        set { customerName = value; OnPropertyChanged(); }
    }
}
