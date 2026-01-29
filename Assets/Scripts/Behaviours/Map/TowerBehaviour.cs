using UnityEngine;

namespace BWW.Behaviours.Map
{
    public class TowerBehaviour : MonoBehaviour
    {
        [SerializeField] private int m_dTowerId;

        public int TowerId
        {
            get => m_dTowerId;
        }
    }
}
