using System;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    [SerializeField]
    private List<QuestObjective> _questObjectives;

    [SerializeField]
    private List<QuestAsset> _questAssets;

    public event Action OnQuestComplete;

    public void StartQuest()
    {
        InitializeQuest();
        BeginQuest();
    }

    private void BeginQuest()
    {
        Debug.Log($"Beginning Quest '{name}'");
        foreach (var asset in _questAssets)
        {
            asset.TriggerQuestBegin();
        }

        foreach (var objective in _questObjectives)
        {
            objective.BeginObjective();
        }

        if (IsQuestDone())
        {
            EndQuest();
        }
    }

    private void EndQuest()
    {
        foreach (var asset in _questAssets)
        {
            asset.TriggerQuestComplete();
        }

        Debug.Log($"Completed Quest '{name}'");
        OnQuestComplete?.Invoke();
    }

    private void InitializeQuest()
    {
        foreach (var asset in _questAssets)
        {
            asset.ResetAsset();
        }

        foreach (var objective in _questObjectives)
        {
            objective.ResetObjective();
            if (!objective.IsDone())
            {
                objective.OnIsDone += QuestObjectiveIsDone;
            }
        }
    }

    private void QuestObjectiveIsDone(QuestObjective objective)
    {
        objective.OnIsDone -= QuestObjectiveIsDone;

        if (IsQuestDone())
        {
            EndQuest();
        }
    }

    private bool IsQuestDone()
    {
        foreach (var objective in _questObjectives)
        {
            if (!objective.IsDone())
            {
                return false;
            }
        }

        return true;
    }
}
