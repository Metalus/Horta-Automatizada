using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HortaAutomatizada.Model
{
    public class DataJson
    {
        public int UmidadeAr { get; set; }
        public int UmidadeSolo { get; set; }

        public int Tanque1 { get; set; }
        public int Tanque2 { get; set; }
        public int Tanque3 { get; set; }

        public Dictionary<DayOfWeek,List<DateTime>> Agenda { get; set; }
    }
}
