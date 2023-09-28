using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;

public class CameraMove : MonoBehaviour
{
    [Header("MouseWheel")]
    [SerializeField] private float wheelSpeed;
    [SerializeField] private float maxSize;
    [SerializeField] private float minSize;

    [Header("FollowValue")]
    [SerializeField] private Transform units;
    [SerializeField] private Transform player;

    [Header("UnitProfile")]
    [SerializeField] private GameObject unitProfile;
    [SerializeField] private Image unitImage;
    [SerializeField] private TextMeshProUGUI unitText;

    private CinemachineVirtualCamera vcam;
    private Rigidbody2D rb;
    private int unitIndex;

    private void Awake()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MouseWheel();
        OnFollowCam();
    }

    private void OnFollowCam()
    {
        if (Input.GetMouseButtonDown(1) && units.childCount > 0)
        {
            unitProfile.SetActive(true);

            Transform unit = units.GetChild(unitIndex);
            AgentData unitData = Resources.Load<AgentData>($"UnitSO/{unit.name}");
            vcam.Follow = unit;
            unitImage.sprite = unitData.Sprite;
            unitText.text = $"LV.{unitData.level}";

            if (unitIndex >= units.childCount - 1)
            {
                unitIndex = 0;
            }
            else
            {
                unitIndex++;
            }
        }
        else if(Input.GetAxisRaw("Horizontal") != 0 ||
            Input.GetAxisRaw("Vertical") != 0)
        {
            unitProfile.SetActive(false);
            vcam.Follow = player;
        }
    }

    private void MouseWheel()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * -wheelSpeed;

        if (vcam.m_Lens.OrthographicSize >= maxSize && scroll > 0) //√÷¥Î ¡‹æ∆øÙ
            vcam.m_Lens.OrthographicSize = maxSize;
        else if (vcam.m_Lens.OrthographicSize <= minSize && scroll < 0) //√÷¥Î ¡‹¿Œ
            vcam.m_Lens.OrthographicSize = minSize;
        else
            vcam.m_Lens.OrthographicSize += scroll;
    }
}
