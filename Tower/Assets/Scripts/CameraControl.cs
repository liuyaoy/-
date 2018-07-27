using UnityEngine;

public class CameraControl : MonoBehaviour {

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical);

        if (direction != Vector3.zero)
        {
            // 让摄像机移动
            transform.Translate(direction * 20 * Time.deltaTime, Space.World);
        }
    }
}
