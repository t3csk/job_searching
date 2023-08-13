using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public Slider slider;
    public int maxHp = 10;
    public int hp;

    [SerializeField]
    GameObject gameUI;

    void Start()
    {
        hp = maxHp;
        slider.value = 1;
    }

    public void HpDamage(int d)
    {
        hp -= d;
        slider.value = (float)  hp/ (float) maxHp;
        if (hp <= 0)
        {
            ShowGameOverUI();
        }
    }

    private void ShowGameOverUI()
    {
        gameUI.SetActive(true);
    }
}
