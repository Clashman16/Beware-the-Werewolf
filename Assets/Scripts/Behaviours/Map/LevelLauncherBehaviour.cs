using BWW.Managers.Map;
using BWW.Managers.Save;
using BWW.ScriptableObjects.Map;
using UnityEngine;

namespace BWW.Behaviours.Map
{
    public class LevelLauncherBehaviour : MonoBehaviour
    {
        [SerializeField] private ScriptableLevelDatabase m_database;

        private LevelBuilderManager m_levelBuilder;

        private void Start()
        {
            LaunchCurrentLevel();
        }

        public void LaunchCurrentLevel()
        {
            int l_dCurrentLevelId = SaveManager.Instance.Save.CurrentLevelId;

            ScriptableLevelConfiguration l_levelConfig = m_database.GetDataById(l_dCurrentLevelId);

            BuildCurrentLevel(l_levelConfig);
        }

        private void BuildCurrentLevel(ScriptableLevelConfiguration p_levelConfig)
        {
            if(m_levelBuilder == null)
            {
                m_levelBuilder = new LevelBuilderManager(p_levelConfig);
            }
            else
            {
                m_levelBuilder.BuildLevel(p_levelConfig);
            }
        }
    }
}
