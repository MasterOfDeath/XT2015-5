namespace Employees.PL.Web.Models
{
    public class AjaxResponse
    {
        public AjaxResponse(string answer)
            :this(answer, null)
        {
            this.Answer = answer;
        }

        public AjaxResponse(string answer, object data)
        {
            this.Answer = answer;
            this.Data = data;
        }

        public string Answer { get; }

        public object Data { get; }
    }
}