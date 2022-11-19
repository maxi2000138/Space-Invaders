using Infrastructure.Services;

public interface IPersistantProgressService : IService
{
    public PlayerProgress PlayerProgress { get; set; }
}
