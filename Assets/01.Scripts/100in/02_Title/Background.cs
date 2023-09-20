using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private Vector2 _parallaxRatio; //�갡 ī�޶� ���ۼ�Ʈ �ݿ����� x,y ������

    private Transform _mainCamTrm;
    private Vector3 _lastCamPos;



    private void Start()
    {
        _mainCamTrm = Camera.main.transform;
        _lastCamPos = _mainCamTrm.position;

 
  
    }

    private void LateUpdate()//Updata ������ ����Ǵ� ����
    {
        Vector3 deltMove = _mainCamTrm.position - _lastCamPos;
        transform.Translate(
            new Vector3(deltMove.x * _parallaxRatio.x, deltMove.y * _parallaxRatio.y),
            Space.World);
        _lastCamPos = _mainCamTrm.position;

    }
}
