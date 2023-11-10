using Newtonsoft.Json;

namespace Core.Helpers.RequestResponse
{
    public class RequestResponseLogModelCreator : IRequestResponseLogModelCreator
    {
        public RequestResponseLogModel LogModel { get; private set; }
        public RequestResponseLogModelCreator()
        {
            LogModel = new RequestResponseLogModel();
        }

        public string LogString()
        {
            var jsonString = JsonConvert.SerializeObject(LogModel);
            return jsonString;
        }
    }
}
