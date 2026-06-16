using BWW.Behaviours.Map.Items;
using BWW.Managers.Map;
using UnityEngine;

namespace BWW.Behaviours.Characters
{
   public class WerewolfCollisionBehaviour : CharacterCollisionBehaviour
   {
        public override void OnTriggerEnter(Collider p_collider)
        {
            MovableItem l_item = p_collider.GetComponent<MovableItem>();

            if (l_item != null)
            {
                CharacterDataBehaviour l_data = GetComponent<CharacterDataBehaviour>();
                if (l_data.State == Enums.ECharacterState.PUSHED)
                {
                    // The villager must take damages according to his speed for the distance between the place where he was hurt and the place where he is now
                }
            }
        }

        public void OnTriggerExit(Collider p_collider)
        {
            MovableItem l_item = p_collider.GetComponent<MovableItem>();

            if (l_item != null)
            {
                NavMeshManager.Instance.HandleFlag(p_collider.gameObject);
            }
        }
    }
}
