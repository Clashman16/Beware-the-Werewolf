using System.Collections.Generic;
using UnityEngine;

namespace BWW.ScriptableObjects.Map
{
    [CreateAssetMenu(fileName = "ScriptableLevelList", menuName = "BWW/ScriptableObjects/ScriptableLevelList")]
    public class ScriptableLevelList : ScriptableObject
    {
        [SerializeField] private List<ScriptableLevelConfiguration> m_lstAllPossibleParts;
    }
}
