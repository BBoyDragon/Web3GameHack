using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCellObject : MonoBehaviour
{
    [SerializeField] GameObject topWall;
    [SerializeField] GameObject bottomWall;
    [SerializeField] GameObject leftWall;
    [SerializeField] GameObject rightWall;

    public void Init(bool top, bool bottom, bool left, bool right, float scale)
    {
        topWall.SetActive(top);
        bottomWall.SetActive(bottom);
        leftWall.SetActive(left);
        rightWall.SetActive(right);
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
