using BWW.Behaviours.Characters;
using BWW.Enums;
using BWW.Managers.Map;
using BWW.Managers.UI;
using BWW.Utils.Characters;
using System.Collections;
using UnityEngine;

namespace BWW.Behaviours.Map
{
    public class SpawnPointBehaviour : MonoBehaviour
    {
        [SerializeField] private int m_dSpawnerId;

        public int SpawnerId
        {
            get => m_dSpawnerId;
        }

        VillagerGenderPickerUtility m_villagerGenderPicker;

        public VillagerGenderPickerUtility EnemyAppearancePicker
        {
            set => m_villagerGenderPicker = value;
        }

        public virtual GameObject InstantiateVillager(EVillagerType p_eEnemyType, int p_dWalkAnimationId)
        {
            bool l_bIsCharacterFemale = m_villagerGenderPicker.Pick() == 1;

            GameObject l_goVillager = Instantiate(m_villagerGenderPicker.CurrentGender);

            VillagerAppearanceBehaviour l_villager = l_goVillager.GetComponent<VillagerAppearanceBehaviour>();

            l_villager.UpdateAppearance(l_bIsCharacterFemale);

            VillagerDataBehaviour l_data = l_villager.GetComponent<VillagerDataBehaviour>();

            l_data.Init(p_eEnemyType);

            l_villager.GetComponent<VillagerMovementBehaviour>().Init(l_data);

            l_villager.GetComponent<VillagerAnimationBehaviour>().Init(l_data, p_dWalkAnimationId);

            CharacterHealthBarLinker.Instance.LinkBarToCharacterData(l_data);

            //StartCoroutine(LoopSpawn());

            return l_goVillager;
        }

        private IEnumerator LoopSpawn()
        {
            yield return new WaitForSeconds(5);

            VillagersSpawnManager.Instance.Spawn();
        }
    }
}
