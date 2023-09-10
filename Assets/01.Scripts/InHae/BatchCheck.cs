using System;
using UnityEngine;

public class BatchCheck : MonoBehaviour
{
    private SpriteRenderer batchAreaRenderer;
    public static bool batchble = true;

    private void Awake()
    {
        batchAreaRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Unit") || !other.CompareTag("Ground"))
        {
            batchble = false;
            batchAreaRenderer.color = new Color(0.5f, 0, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Unit") || !other.CompareTag("Ground"))
        {
            batchble = true;
            batchAreaRenderer.color = new Color(0f, 0.5f, 0);
        }
    }
}
