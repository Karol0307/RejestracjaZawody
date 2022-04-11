using Competitions.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Competitions.Helpers
{
    class DatabaseLocator
    {
        public static ZawodyEntities Database { get; set; }

        public static Uzytkownik user { get; set; }
    }
}
