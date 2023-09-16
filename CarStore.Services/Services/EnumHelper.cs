using CarStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarStore.Services.Services
{
    public class EnumHelper
    {
        public static Status EnumParse(string value, Status defaultStatus)
        {
            if (!Enum.TryParse(value, out Status carStatus))
            {
                return defaultStatus;
            }
            return carStatus;
        }
    }
}

