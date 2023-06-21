using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SecondaryEvent : MonoBehaviour
{
    public void Initialize(List<Transform> interactionsList)
    {
        gameObject.SetActive(false);
        interactionsList.Add(transform);
    }
}
