using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class BatchTile : MonoBehaviour
{
    private Tilemap _tilemap;
    public static BatchTile Instance;
    private void Awake()
    {
        if(Instance != null)
            Destroy(Instance);
        Instance = this;

        _tilemap = GetComponent<Tilemap>();
    }

    public bool IsBatchAble(Vector2 position)
    {
        TileBase tileBase = null;
        tileBase = _tilemap.GetTile(_tilemap.WorldToCell(position));
        return tileBase;
    }

    public Vector2 Vector2IntPos(Vector2 position)
    {
        Vector2 pos = Vector2Int.CeilToInt(position);
        pos += new Vector2(-0.5f, -0.5f);
        return pos;
    }
}
