using UnityEngine;

namespace BWW.Behaviours.Map
{
    public class TowerBehaviour : GateBehaviour
    {
        [SerializeField] private int m_dTowerId;

        public int TowerId
        {
            get => m_dTowerId;
        }
    }
}
