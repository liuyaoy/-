using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class TowerInfo {
    public GameObject TowerPre;     // 防御塔类型
    public float TowerInterval;     // 防御塔攻击频率
    public float TowerAttack;       // 防御塔攻击力
}

/// <summary>
/// 负责防御塔的生成
/// </summary>
public class TowerManager : MonoBehaviour {

    /// <summary>
    /// 所有可以创建的防御塔
    /// </summary>
    public List<TowerInfo> towers;

    /// <summary>
    /// 当前选择的防御塔下标
    /// </summary>
    private int currentChoise = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 创建防御塔
            AddTower();
        }
    }

    /// <summary>
    /// 创建防御塔
    /// </summary>
    private void AddTower()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool result = Physics.Raycast(ray, out hit);

        if (result)
        {
            // 判断点击的是一个底座, 并且这个底座上没有炮塔
            if (hit.collider.name.Contains("Tower_Base") && hit.collider.transform.childCount == 0)
            {
                // 在这个底座上生成一个炮塔
                GameObject tower = Instantiate(towers[currentChoise].TowerPre, hit.collider.transform);
                // 修改这个炮塔的位置
                Vector3 position = tower.transform.localPosition;
                position.y = 2.59f;
                tower.transform.localPosition = position;

                // 设置这个炮塔的攻击力和攻击频率
                tower.GetComponent<Tower>().attackDamage = towers[currentChoise].TowerAttack;
                tower.GetComponent<Tower>().attackInterval = towers[currentChoise].TowerInterval;
            }
        }
    }
}
