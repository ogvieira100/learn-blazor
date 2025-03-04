using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAppTreino.Domain.Models
{
   public class Pagination
    {

        public int Page { get; set; }

        public int Length { get; set; }

        public long QtdItens { get; set; }

        public int QtdPages
        {
            get
            {
                if (QtdItens <= 0)
                    return 0;
                return (int)System.Math.Ceiling((decimal)QtdItens / Length);
            }
        }
        public Pagination()
           
        {
            Page = 1;
            Length = 10;

        }
    }
}
