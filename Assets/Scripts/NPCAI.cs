using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NPCAI : MonoBehaviour
{
    private NavMeshAgent _agent;
    private ObjectPool _objectPool;
    
    [Space(15f)]
    [SerializeField] private int luckMin;
    [SerializeField] private int luckMax;

    private int _luck;
    public int Luck => _luck;

    [Space(15f)]
    [SerializeField] private float speedMin;
    [SerializeField] private float speedMax;

    private float speed;

    [Space(15f)]
    [SerializeField] private State _initState;

    private State _currentState;

    public State _idleState;
    public State _wanderState;

    public event Action OnDestinationReached;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _objectPool = FindObjectOfType<ObjectPool>();
    }

    private void OnEnable()
    {
        ConfigureAI();
        ChangeState(_initState);
        OnDestinationReached += ChanceToStay;
    }

    private void OnDisable()
    {
        OnDestinationReached -= ChanceToStay;
    }

    public void Update()
    {
        _currentState.UpdateState();
    }

    private void ConfigureAI()
    {
        
        _luck = Random.Range(luckMin, luckMax);
        speed = Random.Range(speedMin, speedMax);

        _agent.speed = speed;
    }

    public void Die()
    {
        _objectPool.ReturnObject(gameObject);
    }

    public void MoveTo(Vector3 destination)
    {
        _agent.SetDestination(destination);

        if (CheckDestination())
        {
            OnDestinationReached?.Invoke();
        }
    }

    public void ChangeState(State newState)
    {
        _currentState?.ExitState();

        _currentState = Instantiate(newState);
        _currentState.npcAI = this;

        _currentState?.EnterState();
    }

    private bool CheckDestination()
    {
        if (!_agent.pathPending)
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void ChanceToStay()
    {
        float randomNumber = Random.value;

        if (randomNumber >= .5f)
        {
            ChangeState(_idleState);
        }
    }


}
