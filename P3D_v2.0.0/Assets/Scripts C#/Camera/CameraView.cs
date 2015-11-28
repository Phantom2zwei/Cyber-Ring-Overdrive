using UnityEngine;
using System.Collections;

public class CameraView : MonoBehaviour {

    [SerializeField]
    private Transform m_camera;
    [SerializeField]
    private float m_mouseSensitivityX;
    [SerializeField]
    private float m_mouseSensitivityY;

    private Transform m_trans;
    private Quaternion m_cameraTargetRotX;
    private Quaternion m_cameraTargetRotY;
    private float m_minimumX;
    private float m_maximumX;

    void Awake()
    {
        m_trans = transform;
        m_mouseSensitivityX = 2.0f;
        m_mouseSensitivityY = 2.0f;
        m_minimumX = -90.0f;
        m_maximumX = 90.0f;
        InitCamera();
    }

    void LateUpdate()
    {
        if (PlayerPrefs.GetInt("Pause") == 0)
        {
            LookRotation();
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void InitCamera()
    {
        m_cameraTargetRotX = m_trans.localRotation;
        m_cameraTargetRotY = m_camera.localRotation;
    }

    private void LookRotation()
    {
        float rotY = Input.GetAxis("Mouse X") * m_mouseSensitivityX;
        float rotX = Input.GetAxis("Mouse Y") * m_mouseSensitivityY;

        m_cameraTargetRotX *= Quaternion.Euler(-rotX, 0f, 0f);
        m_cameraTargetRotX = ClampRotationAroundXAxis(m_cameraTargetRotX);
        m_trans.localRotation = m_cameraTargetRotX;
        m_cameraTargetRotY *= Quaternion.Euler(0, rotY, 0f);
        m_camera.localRotation = m_cameraTargetRotY;
    }

    Quaternion ClampRotationAroundXAxis(Quaternion quaternion)
    {
        quaternion.x /= quaternion.w;
        quaternion.y /= quaternion.w;
        quaternion.z /= quaternion.w;
        quaternion.w = 1.0f;

        float tmpAngleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(quaternion.x);

        tmpAngleX = Mathf.Clamp(tmpAngleX, m_minimumX, m_maximumX);

        quaternion.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * tmpAngleX);

        return quaternion;
    }
}
