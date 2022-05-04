using UnityEngine;

namespace InGame.Field
{
    public class GoalTileObject : TileObject
    {
        protected override void OnPlayerStepOnTheTile()
        {
            gameRepository.OnGoal();
        }
    }
}