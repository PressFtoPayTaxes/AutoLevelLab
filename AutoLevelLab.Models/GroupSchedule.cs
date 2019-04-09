using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLevelLab.Models
{
    public class GroupSchedule
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int DayOfWeek { get; set; }
        public int AudithoryId { get; set; }
    }
}
