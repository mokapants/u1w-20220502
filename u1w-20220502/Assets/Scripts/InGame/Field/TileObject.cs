using Data.ValueObjects.Field;
using UnityEngine;
using UnityEngine.UI;

namespace InGame.Field
{
    public class TileObject : MonoBehaviour
    {
        [SerializeField] private Text pointText;
        private int x;
        private int z;
        private Tile tile;

        public void Init(int x, int z, Tile tile)
        {
            this.x = x;
            this.z = z;
            this.tile = tile;

            pointText.text = tile.Point.ToString();
        }
    }
}