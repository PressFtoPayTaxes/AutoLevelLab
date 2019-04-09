using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLevelLab.Models
{
    public class TeacherSchedule
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int DayOfWeek { get; set; }
        public int AudithoryId { get; set; }
    }
}
