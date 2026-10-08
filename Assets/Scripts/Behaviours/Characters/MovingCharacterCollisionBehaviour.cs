using BWW.Behaviours.Map.Items;
using BWW.Behaviours.UI;
using BWW.Managers.Map;
using BWW.Utils.Characters;
using UnityEngine;
using UnityEngine.Events;

namespace BWW.Behaviours.Characters
{
    public class MovingCharacterCollisionBehaviour : CharacterCollisionBehaviour
    {
        [SerializeField] private LayerMask m_obstacleLayerMask;

        private UnityAction m_OnCharacterPushed;

        private CharacterMovementBehaviour m_movement;

        private bool m_bIsPushed;

        #region Movement after collision

        private CharacterDirectionPicker m_directionPicker;

        private Vector3 m_vecPushDestination;

        private Vector3 m_vecPushDirection;

        private const float m_fPushSpeed = 1.5f;

        #endregion

        #region Collision damages

        private const float m_fDamageRatio = 0.005f;

        private float m_fSampleDuration = 0.05f;

        private float m_fSampleDistance;

        private bool m_bDamagePushActive;

        private float m_fNextDamageDistance;

        private Vector3 m_vecDamagePushStart;

        private Vector3 m_vecDamagePushDirection;

        private float m_fDamagePushDistance;

        private CharacterHealthBarBehaviour m_healthBar;

        public CharacterHealthBarBehaviour HealthBar
        {
            set => m_healthBar = value;
        }

        #endregion

        public void Start()
        {
            m_OnCharacterPushed += OnCharacterPushed;
        }

        public void Update()
        {
            if(m_bIsPushed)
            {
                CharacterDataBehaviour l_data = GetComponent<CharacterDataBehaviour>();

                transform.position = Vector3.MoveTowards(transform.position, m_vecPushDestination, m_fPushSpeed * Time.deltaTime);

                Vector3 l_vecFromFirstCollision = transform.position - m_vecDamagePushStart;

                float l_fFirstPushDistanceTravelled = Vector3.Dot(l_vecFromFirstCollision, m_vecDamagePushDirection);

                l_fFirstPushDistanceTravelled = Mathf.Clamp(l_fFirstPushDistanceTravelled, 0f, m_fDamagePushDistance);

                while (l_fFirstPushDistanceTravelled >= m_fNextDamageDistance)
                {
                    l_data.ApplyDamage(m_fDamageRatio);

                    m_fNextDamageDistance += m_fSampleDistance;
                }

                if (transform.position == m_vecPushDestination)
                {
                    m_bIsPushed = false;

                    l_data.State = Enums.ECharacterState.IDDLE;

                    m_movement.ResumeMove();
                }
            }
        }

        private void OnCharacterPushed()
        {
            if (m_movement == null)
            {
                m_movement = GetComponent<CharacterMovementBehaviour>();
            }
                
            if (m_directionPicker == null)
            {
                m_directionPicker = new CharacterDirectionPicker(m_movement.Agent, m_obstacleLayerMask);
            }
                
            (Vector3, float) l_pick =  m_directionPicker.PickDestination();

            m_vecPushDirection = l_pick.Item1;

            m_vecPushDestination = transform.position + m_vecPushDirection * l_pick.Item2;

            if (m_bDamagePushActive)
            {
                m_vecDamagePushStart = transform.position;

                m_vecDamagePushDirection = m_vecPushDirection;

                m_fDamagePushDistance = l_pick.Item2;

                m_fSampleDistance = m_fPushSpeed * m_fSampleDuration;

                m_fNextDamageDistance = m_fSampleDistance;

                m_bDamagePushActive = false;
            }

            m_bIsPushed = true;

            m_movement.StopMove();
        }

        public override void OnTriggerEnter(Collider p_collider)
        {
            MovableItem l_item = p_collider.GetComponent<MovableItem>();

            if (l_item != null)
            {
                CharacterDataBehaviour l_data = GetComponent<CharacterDataBehaviour>();
                if (l_data.State == Enums.ECharacterState.PUSHED)
                {
                    m_OnCharacterPushed.Invoke();
                }
                else
                {
                    m_bDamagePushActive = true;

                    l_data.State = Enums.ECharacterState.PUSHED;

                    m_healthBar.gameObject.SetActive(true);

                    m_healthBar.MustDisappear = false;

                    m_OnCharacterPushed.Invoke();
                }
            }
        }
        public virtual void OnTriggerExit(Collider p_collider)
        {
            MovableItem l_item = p_collider.GetComponent<MovableItem>();

            if (l_item != null)
            {
                NavMeshManager.Instance.HandleFlag(p_collider.gameObject);
            }
        }
    }
}
