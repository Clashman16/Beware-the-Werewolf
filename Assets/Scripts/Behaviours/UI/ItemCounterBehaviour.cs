using UnityEngine;

namespace BWW.Behaviours.UI
{
   public class ItemCounterBehaviour : MonoBehaviour
   {
      public void Disappear()
      {
         gameObject.SetActive(false);
      }
   }
}
