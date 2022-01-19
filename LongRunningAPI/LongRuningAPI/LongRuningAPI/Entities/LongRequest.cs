using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LongRuningAPI.Entities
{
    public class LongRequest
    {
        [Key]
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public int ExecutionTime { get; set; }

        public DateTime? CompleteDate { get; set; }

        public bool Complete { get; set; }

        public string ThreadId { get; set; }

        public string ExternalId { get; set; }

        [NotMapped]
        public decimal PercentComplete
        {
            get
            {
                return Math.Round((decimal)SecondsExecuting / ExecutionTime * 100);
            }
        }

        public int SecondsExecuting
        {
            get
            {
                return (int)(DateTime.Now - StartDate).TotalSeconds;
            }
        }

    }
}
