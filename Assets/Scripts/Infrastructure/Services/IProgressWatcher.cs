namespace Infrastructure.Services
{
    public interface IProgressWatcher : IProgressReader
    {
        void SaveProgress(PlayerProgress progress);
    }
}