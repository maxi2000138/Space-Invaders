namespace Infrastructure.Services
{
    public interface ISaveLoadService : IService
    {
        public void SaveProgress();

        public PlayerProgress LoadProgress();
    }
}
