using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsVisualizer : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Transform heartGrid;

    public void UpdateHeartGrid(int heartsAmount)
    {
        foreach (Transform child in heartGrid.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < heartsAmount; i++)
        {
            Transform heartObject = Instantiate(heartPrefab, heartGrid.position, Quaternion.identity).transform;
            heartObject.parent = heartGrid;
        }
    }
}