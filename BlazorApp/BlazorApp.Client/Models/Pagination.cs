namespace BlazorApp.Client.Models
{
    public class Pagination
    {
        public int Page { get; set; }
        public int QtdItens { get; set; }
        public int Length { get; set; }
        public int QtdPages
        {
            get
            {
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
