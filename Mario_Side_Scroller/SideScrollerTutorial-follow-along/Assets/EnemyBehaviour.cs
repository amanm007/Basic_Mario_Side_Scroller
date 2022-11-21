using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : DamagerBehaviour
{
    public Vector3 targetOffset;
    public float speed = 1;

    private Vector3 m_startPosition;

    // Start is called before the first frame update
    void Start()
    {
        m_startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 p0 = m_startPosition;
        Vector3 p1 = p0 + targetOffset;
        transform.position = Vector3.Lerp(p0, p1, Mathf.PingPong(Time.time * speed, 1));
    }
}
