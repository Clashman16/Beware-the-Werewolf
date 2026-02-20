using BWW.Enums;
using UnityEngine;

namespace BWW.Behaviours.Map
{
   public class GateBehaviour : SpawnPointBehaviour
   {
      public override GameObject InstantiateVillager(EVillagerType p_eEnemyType)
      {
         GameObject l_goVillager = base.InstantiateVillager(p_eEnemyType);

         l_goVillager.transform.SetParent(transform, true);

         l_goVillager.transform.localPosition = Vector3.zero;

         l_goVillager.transform.parent = null;

         return l_goVillager;
      }
   }
}
