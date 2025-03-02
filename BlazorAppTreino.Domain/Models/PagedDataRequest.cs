using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAppTreino.Domain.Models
{
    public class PagedDataRequest
    {
        public int Page { get; set; }
        public int Limit { get; set; }
        public bool? Active { get; set; }
        public string? Column { get; set; }
        public bool Desc { get; set; }


        protected PagedDataRequest()
        {

            Limit = 30;

            Page = 1;

        }
    }
}
