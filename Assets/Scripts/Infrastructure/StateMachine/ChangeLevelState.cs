using System.Collections;
using Infrastructure.Services;
using Infrastructure.States;
using UnityEngine;

namespace Infrastructure.StateMachine
{
    public class ChangeLevelState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly ILevelStaticDataService _levelStaticDataService;
        private int levelNum = 0;
        private Blackout _blackout;

        public ChangeLevelState(GameStateMachine gameStateMachine, IGameFactory gameFactory,
            ILevelStaticDataService levelStaticDataService)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            _levelStaticDataService = levelStaticDataService;
        }

        public void Enter()
        {
            _blackout ??= _gameFactory.Blackout;
            _blackout.OnBlackoutRoutineEnd += LevelChanging;
            _blackout.ShowBlackout();
        }

        public void LevelChanging()
        {
            _gameFactory.CreateEnemiesOnLevel(_levelStaticDataService.GiveLevel(levelNum++));
            _blackout.ShowLight();
            _gameStateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
            _blackout.OnBlackoutRoutineEnd -= LevelChanging;
        }
        
    }
}