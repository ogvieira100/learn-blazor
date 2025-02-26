using System.Reflection.PortableExecutable;

namespace BlazorApp.Client.Models
{

    public class ResponseCustomerPaginated : ResponsePaginated<Customers>
    {
        public ResponseCustomerPaginated(IEnumerable<Customers> itens,
            Pagination pagination) : base(itens, pagination, new List<string>
            {
                    "Nome",
                    "Email" ,
                    "Telefone",
                    "SobreNome",
                    "CPF"
            })
        {


        }

        protected override List<Func<Customers, object>> GetListFormated()
        {
            return new List<Func<Customers, object>>
            {
                (item) => item.Name,
                (item) => item.Email,
                (item) => item.Telphone,
                (item) => item.SurName,
                (item) => item.CPF
            };
        }
    }
    public abstract class ResponsePaginated<T> where T : class
    {

        public required IEnumerable<T> List { get; set; }
        public required IEnumerable<string> Headers { get; set; }
        public required Pagination Pagination { get; set; }
        protected abstract List<Func<T, object>> GetListFormated();
        public List<Func<T, object>>? ColumnsTemplates { get; protected set; }


        protected ResponsePaginated(IEnumerable<T> itens,
            Pagination pagination, IEnumerable<string> headers) : base()
        {
            Headers = headers;
            List = itens;
            Pagination = pagination;
            ColumnsTemplates = GetListFormated();
        }
    }
}
