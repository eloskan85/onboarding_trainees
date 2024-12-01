using UnityEngine;

public class QuestUI : QuestAsset
{
    [SerializeField]
    private bool _activateOnQuestBegin = false;

    [SerializeField]
    private bool _activateOnQuestEnd = false;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public override void ResetAsset()
    {
    }

    public override void TriggerQuestBegin()
    {
        if (_activateOnQuestBegin)
        {
            gameObject.SetActive(true);
        }
    }

    public override void TriggerQuestComplete()
    {
        if (_activateOnQuestEnd)
        {
            gameObject.SetActive(true);
        }
    }
}
