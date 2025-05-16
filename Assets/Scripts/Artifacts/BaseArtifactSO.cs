using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseArtifactSO<T> : ScriptableObject, IArtifact, IEventListener<T> where T : IEventType
{
    public string Id;
    public string ArtifactName;
    public Sprite Icon;

    string IArtifact.Id => Id;
    string IArtifact.ArtifactName => ArtifactName;
    Sprite IArtifact.Icon => Icon;
    public Type EventType => typeof(T);

    public abstract void OnEvent(T eventData);

    protected virtual void OnEnable()
    {
        if (string.IsNullOrEmpty(Id))
        {
            Id = System.Guid.NewGuid().ToString();
        }
    }

    public override bool Equals(object obj)
    {
        return obj is BaseArtifactSO<T> artifact && artifact.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public void OnEquip(GameObject target)
    {

    }

    public void OnUnEquip(GameObject target)
    {

    }
}
