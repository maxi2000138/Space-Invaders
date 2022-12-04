using Infrastructure.Services;

public interface ILevelStaticDataService : IService
{
    void LoadLevels();
    LevelStaticData GiveLevel(int levelNum);
}