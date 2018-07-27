using UnityEngine;

public class EndCondition : MonoBehaviour {

    /// <summary>
    /// 检测是否有物体进入触发器
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        // 销毁进入的物体
        Destroy(other.gameObject);
    }
}
