using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Competitions.ViewModels.Controls
{
    public class ActiveCompetitionViewModel
    {
        public int id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public double Distance { get; set; }

        public string Place { get; set; }

        public int Number { get; set; }
    }
}
