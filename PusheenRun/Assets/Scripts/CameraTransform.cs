using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class CameraTransform : MonoBehaviour
{
    public Transform player;
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    private Camera m_Camera;
    // Start is called before the first frame update
    void Start()
    {
        m_Camera = GetComponent<Camera>();
        Debug.Log(transform.position.x - player.position.x );
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("player" + player.position);
        if (player)
        {
            Vector3 point = m_Camera.WorldToViewportPoint(player.position);
            if (point.x > 0.5F)
            {

                Vector3 delta = new Vector3(player.position.x, 0, 0) - m_Camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
                Vector3 destination = transform.position + delta;
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            }
        }

    }
}
