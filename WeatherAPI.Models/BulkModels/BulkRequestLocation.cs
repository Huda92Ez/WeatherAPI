﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI.Models.BulkModels
{
    public class BulkRequestLocation
    {

        public string q { set; get; }

        public string custom_id { set; get; }
    }
}
