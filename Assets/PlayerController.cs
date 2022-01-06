using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private float lookSensitivity; // 카메라 돌아가는 민감도

    [SerializeField]
    private float cameraRotationLimit; // 고개 올리고 내리는 각도 제한
    private float currentCameraRotationX=0; // 현재 고개 각도

    [SerializeField] // player 안에 있는 자식 객체에 접근해서 camera 컴포넌트 가져와야 하므로 
    private Camera theCamera;

    private Rigidbody myRigid;


    void Start()
    {

        myRigid = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Move();
        CameraRotation();
        CharacterRotation();
    }

    private void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX; // transform.right: (1,0,0)
        Vector3 _moveVertical = transform.forward * _moveDirZ; // transform.forward: (0,0,1)

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * walkSpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    private void CharacterRotation()
    {
        // 좌우 캐릭터 회전
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f)*lookSensitivity; 
        myRigid.MoveRotation(myRigid.rotation*Quaternion.Euler(_characterRotationY)); // 3차원에서의 회전이므로 quaternion으로 변환해줘야 함. quaternion은 4원소
    }

    private void CameraRotation()
    {
        // 상하 카메라 회전
        float _xRotation = Input.GetAxis("Mouse Y"); // 마우스는 x, y만 있다. 위아래로
        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit); // 카메라 각도 가두기. - ~ + 사이에
        Debug.Log(myRigid.rotation);
        Debug.Log(myRigid.rotation.eulerAngles);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);


    }
}
