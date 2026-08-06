using System.ComponentModel;

namespace BillMax.API
{
    public class ResJsonOutput
    {
        public ResJsonOutput()
        {
            //Header = new Header();
            Data = new object();
            Status = new ResStatus();
        }
        //public Header Header { get; set; }        
        public object Data { get; set; }
        public ResStatus Status { get; set; }

        public void Add(object data)
        {
            throw new NotImplementedException();
        }
        public string get(string v)
        {
            throw new NotImplementedException();
        }

    }
    public class ResStatus
    {
        [DefaultValue(false)]
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string StatusCode { get; set; }
    }
}
