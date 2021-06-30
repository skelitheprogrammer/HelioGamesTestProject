using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "State Machine/States/Create Wander State", fileName = "New Wander State")]
public class WanderState : State
{
    [SerializeField] private float _pointOfInterestRadius = 3f;

    private Vector3 _pointPosition = Vector3.zero;
    
    public override void EnterState()
    {
        npcAI.OnDestinationReached += PickPoint;

        PickPoint();
    }

    public override void ExitState()
    {
        npcAI.OnDestinationReached -= PickPoint;
    }

    public override void UpdateState()
    {
        Wander();
    }

    private void Wander()
    {
        npcAI.MoveTo(_pointPosition);
    }

    private void PickPoint() 
    {
        _pointPosition = RandomNavmeshLocation(_pointOfInterestRadius);
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += npcAI.transform.position;

        Vector3 finalPosition = Vector3.zero;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, radius, 1))
        {
            finalPosition = hit.position;
        }

        return finalPosition;
    }
}
