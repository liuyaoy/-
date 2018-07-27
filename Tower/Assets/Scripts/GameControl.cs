using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

    void Start () {
        transform.GetChild(0).GetComponent<Toggle>().onValueChanged.AddListener(isOn => {
            if (isOn)
                Time.timeScale = 3;
        });
        transform.GetChild(1).GetComponent<Toggle>().onValueChanged.AddListener(isOn => {
            if (isOn)
                Time.timeScale = 2;
        });
        transform.GetChild(2).GetComponent<Toggle>().onValueChanged.AddListener(isOn => {
            if (isOn)
                Time.timeScale = 1;
        });
        transform.GetChild(3).GetComponent<Toggle>().onValueChanged.AddListener(isOn => {
            if (isOn)
                Time.timeScale = 0;
        });
    }
}
