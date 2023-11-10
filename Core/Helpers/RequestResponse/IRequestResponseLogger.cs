namespace Core.Helpers.RequestResponse
{
    public interface IRequestResponseLogger
    {
        void Log(IRequestResponseLogModelCreator logCreator);
    }
}
