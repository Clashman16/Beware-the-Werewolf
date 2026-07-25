using BWW.ScriptableObjects.Save;
using UnityEngine;

namespace BWW.Managers.Save
{
    public sealed class SaveManager
    {
        private const string m_sSaveKey = "BWW.";

        private const string m_sSavePath = "ScriptableObjects/Save/Save";

        private static SaveManager m_instance;

        public static SaveManager Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new SaveManager();
                }

                return m_instance;
            }
        }

        ScriptableSave m_save;

        public ScriptableSave Save
        {
            get => m_save;
        }

        private SaveManager()
        {
            m_save = Resources.Load<ScriptableSave>(m_sSavePath);

            LoadSave();
        }

        public void SavePseudo()
        {
            PlayerPrefs.SetString(m_sSaveKey + "Pseudo", m_save.Pseudo);
        }

        private void LoadPseudo()
        {
            m_save.Pseudo = PlayerPrefs.GetString(m_sSaveKey + "Pseudo", "username");
        }

        public void SaveCurrentLevelId()
        {
            PlayerPrefs.SetInt(m_sSaveKey + "CurrentLevelId", m_save.CurrentLevelId);
        }

        private void LoadCurrentLevelId()
        {
            m_save.CurrentLevelId = PlayerPrefs.GetInt(m_sSaveKey + "CurrentLevelId", 0);
        }

        private void LoadSave()
        {
            LoadPseudo();

            LoadCurrentLevelId();
        }
    }
}
