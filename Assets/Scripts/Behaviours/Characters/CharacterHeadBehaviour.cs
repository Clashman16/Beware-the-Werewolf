using UnityEngine;

namespace BWW.Behaviours.Characters
{
    public class CharacterHeadBehaviour : MonoBehaviour
    {
        public Vector3 HealthBarPosition => transform.position + Vector3.up * 0.25f;
    }
}
