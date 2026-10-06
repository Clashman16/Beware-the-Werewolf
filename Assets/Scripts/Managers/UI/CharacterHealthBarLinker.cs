using BWW.Behaviours.Characters;
using BWW.Behaviours.UI;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace BWW.Managers.UI
{
    public sealed class CharacterHealthBarLinker
    {
        private Queue<CharacterHealthBarBehaviour> m_lstHealthBarPool;

        private static CharacterHealthBarLinker m_instance;

        public static CharacterHealthBarLinker Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new CharacterHealthBarLinker();
                }
                return m_instance;
            }
        }

        private CharacterHealthBarLinker()
        {
            m_lstHealthBarPool = new Queue<CharacterHealthBarBehaviour>();

            CharacterHealthBarBehaviour[] l_lstHealthBar = Object.FindObjectsByType<CharacterHealthBarBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            foreach (CharacterHealthBarBehaviour l_healthBar in l_lstHealthBar)
            {
                l_healthBar.gameObject.SetActive(true);

                l_healthBar.OnEmptyBar = () =>
                {
                    m_lstHealthBarPool.Enqueue(l_healthBar);

                    l_healthBar.gameObject.SetActive(false);
                };

                l_healthBar.gameObject.SetActive(false);

                m_lstHealthBarPool.Enqueue(l_healthBar);
            }
        }

        public async void LinkBarToCharacterData(CharacterDataBehaviour p_data)
        {
            if (m_lstHealthBarPool.Count > 0)
            {
                CharacterHealthBarBehaviour l_healthBar = m_lstHealthBarPool.Dequeue();

                l_healthBar.gameObject.SetActive(true);

                l_healthBar.Init(p_data);
            }
            else
            {
                CharacterHealthBarBehaviour[] l_lstHealthBar = Object.FindObjectsByType<CharacterHealthBarBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None);

                if (l_lstHealthBar.Length < 5)
                {
                    CharacterHealthBarBehaviour l_barTemplate = l_lstHealthBar[0];

                    GameObject l_barCopy = Object.Instantiate(l_barTemplate.gameObject, l_barTemplate.transform.parent);

                    CharacterHealthBarBehaviour l_healthBar = l_barCopy.GetComponent<CharacterHealthBarBehaviour>();

                    l_healthBar.Init(p_data);
                }
                else
                {
                    await WaitForFeedbackCatcher(p_data);
                }
            }
        }

        private async Task WaitForFeedbackCatcher(CharacterDataBehaviour p_data)
        {
            while (m_lstHealthBarPool.Count == 0)
            {
                await Task.Delay(50);
            }

            LinkBarToCharacterData(p_data);
        }
    }
}
