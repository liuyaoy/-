using System.Collections.Generic;
using UnityEngine;

/* 攻击对象查找:
 * 每当一个怪物进入到攻击范围, 就把这个怪物加到一个集合中
 * 攻击这个集合中的第0个元素
 * 如果怪物死亡或者怪物走出攻击范围, 把这个怪物从集合中移除
 */
public class Tower : MonoBehaviour {

    /// <summary>
    /// 炮弹
    /// </summary>
    public GameObject Bullet;
    public float attackInterval = 1;   // 攻击的时间间隔
    public float attackDamage = 0;     // 炮塔的攻击力

    /// <summary>
    /// 所有的攻击范围的敌人
    /// </summary>
    private List<Monster> targets;
    private Transform turret;           // 炮台
    private Transform firePoint;        // 炮弹发射位置

    private float attackTimer;          // 攻击计时器

    private void Awake()
    {
        targets = new List<Monster>();
        turret = transform.Find("Base/Turret");
        firePoint = turret.Find("Barrel/Plasma_fx");
    }

    private void OnTriggerEnter(Collider other)
    {
        // 每当走进一个怪物, 就把这个怪物加到攻击队列中
        if (other.tag == "Enemy")
        {
            targets.Add(other.GetComponent<Monster>());
        }
    }

    private void FixedUpdate()
    {
        // 空集合判断
        if (targets.Count == 0) return;

        // 获取当前的攻击目标
        Monster currentTarget = targets[0];

        // 判断当前目标是否已经死亡, 如果已经死了, 切换新的目标
        if (currentTarget == null || currentTarget.Hp <= 0)
        {
            // 移除攻击队列
            targets.Remove(currentTarget);
            // 设置新的目标
            if (targets.Count == 0) return;
            currentTarget = targets[0];
        }

        if (currentTarget == null) return;


        // 看向目标
        LookTarget(currentTarget);

        attackTimer += Time.fixedDeltaTime;
        if (attackTimer >= attackInterval && shouldFire(currentTarget)) {
            // 攻击目标
            Fire(currentTarget);
            // 重置计时器
            attackTimer = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 每当一个怪物走出攻击范围, 就把这个怪物从攻击队列中移除
        if (other.tag == "Enemy")
        {
            targets.Remove(other.GetComponent<Monster>());
        }
    }

    /// <summary>
    /// 转动炮台, 看向当前的目标
    /// </summary>
    /// <param name="target"></param>
    private void LookTarget(Monster target) {
        // turret.LookAt(target.transform);
        // 获取目标旋转角度
        Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - turret.position);
        turret.rotation = Quaternion.Lerp(turret.rotation, targetRotation, Time.deltaTime * 10);
    }

    /// <summary>
    /// 能否可以攻击目标
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    private bool shouldFire(Monster target) {
        // 如果炮台的前方和目标的夹角小于一定的值, 就认为瞄准敌人了, 可以开炮
        float angle = Vector3.Angle(turret.forward, target.transform.position - turret.position);
        return angle < 5;
    }

    /// <summary>
    /// 攻击目标
    /// </summary>
    /// <param name="target"></param>
    private void Fire(Monster target)
    {
        // 生成炮弹
        GameObject bullet = Instantiate(Bullet, firePoint.position, Quaternion.identity);
        // 设置炮弹的目标
        bullet.GetComponent<Bullet>().Target = target;
        // 设置炮弹的攻击力
        bullet.GetComponent<Bullet>().damage = attackDamage;
    }
}
