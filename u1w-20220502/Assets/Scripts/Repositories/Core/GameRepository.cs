using System;
using Cysharp.Threading.Tasks;
using Data.ScriptableObjects.Stage;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Repositories.Core
{
    public class GameRepository
    {
        private const string CurrentStageIdKey = "CurrentStageId";

        private readonly StageMasterData stageMasterData;
        private int currentStageId;

        // プロパティ
        public int CurrentStageId => currentStageId;

        [Inject]
        public GameRepository(
            StageMasterData stageMasterData
        )
        {
            this.stageMasterData = stageMasterData;
            currentStageId = ES3.Load(CurrentStageIdKey, 0);
        }

        /// <summary>
        /// ゴール時
        /// </summary>
        public async void OnGoal()
        {
            var nextStageId = currentStageId + 1;
            if (stageMasterData.StageDataList.Count <= nextStageId)
            {
                nextStageId = 0;
            }

            ES3.Save(CurrentStageIdKey, nextStageId);

            await UniTask.Delay(TimeSpan.FromSeconds(1f));

            SceneManager.LoadScene("InGame");
        }
    }
}