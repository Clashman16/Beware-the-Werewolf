using UnityEngine;

namespace BWW.Behaviours.Map.Items
{
    public class ResourceItem : MonoBehaviour
    {
        [SerializeField] private string m_sID;

        public string ID
        {
            get => m_sID;
        }
    }
}
