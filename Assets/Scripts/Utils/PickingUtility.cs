using System.Collections.Generic;
using UnityEngine;

namespace BWW.Utils
{
   public class PickingUtility
   {
      private List<int> m_lstPossiblePicks;

      public List<int> PossiblePicks
      {
         get => m_lstPossiblePicks;
      }

      private int m_dLastPickedId;

      public int LastPickedId
      {
         get => m_dLastPickedId;
         set => m_dLastPickedId = value;
      }

      public PickingUtility()
      {
         m_lstPossiblePicks = new List<int>();
      }

      public int GetRandomPickId()
      {
         return Random.Range(0, m_lstPossiblePicks.Count);
      }

      public virtual int Pick()
      {
         if (m_dLastPickedId == -1)
         {
            m_dLastPickedId = GetRandomPickId();
         }

         int l_dRandomId = GetRandomPickId();

         if (m_dLastPickedId == l_dRandomId)
         {
            if (MathUtils.HeadsOrTails())
            {
               l_dRandomId = GetRandomPickId();
            }
         }

         return l_dRandomId;
      }
   }
}
