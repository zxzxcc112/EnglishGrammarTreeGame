using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListenerwithDelay : GameEventListener
{
    [SerializeField] private int delayMillisecond;
    [SerializeField] private UnityEvent _delayedUnityEvent;

    public override void RaiseEvent()
    {
        base.RaiseEvent();
        RunDelayedEvent();
    }

    private async void RunDelayedEvent()
    {
        await Task.Delay(delayMillisecond);
        _delayedUnityEvent.Invoke();
    }
}
