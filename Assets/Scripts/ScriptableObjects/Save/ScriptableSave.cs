using UnityEngine;

namespace BWW.ScriptableObjects.Save
{
    [CreateAssetMenu(fileName = "ScriptableSave", menuName = "BWW/ScriptableObjects/ScriptableSave")]
    public class ScriptableSave : ScriptableObject
    {
        [SerializeField] private string m_sPseudo;

        public string Pseudo
        {
            get => m_sPseudo;
            set => m_sPseudo = value;
        }

        [SerializeField] private int m_dCurrentLevelId;

        public int CurrentLevelId
        {
            get => m_dCurrentLevelId;
            set => m_dCurrentLevelId = value;
        }
    }
}