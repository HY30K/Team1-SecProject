using UnityEngine;
using UnityEngine.Tilemaps;

public class BatchCheck : MonoBehaviour
{
    private SpriteRenderer batchAreaRenderer;
    public static bool batchble = true;

    private void Awake()
    {
        batchAreaRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 0f, LayerMask.GetMask("Unit"));
        if (!BatchTile.Instance.IsBatchAble(transform.position) || hit.collider != null)//ª°∞≠
        {
            batchAreaRenderer.color = new Color(0.5f, 0f,0);
            batchble = false;
        }
        else//√ ∑œ
        {
            batchAreaRenderer.color = new Color(0f, 0.5f,0);
            batchble = true;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Unit"))
        {
            batchble = false;
            batchAreaRenderer.color = new Color(0.5f, 0, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Unit"))
        {
            batchble = true;
            batchAreaRenderer.color = new Color(0f, 0.5f, 0);
        }
    }
}
