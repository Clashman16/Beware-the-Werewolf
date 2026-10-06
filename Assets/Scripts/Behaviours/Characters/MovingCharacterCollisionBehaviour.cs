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
        private UnityAction m_OnCharacterPushed;

        private CharacterDirectionPicker m_directionPicker;

        private CharacterMovementBehaviour m_movement;

        private bool m_bIsPushed;

        private Vector3 m_vecPushDestination;

        private Vector3 m_vecPushDirection;

        private float m_fPushSpeed;

        private CharacterHealthBarBehaviour m_healthBar;

        public CharacterHealthBarBehaviour HealthBar
        {
            set => m_healthBar = value;
        }

        [SerializeField] private LayerMask m_obstacleLayerMask;

        public void Start()
        {
            m_OnCharacterPushed += OnCharacterPushed;

            m_fPushSpeed = 1.5f;
        }

        public void Update()
        {
            if(m_bIsPushed)
            {
                transform.position = Vector3.MoveTowards( transform.position, m_vecPushDestination, m_fPushSpeed * Time.deltaTime);;

                if(transform.position == m_vecPushDestination)
                {
                    m_bIsPushed = false;

                    GetComponent<CharacterDataBehaviour>().State = Enums.ECharacterState.IDDLE;

                    m_movement.ResumeMove();
                }
            }
        }

        private void OnCharacterPushed()
        {
            if(m_movement == null)
            {
                m_movement = GetComponent<CharacterMovementBehaviour>();
            }

            if (m_directionPicker == null)
            {
                m_directionPicker = new CharacterDirectionPicker(m_movement.Agent, m_obstacleLayerMask);
            }

            (Vector3, float) l_pick = m_directionPicker.PickDestination();

            m_vecPushDirection = l_pick.Item1;

            m_vecPushDestination = transform.position + m_vecPushDirection * l_pick.Item2;

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
                    // The villager must take damages according to his speed for the distance between the place where he was hurt and the place where he is now
                }
                else
                {
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
