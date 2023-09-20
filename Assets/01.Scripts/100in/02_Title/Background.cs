using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private Vector2 _parallaxRatio; //얘가 카메라를 몇퍼센트 반영할지 x,y 축으로

    private Transform _mainCamTrm;
    private Vector3 _lastCamPos;



    private void Start()
    {
        _mainCamTrm = Camera.main.transform;
        _lastCamPos = _mainCamTrm.position;

 
  
    }

    private void LateUpdate()//Updata 다음에 실행되는 거임
    {
        Vector3 deltMove = _mainCamTrm.position - _lastCamPos;
        transform.Translate(
            new Vector3(deltMove.x * _parallaxRatio.x, deltMove.y * _parallaxRatio.y),
            Space.World);
        _lastCamPos = _mainCamTrm.position;

    }
}
