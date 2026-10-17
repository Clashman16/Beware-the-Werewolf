using BWW.Behaviours.Characters;
using BWW.Enums;
using BWW.Utils;
using BWW.Utils.Characters;
using UnityEngine;

namespace BWW.Managers.Map
{
    public sealed class PrisonerSpawnManager
    {
        private VillagerGenderPickerUtility m_prisonerGenderPicker;

        private PickingUtility m_textureIdPicker;

        private static PrisonerSpawnManager m_instance;

        public static PrisonerSpawnManager Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new PrisonerSpawnManager();
                }

                return m_instance;
            }
        }

        private PrisonerSpawnManager()
        {
            m_prisonerGenderPicker = new VillagerGenderPickerUtility(true);

            m_textureIdPicker = new PickingUtility();

            m_textureIdPicker.PossiblePicks.AddRange(new[] { 0, 1, 2, 3 });
        }

        public void SpawnPrisoner(EWerewolfType p_eWerewolfType)
        {
            bool l_bIsCharacterFemale = m_prisonerGenderPicker.Pick() == 1;

            GameObject l_goPrisoner = Object.Instantiate(m_prisonerGenderPicker.CurrentGender);

            PrisonerAppearanceBehaviour l_prisoner = l_goPrisoner.GetComponent<PrisonerAppearanceBehaviour>();

            l_prisoner.TextureId = m_textureIdPicker.Pick();

            l_prisoner.UpdateAppearance(l_bIsCharacterFemale);

            WerewolfDataBehaviour l_data = l_goPrisoner.GetComponent<WerewolfDataBehaviour>();

            l_data.Init(p_eWerewolfType);
        }
    }
}
