using UnityEngine;
using System;

public abstract class QuestObjective : MonoBehaviour
{
    public event Action<QuestObjective> OnIsDone;

    public abstract void ResetObjective();

    public abstract bool IsDone();

    public abstract void BeginObjective();

    protected void TriggerIsDone()
    {
        OnIsDone?.Invoke(this);
    }
}
