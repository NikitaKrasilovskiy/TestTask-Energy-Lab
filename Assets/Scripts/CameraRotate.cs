using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] private Transform target;   // объект вокруг которого будем вращать камеру
    [SerializeField] private float sensitivity;  // чувствительность мышки
    [SerializeField] private float limit;        // ограничение вращения по Y
    [SerializeField] private float zoom;         // чувствительность при увеличении, колесиком мышки
    [SerializeField] private float zoomMax;      // макс. увеличение
    [SerializeField] private float zoomMin;      // мин. увеличение

    [SerializeField] private GameObject _panelMenu;

    private Vector3 offset = new(0, 0.5f, -0.5f);
    private float X, Y;

    private void Awake()
    {
        limit = Mathf.Abs(limit);
        if (limit > 90) limit = 90;
        offset = new Vector3(offset.x, offset.y, -Mathf.Abs(zoomMax) / 2);
        transform.localPosition = target.localPosition + offset;

        RotateCam();
        ZoomCamera();
    }

    private void Update()
    {
        if (_panelMenu.activeInHierarchy) return;

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            offset.z += zoom;
            ZoomCamera();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            offset.z -= zoom;
            ZoomCamera();
        }

        if (Input.GetMouseButton(1))
        { RotateCam(); }
    }

    private void RotateCam()
    {
        X = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
        Y += Input.GetAxis("Mouse Y") * sensitivity;
        Y = Mathf.Clamp(Y, -limit, limit);
        transform.localEulerAngles = new Vector3(-Y, X, 0);
        transform.localPosition = transform.localRotation * offset + target.localPosition;
    }

    private void ZoomCamera()
    {
        offset.z = Mathf.Clamp(offset.z, -Mathf.Abs(zoomMin), -Mathf.Abs(zoomMax));
        transform.localPosition = transform.localRotation * offset + target.localPosition;
    }
}