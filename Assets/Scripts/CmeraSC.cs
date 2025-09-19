using UnityEngine;
using UnityEngine.UI; // �������� UI elements ��� Slider

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // ������
    public float followSpeed = 2f;
    public Vector3 cameraOffset2D = new Vector3(0, 5, -10); // �������� �� ��� 2D
    public Vector3 cameraOffset3D = new Vector3(0, 5, -10); // �������� �� ��� 3D

    public Slider shakeSlider;  // �������� ���� ���� ����� ����� ������ ���������

    private bool isIn2DMode = true;
    private float originalCameraY;  // ����� ������ �������� ������
    private float shakeTimer = 0f;  // ���� ������
    private float randomMovementSpeed = 1f;  // ���� ������ ���������
    private Vector3 randomOffset = Vector3.zero;  // ������ ���������
    private float randomAmount = 0f;  // ����� ������� ��������
    private float randomIncrement = 0.01f; // ����� ������� �� ������� ��������
    private float randomDecreaseSpeed = 0.5f; // ���� ����� ������� �� 2D
    private float randomIncreaseSpeed = 0.3f; // ���� ����� ������� �� 3D

    void Start()
    {
        // ����� ������ �������� ������
        originalCameraY = transform.position.y;

        // ����� �������� ��� ������ �������
        if (shakeSlider != null)
        {
            shakeSlider.value = 0f;  // �������� ���� �� �����
        }
    }

    void Update()
    {
        // ������� ��� �������� ������� ������
        if (Input.GetKeyDown(KeyCode.R))
        {
            isIn2DMode = !isIn2DMode;
        }

        // ����� ���� ������ ���������
        shakeTimer += Time.deltaTime * randomMovementSpeed;

        // ����� �� ����� ������ ��� �����
        if (isIn2DMode)
        {
            // ����� ������� �������� �������� �� ��� 2D
            randomAmount = Mathf.Lerp(randomAmount, 0, Time.deltaTime * randomDecreaseSpeed);
        }
        else
        {
            // ����� ������� �������� �������� �� ��� 3D
            randomAmount = Mathf.Lerp(randomAmount, 1, Time.deltaTime * randomIncreaseSpeed);
        }

        // ����� ���� ������� ����� �� �� ���
        randomOffset = new Vector3(
            Mathf.Sin(shakeTimer * randomMovementSpeed) * randomAmount,  // ���� ������� ����� �� ������� X
            Mathf.Cos(shakeTimer * randomMovementSpeed) * randomAmount,  // ���� ������� ����� �� ������� Y
            0);  // �� ���� �� ����� �������� ������ �� ������� Z

        // ����� �������� ����� ��� ����� ������� ��������
        if (shakeSlider != null)
        {
            shakeSlider.value = randomAmount;  // ����� �������� ����� ����� ������� ��������
        }

        // ����� �������� ����� ��� �����
        if (isIn2DMode)
        {
            // �� ��� 2D� �������� ����� ��� �� ������ ��� ������ X
            Vector3 targetPosition = new Vector3(player.position.x, player.position.y + cameraOffset2D.y, cameraOffset2D.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition + randomOffset, followSpeed * Time.deltaTime);

            // �������� ��� ����� ������
            transform.rotation = Quaternion.Euler(0, 0, 0);  // ����� ����� �������� ���� �� ����
        }
        else
        {
            // �� ��� 3D� �������� ����� �� ������ �� ������� X � Y � Z
            transform.position = Vector3.Lerp(transform.position, player.position + cameraOffset3D + randomOffset, followSpeed * Time.deltaTime);

            // �������� ����� ������
            transform.LookAt(player);  // �������� ����� ������
        }
    }
}
