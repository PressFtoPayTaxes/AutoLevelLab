using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLevelLab.Models
{
    public class Syllabus
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public DateTime Date { get; set; }
        public int SubjectId { get; set; }
    }
}
