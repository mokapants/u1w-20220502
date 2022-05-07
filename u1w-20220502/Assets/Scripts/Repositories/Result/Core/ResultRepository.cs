using Data.StaticValue;
using UnityEngine;

namespace Repositories.Result.Core
{
    public class ResultRepository : MonoBehaviour
    {
        public float LoadScore()
        {
            return ES3.Load(SaveDataKey.CurrentScoreKey, 99999f);
        }
    }
}