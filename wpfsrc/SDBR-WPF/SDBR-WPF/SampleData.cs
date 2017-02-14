﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SDBR_WPF
{

    public class Person
    {
        public string Id { get; private set; }
        public string Message { get; private set; }
        public string Time { get; private set; }
        public string Status { get; private set; }

        public Person(string id, string message, string time,string status)
        {
            this.Id = id;
            this.Message = message;
            this.Time = time;
            this.Status = status;
        }
        public int StatusNumber
        {
            get
            {
                if (Status == "2") return 1;

                if (Status == "3") return 2;
                return 0;
            }
        }
    }
}
