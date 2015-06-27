using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpulseApp.Models.DTO
{
    public class GeneralTableModel
    {
        public GeneralTableModel()
        {
            Keys = new List<String>();
            Lines = new List<List<String>>();
        }
        public List<String> Keys { get; set; }
        public List<List<String>> Lines { get; set; }
    }
}
