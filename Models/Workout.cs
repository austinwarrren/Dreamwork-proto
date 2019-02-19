using System;

namespace dreamwork_proto.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string content { get; set; }
    }
}