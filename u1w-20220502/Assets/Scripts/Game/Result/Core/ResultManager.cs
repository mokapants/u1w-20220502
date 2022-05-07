using System;
using Repositories.Result.Core;
using UnityEngine;
using VContainer;

namespace Game.Result.Core
{
    public class ResultManager : MonoBehaviour
    {
        private ResultRepository resultRepository;

        [Inject]
        public void Constructor(
            ResultRepository resultRepository
        )
        {
            this.resultRepository = resultRepository;
        }

        private void Start()
        {
            naichilab.RankingLoader.Instance.SendScoreAndShowRanking(resultRepository.LoadScore());
        }
    }
}