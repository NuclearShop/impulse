using ImpulseApp.Models.UtilModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpulseApp.Models.DTO
{
    public class ModeratorViewDTO
    {
        public SimpleAdModelDTO ad { get; set; }
        public ModeratorView review { get; set; }
    }
}
