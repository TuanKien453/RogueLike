using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactManager : MonoBehaviour
{
    private List<IArtifact> equippedArtifacts = new();
    public IReadOnlyList<IArtifact> EquippedArtifacts => equippedArtifacts.AsReadOnly();
    public bool AddArtifact<T>(BaseArtifactSO<T> artifact) where T:IEventType
    {
        if (!equippedArtifacts.Contains(artifact))
        {
            var instance = Instantiate(artifact);
            equippedArtifacts.Add(instance);
            artifact.OnEquip(gameObject);
            EventManager.AddListener<T>(instance);
            return true;
        }

        return false;
    }
    public bool RemoveArtifact<T>(IArtifact artifact) where T : IEventType
    {
        if (artifact is BaseArtifactSO<T> typedArtifact)
        {
            return RemoveArtifact<T>(typedArtifact.Id);
        }
        return false;
    }

    public bool RemoveArtifact<T>(string id) where T : IEventType
    {
        int index = equippedArtifacts.FindIndex(a => a is BaseArtifactSO<T> ba && ba.Id == id);
        if (index != -1)
        {
            var instance = (BaseArtifactSO<T>)equippedArtifacts[index];
            equippedArtifacts.RemoveAt(index);

            EventManager.RemoveListener(instance);
            instance.OnUnEquip(gameObject);
            return true;
        }
        return false;
    }

}
