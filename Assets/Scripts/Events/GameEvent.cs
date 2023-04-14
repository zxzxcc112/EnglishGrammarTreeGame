using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObj/GameEvent", fileName = "New Game Event")]
public class GameEvent : ScriptableObject
{
    private HashSet<GameEventListener> gameEventListeners = new HashSet<GameEventListener>();

    public void Invoke()
    {
        foreach(GameEventListener listener in gameEventListeners)
            listener.RaiseEvent();
    }

    public void Register(GameEventListener listener) => gameEventListeners.Add(listener);

    public void Deregister(GameEventListener listener) => gameEventListeners.Remove(listener);
}
