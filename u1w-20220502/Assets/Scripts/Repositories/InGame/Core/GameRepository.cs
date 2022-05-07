using System;
using Cysharp.Threading.Tasks;
using Data.ScriptableObjects.Stage;
using Data.StaticValue;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Repositories.Core
{
    public class GameRepository
    {
        private int currentStageId = 0;

        // プロパティ
        public int CurrentStageId => currentStageId;

        [Inject]
        public GameRepository(
            StageMasterData stageMasterData
        )
        {
            currentStageId = ES3.Load(SaveDataKey.CurrentStageIdKey, 0);
        }

        /// <summary>
        /// スコアを記録
        /// </summary>
        public void SaveScore(float elapsedTime)
        {
            ES3.Save(SaveDataKey.CurrentScoreKey, elapsedTime);
        }
    }
}