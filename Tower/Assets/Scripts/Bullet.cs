using UnityEngine;

public class Bullet : MonoBehaviour {

    private Monster target; // 当前的敌人
    public float damage;    // 炮弹的攻击力

    public Monster Target {
        set {
            target = value;
        }
    }

    private void Update()
    {
        if (target != null)
            // 追踪敌人
            transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime * 10);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            // 炮弹打中敌人了
            Destroy(gameObject, 1f);
            // 敌人受到伤害
            other.GetComponent<Monster>().GetDamaged(damage);
        }
    }

}
