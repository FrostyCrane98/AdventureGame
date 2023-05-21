using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoSingleton<MonsterController>
{
    public MonsterCollection Monsters;
    public List<Monster> activeMonsters = new List<Monster>();

    Queue<Monster> MonstersToMove = new Queue<Monster>();
    Monster currentMonster = null;

    public Monster AddMonster(MonsterPrototype.eMonsterID _id, int _level)
    {
        Monster newMonster = Monsters.GetMonster(_id, _level);
        activeMonsters.Add(newMonster);
        return newMonster;
    }

    public void RemoveMonster(Monster _monster)
    {
        activeMonsters.Remove(_monster);
    }

    public void MoveMonsters()
    {
        for (int i = 0; i < activeMonsters.Count; i++)
        {
            if (activeMonsters[i] != null && activeMonsters[i].isActiveAndEnabled)
            {
                MonstersToMove.Enqueue(activeMonsters[i]);
            }
        }
        if (MonstersToMove.Count == 0)
        {
            GameController.Instance.GoToState(GameController.eGameState.PlayerTurn);
        }
    }

    private void Update()
    {
        if (currentMonster != null && currentMonster.ActorState == Actor.eActorState.Idle)
        {
            currentMonster = null;
            if (MonstersToMove.Count == 0)
            {
                GameController.Instance.GoToState(GameController.eGameState.PlayerTurn);
            }
        }

        if (currentMonster == null && MonstersToMove.Count > 0)
        {
            currentMonster = MonstersToMove.Dequeue();
        }

        if (currentMonster != null && currentMonster.ActorState == Actor.eActorState.Idle)
            {
                currentMonster.DecideMove();
            }
    }
}
