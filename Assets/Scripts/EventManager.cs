using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The EventManager class.
/// Instantiated lazily.
/// Events to be triggered require implementing the IEvent interface.
/// </summary>
public class EventManager
{
    private static readonly Lazy<EventManager> instance
      = new Lazy<EventManager>(() => new EventManager());

    private EventManager() { }

    public static EventManager Instance => instance.Value;

    private Dictionary<Type, Action<IEvent>> listeners = new Dictionary<Type, Action<IEvent>>();
    private Dictionary<Delegate, Action<IEvent>> lookup = new Dictionary<Delegate, Action<IEvent>>();

    /// <summary>
    /// Adds a listener and stores it in a dictionary.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="action"></param>
    public void AddListener<T>(Action<T> action) where T : IEvent
    {
        if (action == null)
        {
            return;
        }

        if (lookup.ContainsKey(action))
        {
            return;
        }

        Action<IEvent> callback = (e) => action((T)e);
        lookup.Add(action, callback);

        Type type = typeof(T);
        if (listeners.TryGetValue(type, out Action<IEvent> existingAction))
        {
            listeners[type] = existingAction += callback;
            return;
        }

        listeners[type] = callback;
    }

    /// <summary>
    /// Removes any listener stored in the dictionary.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="action"></param>
    public void RemoveListener<T>(Action<T> action) where T : IEvent
    {
        if (action == null)
        {
            return;
        }

        if (lookup.TryGetValue(action, out Action<IEvent> callback))
        {
            Type type = typeof(T);
            if (listeners.TryGetValue(type, out Action<IEvent> existingAction))
            {
                existingAction -= callback;
                if (existingAction == null)
                {
                    listeners.Remove(type);
                }
                else
                {
                    listeners[type] = existingAction;
                }
            }

            lookup.Remove(action);
            return;
        }
    }

    /// <summary>
    /// Triggers an event of type IEvent immediately.
    /// </summary>
    /// <param name="evt"></param>
    public void TriggerEvent(IEvent evt)
    {
        Type type = evt.GetType();
        if (listeners.TryGetValue(type, out Action<IEvent> action))
        {
            action.Invoke(evt);
            return;
        }

        Debug.LogWarning($"Event has no event listeners {type}");
    }
}