using UnityEngine;

public abstract class State : ScriptableObject
{
    [HideInInspector] public NPCAI npcAI;

    public abstract void EnterState();

    public virtual void ExitState() {}

    public abstract void UpdateState();
}