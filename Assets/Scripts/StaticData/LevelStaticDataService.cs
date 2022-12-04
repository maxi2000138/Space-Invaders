using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelStaticDataService : ILevelStaticDataService
{
    private Dictionary<int, LevelStaticData> _levels;

    public void LoadLevels()
    {
        _levels = Resources.LoadAll<LevelStaticData>("StaticData/Levels")
            .ToDictionary(x => x.LevelNumber, x => x);
    }

    public LevelStaticData GiveLevel(int levelNum) => 
        _levels.TryGetValue(levelNum, out LevelStaticData level) 
            ? level 
            : null;
}