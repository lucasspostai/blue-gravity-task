using System.Collections.Generic;
using Items;
using UnityEngine;

namespace Characters
{
    [CreateAssetMenu(fileName = "Character Properties", menuName = "Blue Gravity/Character Properties")]
    public class CharacterProperties : ScriptableObject
    {
        [Header("Locomotion")]
        public float WalkSpeed = 10f;

        [Header("Starting Items")] 
        public Item EquippedHat;
        public Item EquippedClothes;
        public List<Item> InventoryItems;
        public int Coins;
    }
}
