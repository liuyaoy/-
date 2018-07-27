using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 怪物详情
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class Monster : MonoBehaviour {
    // 怪物身上的导航组件
    private NavMeshAgent nav;
    // 怪物移动的终点
    private Transform targetPosition;
    // 怪物的动画组件
    private Animation ani;

    private float speed;
    private int hp;

    public int Hp {
        get {
            return hp;
        }
    }

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        // 获取终点
        targetPosition = GameObject.Find("/End").transform;
        // 朝终点移动
        nav.SetDestination(targetPosition.position);
        // 
        ani = GetComponent<Animation>();
    }

    /// <summary>
    /// 设置怪物信息
    /// </summary>
    /// <param name="hp"></param>
    /// <param name="speed"></param>
    public void SetInfo(int hp, float speed) {
        this.hp = hp;
        this.speed = speed;
        // 设置导航的速度
        nav.speed = speed;
    }

    /// <summary>
    /// 收到伤害
    /// </summary>
    /// <param name="damage"></param>
    public void GetDamaged(float damage)
    {
        // 减血
        hp -= (int)damage;

        if (hp <= 0)
        {
            // 停止导航
            nav.isStopped = true;
            // 说明怪物死亡
            ani.CrossFade("Dead");
            Destroy(gameObject, 1.5f);
        }
    }
}