using BWW.Behaviours.Map;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace BWW.Behaviours.Characters
{
    public class VillagerMovementBehaviour : CharacterMovementBehaviour
    {
        public Vector3 HitPosition => transform.position + Vector3.up * 0.5f;

        internal override IEnumerator LaunchNavMeshAgent()
        {
            Agent = GetComponent<NavMeshAgent>();

            Target = new Vector3(18.0540009f, 0, -34.9550018f);

            Agent.enabled = false;

            NavMeshHit l_hit;

            while (!NavMesh.SamplePosition(transform.position, out l_hit, 10, NavMesh.AllAreas))
            {
                yield return null;
            }

            transform.position = l_hit.position;
            Agent.enabled = true;

            PreviousPosition = transform.position;

            PrepareToMove();
        }

        internal override void Update()
        {
            GateAnimationBehaviour[] l_lstAllGates = FindObjectsByType<GateAnimationBehaviour>(FindObjectsSortMode.None);

            int l_dGateCount = 0;

            while (l_dGateCount < l_lstAllGates.Length)
            {
                GateAnimationBehaviour l_gate = l_lstAllGates[l_dGateCount];

                if (!l_gate.IsOpen && Vector3.Distance(l_gate.transform.position, HitPosition) <= 1)
                {
                    Vector3 l_vecDirection = (l_gate.transform.position - HitPosition).normalized;
                    Vector3 l_vecForward = transform.forward;

                    float l_fDot = Vector3.Dot(l_vecForward, l_vecDirection);

                    if (l_fDot > 0f)
                    {
                        l_dGateCount = l_lstAllGates.Length;

                        Agent.ResetPath();

                        l_gate.GatePosition = l_gate.transform.position;

                        l_gate.VillagerBehindGate = this;
                    }
                }

                l_dGateCount += 1;
            }

            base.Update();
        }

        private void PrepareToMove()
        {
            Path = new NavMeshPath();

            if (Agent.CalculatePath(Target, Path))
            {
                Vector3 l_vecStartPosition = HitPosition;

                Vector3 l_vecNextPosition = Path.corners[1];

                Vector3 l_vecDirection = (l_vecNextPosition - l_vecStartPosition).normalized;

                l_vecDirection.y = 0f;

                if (l_vecDirection != Vector3.zero)
                {
                    transform.rotation =
                        Quaternion.LookRotation(
                            l_vecDirection,
                            Vector3.up
                        );
                }

                float l_fDistance = Vector3.Distance(l_vecNextPosition, l_vecStartPosition);

                int l_dLayerMaskId = LayerMask.GetMask("CastleGate");

                if (Physics.Raycast(l_vecStartPosition, l_vecDirection, out RaycastHit l_hit, l_fDistance, l_dLayerMaskId))
                {
                    if (l_hit.collider != null)
                    {
                        GateAnimationBehaviour l_gateAnimation = l_hit.collider.GetComponent<GateAnimationBehaviour>();

                        if (l_gateAnimation != null)
                        {
                            l_gateAnimation.GatePosition = l_hit.point;

                            l_gateAnimation.VillagerBehindGate = this;
                        }
                    }
                }
            }
        }
    }
}
