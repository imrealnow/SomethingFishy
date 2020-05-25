using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameManager", menuName = "SO/Managers/GameManager", order = 1)]
public class GameManager : SManager
{
    public List<Condition> winConditions = new List<Condition>();
    public List<Condition> loseConditions = new List<Condition>();
    public UnityEvent OnWin, OnLose;

    public override void Update()
    {
        if (Condition.CheckList(winConditions) && OnWin != null)
            OnWin.Invoke();
        if (Condition.CheckList(loseConditions) && OnLose != null)
            OnLose.Invoke();
    }
}
