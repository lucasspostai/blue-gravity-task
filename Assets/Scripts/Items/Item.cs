using UnityEngine;
using UnityEngine.U2D.Animation;

namespace Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Blue Gravity/Item")]
    public class Item : ScriptableObject
    {
        public Sprite Icon;
        public SpriteLibraryAsset LibraryAsset;
        public ItemType Type;
        public int Price;
    }
}
