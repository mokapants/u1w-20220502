using System.Collections.Generic;
using UnityEngine;

namespace Data.ScriptableObjects.Stage
{
    [CreateAssetMenu(fileName = "StageMasterData", menuName = "mokapants/StageMasterData", order = 0)]
    public class StageMasterData : ScriptableObject
    {
        [SerializeField] private List<TextAsset> stageDataList;
        
        // プロパティ
        private List<TextAsset> copyStageDataList = new List<TextAsset>();

        public List<TextAsset> StageDataList
        {
            get
            {
                if (copyStageDataList.Count != stageDataList.Count)
                {
                    copyStageDataList = new List<TextAsset>(stageDataList);
                }

                return copyStageDataList;
            }
        }
    }
}