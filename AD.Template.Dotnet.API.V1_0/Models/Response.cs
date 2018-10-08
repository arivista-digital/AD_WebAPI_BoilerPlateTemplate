namespace AD.Template.Dotnet.API.V1_0.Models
{
    public class Response
    {
        public string Status { get; set; }
        public string Desc { get; set; }

        public Response()
        {
        }

        public Response(bool flag, string des)
        {
            if (flag)
            {
                Status = "Success";
                Desc = des;
            }
            else
            {
                Status = "Failure";
                Desc = des;
            }
        }
    }
}