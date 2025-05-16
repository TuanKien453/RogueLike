using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    private static Dictionary<Type, List<IEventListenerBase>> _subscribers = new();

    public static void AddListener<EventType>(IEventListener<EventType> listener) where EventType : IEventType
    {
        var type = typeof(EventType);
        if (!_subscribers.TryGetValue(type, out var listeners))
        {
            listeners = new List<IEventListenerBase>();
            _subscribers[type] = listeners;
        }

        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }

    public static void RemoveListener<EventType>(IEventListener<EventType> listener) where EventType : IEventType
    {
        var type = typeof(EventType);
        if (_subscribers.TryGetValue(type, out var listeners))
        {
            listeners.Remove(listener);
        }
    }

    public static void TriggerEvent<EventType>(EventType eventData) where EventType : IEventType
    {
        var type = typeof(EventType);
        if (_subscribers.TryGetValue(type, out var listeners))
        {
            foreach (var listener in listeners)
            {
                if (listener is IEventListener<EventType> typedListener)
                {
                    typedListener.OnEvent(eventData);
                }
            }
        }
    }

    public static void DebugSubscribers()
    {
        foreach (var entry in _subscribers)
        {
            var eventType = entry.Key;
            var listeners = entry.Value;

            Debug.Log($"Event Type: {eventType.Name}, Listeners Count: {listeners.Count}");
            foreach (var listener in listeners)
            {
                Debug.Log($"  Listener: {listener.GetType().Name}");
            }
        }
    }

    public static void Clear()
    {
        _subscribers.Clear();
    }
}
public interface IEventType { }
public interface IEventListenerBase { }
public interface IEventListener<T> : IEventListenerBase where T : IEventType
{
    void OnEvent(T eventData);
}

public abstract class AutoAssignEventListener<T> : MonoBehaviour, IEventListener<T> where T : IEventType
{
    public abstract void OnEvent(T eventData);
    private void OnDestroy()
    {
        EventManager.RemoveListener<T>(this);
    }

    private void OnEnable()
    {
        EventManager.AddListener<T>(this);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<T>(this);
    }
}