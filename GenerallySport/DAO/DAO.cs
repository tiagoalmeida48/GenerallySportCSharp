using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenerallySport.DAO
{
    public class DAO
    {
        public string connectionString = @"server=DESKTOP-US4LHLF\SQLEXPRESS; database=GENERALLY;integrated security=yes;";

        public string ConnString
        {
            get
            {
                return this.connectionString;
            }
        }
    }
}

