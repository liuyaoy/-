using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 每一波怪物的信息
/// </summary>
[System.Serializable]
public class MonsterInfo {
    public float WaveInterval;      // 波次的时间间隔
    public float MonsterInterval;   // 怪物生成的间隔
    public int MonsterConut;        // 这一波怪的数量
    public GameObject MonsterPre;   // 这一波怪的类型
    public int MonsterHP;           // 这一波怪的血量
    public float MonsterSpeed;      // 这一波怪的移速
}


/// <summary>
/// 用来做怪物里的生成管理
/// </summary>
public class MonsterManager : MonoBehaviour {

    /// <summary>
    /// 游戏中的所有的怪物
    /// </summary>
    public List<MonsterInfo> monsters;

    private int currentWave = 0, currentCount = 0;  // 当前波次与当前怪物数量
    private float waveTimer = 0, monsterTimer = 0;  // 两个生成的计时器

    /// <summary>
    /// 固定时间间隔, 生成怪物
    /// </summary>
    private void FixedUpdate()
    {
        // 判断是否所有怪物都生成完了
        if (currentWave >= monsters.Count) return;

        waveTimer += Time.fixedDeltaTime;
        if (waveTimer >= monsters[currentWave].WaveInterval) {
            // 说明该生成新的一波怪物
            monsterTimer += Time.fixedDeltaTime;
            if (monsterTimer >= monsters[currentWave].MonsterInterval) {
                // 说明该生成新的怪物了
                InitMonster();
                // 当前怪物数量+1
                currentCount++;
                // 怪物计时器清零
                monsterTimer = 0;

                if (currentCount >= monsters[currentWave].MonsterConut) {
                    // 说明: 这一波怪生成结束
                    currentWave++;
                    waveTimer = 0;
                    currentCount = 0;
                }
            }
        }
    }

    /// <summary>
    /// 生成怪物
    /// </summary>
    private void InitMonster() {
        // 生成一个怪物
        GameObject monster = Instantiate(monsters[currentWave].MonsterPre, transform.position, Quaternion.identity);
        // 设置怪物信息
        monster.GetComponent<Monster>().SetInfo(monsters[currentWave].MonsterHP, monsters[currentWave].MonsterSpeed);
    }
}
