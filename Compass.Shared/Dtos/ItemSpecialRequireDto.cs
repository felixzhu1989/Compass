using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compass.Shared.Dtos;

public class ItemSpecialRequireDto:BaseDto
{
    private string require;
    public string Require
    {
        get { return require; }
        set { require = value; OnPropertyChanged(); }
    }
}
