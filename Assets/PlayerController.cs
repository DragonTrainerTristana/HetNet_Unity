using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private float lookSensitivity; // ī�޶� ���ư��� �ΰ���

    [SerializeField]
    private float cameraRotationLimit; // �� �ø��� ������ ���� ����
    private float currentCameraRotationX=0; // ���� �� ����

    [SerializeField] // player �ȿ� �ִ� �ڽ� ��ü�� �����ؼ� camera ������Ʈ �����;� �ϹǷ� 
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
        // �¿� ĳ���� ȸ��
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f)*lookSensitivity; 
        myRigid.MoveRotation(myRigid.rotation*Quaternion.Euler(_characterRotationY)); // 3���������� ȸ���̹Ƿ� quaternion���� ��ȯ����� ��. quaternion�� 4����
    }

    private void CameraRotation()
    {
        // ���� ī�޶� ȸ��
        float _xRotation = Input.GetAxis("Mouse Y"); // ���콺�� x, y�� �ִ�. ���Ʒ���
        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit); // ī�޶� ���� ���α�. - ~ + ���̿�
        Debug.Log(myRigid.rotation);
        Debug.Log(myRigid.rotation.eulerAngles);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);


    }
}
