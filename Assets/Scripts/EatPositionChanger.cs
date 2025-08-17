using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EatPositionChanger : MonoBehaviour, IEatDestroyer
{
    [SerializeField] private Transform leftUpBound;
    [SerializeField] private Transform rightDownBound;

    public void Destroy()
    {
        GameManager.Instance.AddScore(1);
        GeneratePosition();
    }

    private const int tryCounts = 1000;

    private void GeneratePosition()
    {
        for (int i = 0; i < tryCounts; i++)
        {
            var newPos = new Vector3(
                Random.Range(leftUpBound.position.x, rightDownBound.position.x),
                Random.Range(leftUpBound.position.y, rightDownBound.position.y));

            if (!IsValidPosition(newPos))
                continue;

            transform.position = newPos;
            break;
        }
    }

    private bool IsValidPosition(Vector3 pos)
    {
        var direction = Camera.main.transform.position - pos;
        return !Physics.Raycast(Camera.main.transform.position, direction, 100);
    }
}