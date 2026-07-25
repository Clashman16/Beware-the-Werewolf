using System;
using UnityEngine;

namespace BWW.ScriptableObjects.Items
{
    [Serializable]
    public struct ScriptableInventoryItem
    {
        [SerializeField] private int m_dId;

        public int Id
        {
            get => m_dId;
        }

        [SerializeField] private Sprite m_icon;

        public Sprite Icon
        {
            get => m_icon;
        }

        [SerializeField] private int m_dShopCost;

        public int ShopCost
        {
            get => m_dShopCost;
        }

        [SerializeField] private int m_dShopQuantity;

        public int ShopQuantity
        {
            get => m_dShopQuantity;
        }
    }
}
