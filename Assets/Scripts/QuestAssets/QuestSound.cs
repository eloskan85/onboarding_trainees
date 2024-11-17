using UnityEngine;

public class QuestSound : QuestAsset
{
    [SerializeField]
    private AudioSource _audioSource;

    public override void ResetAsset()
    {
        _audioSource.Stop();
    }

    public override void TriggerQuestBegin()
    {
    }

    public override void TriggerQuestComplete()
    {
        _audioSource.Play();   
    }

    public void SetVolume(float volume)
    {
        _audioSource.volume = volume;
    }
}
