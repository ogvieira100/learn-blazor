namespace BlazorApp.Client.Models
{
    public class Customers
    {
        public Customers(string name, string surName)
        {
            Name = name;
            SurName = surName;
        }

        public  string? Name { get; set; }
        public  string? SurName { get; set; }

        public Customers()
        {
            
        }
    }
}
