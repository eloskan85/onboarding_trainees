using UnityEngine;

public abstract class QuestAsset : MonoBehaviour
{
    public abstract void ResetAsset();

    public abstract void TriggerQuestBegin();

    public abstract void TriggerQuestComplete();
}
