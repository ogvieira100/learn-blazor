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

        public int QtdItens { get; set; }

        public int QtdPages
        {
            get
            {
                return (int)System.Math.Ceiling((decimal)QtdItens / Length);
            }
        }
        public Pagination()
            : base()
        {


        }
    }
}
