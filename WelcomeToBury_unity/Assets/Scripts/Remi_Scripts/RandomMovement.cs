using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{

    Vector2 random;

    // Start is called before the first frame update
    void Start()
    {
        random = new Vector2(Random.Range(0f, 500f), Random.Range(500f, 1000f));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(-2.5f + Mathf.PerlinNoise(Time.time + random.x, 0.5f) * 5f, transform.position.y, -2.5f + Mathf.PerlinNoise(Time.time + random.y, 0.5f) * 5f);
    }
}
