using UnityEngine;

namespace InGame.Field
{
    public class KeyTileObject : TileObject
    {
        protected override void OnPlayerStepOnTheTile()
        {
            if (!Tile.isGettableKey) return;
            if (!playerManager.IsBottomGoldPanel) return;
            
            // 鍵を入手
            tile.isGettableKey = false;
            playerManager.OnGotKey();
        }
    }
}