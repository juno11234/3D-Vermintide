using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Slider hpSlider;
    public Player player;

    public Slider skillSlider;
    public GreatSword greatSword;

    public Image[] guardGageImage;
    public GameObject guardGage;


    private void Start()
    {
        player = Player.CurrentPlayer;
        hpSlider.maxValue = player.stat.maxHp;
        skillSlider.maxValue = greatSword.maxSkillGage;
    }

    void Update()
    {
        hpSlider.value = player.stat.hp;
        skillSlider.value = greatSword.currentSkillGage;

        if (greatSword.isGuarding)
        {
            guardGage.SetActive(true);
            for (int i = 0; i < guardGageImage.Length; i++)
            {
                if (i < greatSword.currentStamina)
                {
                    guardGageImage[i].gameObject.SetActive(true);
                }
                else
                {
                    guardGageImage[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            guardGage.SetActive(false);
        }
    }
}