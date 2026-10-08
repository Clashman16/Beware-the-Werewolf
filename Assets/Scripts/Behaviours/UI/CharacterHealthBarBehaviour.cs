using BWW.Behaviours.Characters;
using BWW.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BWW.Behaviours.UI
{
    public class CharacterHealthBarBehaviour : MonoBehaviour
    {
        private Slider m_slider;

        private Gradient m_gradient;

        private Transform m_trfTarget;

        private CharacterDataBehaviour m_data;

        private float m_fLastDamageTime;

        private const float m_fTimeBeforeDisappearing = 5f;

        private bool m_bMustDisappear;

        public bool MustDisappear
        {
            set => m_bMustDisappear = value;
        }

        private UnityAction m_onEmptyBar;

        public UnityAction OnEmptyBar
        {
            set => m_onEmptyBar = value;
        }

        public void Init(CharacterDataBehaviour p_data)
        {
            if (m_slider == null)
            {
                m_slider = GetComponent<Slider>();

                m_gradient = new Gradient();

                GradientAlphaKey l_alphaKey = new GradientAlphaKey(1, 0);

                GradientColorKey[] l_lstColorKeys = new GradientColorKey[2];

                Color32 l_color = new Color32(0, 255, 237, 255);

                l_lstColorKeys[0] = new GradientColorKey(l_color, 0);

                l_lstColorKeys[1] = new GradientColorKey(Color.red, 1);

                m_gradient.SetKeys(l_lstColorKeys, new []{ l_alphaKey });
            }

            m_data = p_data;

            m_slider.maxValue = m_data.HealthPoints;

            m_slider.value = m_data.HealthPoints;

            m_trfTarget = m_data.GetComponentInChildren<CharacterHeadBehaviour>().transform;

            m_data.GetComponent<MovingCharacterCollisionBehaviour>().HealthBar = this;

            m_bMustDisappear = false;

            m_fLastDamageTime = Time.time;
        }

        private void UpdateHealthBarColor(Image p_imgBar, bool p_bIsFillColor)
        {
            float l_fCurrentAlpha = p_imgBar.color.a;

            Color l_color = p_bIsFillColor ? m_gradient.Evaluate(1 - m_slider.value / m_slider.maxValue) : p_imgBar.color;

            l_fCurrentAlpha += 0.05f * (m_bMustDisappear ? -1 : 1);

            l_color.a = Mathf.Clamp01(l_fCurrentAlpha);

            p_imgBar.color = l_color;
        }

        private void Update()
        {
            transform.position = MathUtils.GetScreenPosition(m_trfTarget.GetComponent<CharacterHeadBehaviour>().HealthBarPosition);

            if (m_slider.value != m_data.HealthPoints)
            {
                m_slider.value = m_data.HealthPoints;

                m_fLastDamageTime = Time.time;

                m_bMustDisappear = false;
            }
            
            if (!m_bMustDisappear && (m_slider.value <= 0 || Time.time - m_fLastDamageTime >= m_fTimeBeforeDisappearing))
            {
                m_bMustDisappear = true;
            }

            if (m_bMustDisappear)
            {
                Image[] l_lstImages = GetComponentsInChildren<Image>();

                foreach(Image l_img in l_lstImages)
                {
                    UpdateHealthBarColor(l_img, l_img.name == "Fill");
                }

                if (l_lstImages[0].color.a <= 0)
                {
                    if(m_slider.value <= 0)
                    {
                        m_onEmptyBar.Invoke();
                    }
                }
            }
            else
            {
                Image[] l_lstImages = GetComponentsInChildren<Image>();

                if(l_lstImages[0].color.a < 1)
                {
                    foreach (Image l_img in l_lstImages)
                    {
                        UpdateHealthBarColor(l_img, l_img.name == "Fill");
                    }
                }
            }
        }
    }
}
