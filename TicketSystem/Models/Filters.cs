using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSystem.Models
{
    public class Filters
    {
        public Filters(string filterstring)
        {
            FilterString = filterstring ?? "all-all";
            string[] filters = FilterString.Split('-');
            SprintId = filters[0];            
            StatusId = filters[1];
        }
        public string FilterString { get; }
        public string SprintId { get; }        
        public string StatusId { get; }

        public bool HasSprint => SprintId.ToLower() != "all";        
        public bool HasStatus => StatusId.ToLower() != "all";
        
    }
}
