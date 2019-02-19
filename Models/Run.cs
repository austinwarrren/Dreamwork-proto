using System;

namespace dreamwork_proto.Models
{
    public class Run
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public double Distance { get; set; }
        public DateTime RunDate { get; set; }
    }
}