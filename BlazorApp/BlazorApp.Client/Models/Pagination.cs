namespace BlazorApp.Client.Models
{

    public  class PaginationRequest {

        public int Page { get; set; }

        public int Length { get; set; }

        public PaginationRequest()
        {
            Page = 1;
            Length = 10;
        }
    }
    public class Pagination: PaginationRequest
    {
        public int QtdItens { get; set; }
        
        public int QtdPages
        {
            get
            {
                return (int)System.Math.Ceiling((decimal)QtdItens / Length);
            }
        }
        public Pagination()
            :base()
        {
            

        }

    }
}
