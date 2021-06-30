using SkilliTools.Timers;
using UnityEngine;

[CreateAssetMenu(menuName = "State Machine/States/Create Idle State", fileName = "New Idle State")]
public class IdleState : State
{
    [SerializeField] private float _waitTime;
    
    private Timer _timer;

    public override void EnterState()
    {
        _timer = new Timer(_waitTime);
        _timer.OnTimerEnd += () => npcAI.ChangeState(npcAI._wanderState);
    }
    public override void UpdateState()
    {
        _timer.Tick(Time.deltaTime);
    }

    public override void ExitState()
    {
        _timer.OnTimerEnd -= () => npcAI.ChangeState(npcAI._wanderState);
        _timer = null;
    }


}
