using UnityEngine;

namespace BWW.Behaviours.Characters
{
   public abstract class CharacterCollisionBehaviour : MonoBehaviour
   {
      public abstract void OnTriggerEnter(Collider p_collider);
   }
}
