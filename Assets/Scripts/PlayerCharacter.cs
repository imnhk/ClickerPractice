using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    public PlayerManager player;
    public ParticleSystem particle;
    public Text hpText;

    public int maxHp;
    public int hp;
    public bool isAlive;

    void Start()
    {
        isAlive = true;
    }
    void Update()
    {
        UpdateUI();
    }

    public void Hit(int damage)
    {
        if(isAlive)
        {
            hp -= damage;
            particle.Play();
        }
    }
    private void UpdateUI()
    {
        hpText.text = "hp: " + hp + " / " + maxHp;
    }
}
