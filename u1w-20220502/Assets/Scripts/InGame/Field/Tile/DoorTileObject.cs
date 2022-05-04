using UnityEngine;

namespace InGame.Field
{
    public class DoorTileObject : TileObject
    {
        protected override void OnPlayerStepOnTheTile()
        {
            if (!Tile.isNeedKey) return;
            
            // 鍵を使用
            tile.isNeedKey = false;
            playerManager.OnUsedKey();
        }
    }
}