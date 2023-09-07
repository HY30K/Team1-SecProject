using Unity.Mathematics;
using UnityEngine;

public class BatchManager : MonoBehaviour
{
    [SerializeField] private GameObject unit;
    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Batch();
        }
    }

    void Batch()
    {
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 1f;
        Instantiate(unit, mousePos, quaternion.identity);
    }
}
