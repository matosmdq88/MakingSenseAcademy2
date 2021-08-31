namespace RentallCarsAPI.Models.Response
{
    public class Response
    {
        public bool Succes { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public Response()
        {
            this.Succes = false;
        }
    }
}
