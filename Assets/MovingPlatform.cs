using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    Vector3 startPos;
    [SerializeField] float speed = 1.0f;
    [SerializeField] float distance = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = startPos + new Vector3(Mathf.Sin(speed * Time.time) * distance, 0, 0);
    }
}
