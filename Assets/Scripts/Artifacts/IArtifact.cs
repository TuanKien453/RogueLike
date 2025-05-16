using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IArtifact
{
    string Id { get; }
    string ArtifactName { get; }
    Sprite Icon { get; }

    Type EventType { get; }

    void OnEquip(GameObject target);
    void OnUnEquip(GameObject target);
}