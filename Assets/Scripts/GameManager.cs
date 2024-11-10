using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<Quest> _quests;

    private int _currentQuestIndex = 0;
    private Quest _currentQuest;

    public bool IsGameFinished => _currentQuestIndex == _quests.Count;

    private void Start()
    {
        if (IsGameFinished)
        {
            return;
        }

        BeginGame();
    }

    private void BeginGame()
    {
        _currentQuest = _quests[_currentQuestIndex];
        _currentQuest.OnQuestComplete += OnCurrentQuestComplete;
        _currentQuest.StartQuest();
    }

    private void OnCurrentQuestComplete()
    {
        _currentQuest.OnQuestComplete -= OnCurrentQuestComplete;
        _currentQuestIndex++;
        Debug.Log($"Quest {_currentQuestIndex} is completed!");

        if (IsGameFinished)
        {
            return;
        }

        _currentQuest = _quests[_currentQuestIndex];
        if (_currentQuest == null)
        {
            throw new NullReferenceException($"{name}: quests at index {_currentQuestIndex} is null. You should set the quest.");
        }

        _currentQuest.OnQuestComplete += OnCurrentQuestComplete;
        _currentQuest.StartQuest();
    }
}
