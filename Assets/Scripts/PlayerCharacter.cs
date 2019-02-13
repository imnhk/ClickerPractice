using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    public PlayerManager player;
    public ParticleSystem particle;
    public Text hpText;
    public Text atkText;

    public int maxHp;
    public int hp;
    public int atk;
    public bool isAlive;

    void Update()
    {
        UpdateUI();
        
        if(hp <= 0) // 죽음ㅠ
            Die();
    }

    public void Attack()
    {
        if(player.targetEnemy != null) // 선택 공격
        {
            player.targetEnemy.Hit(atk);
            player.cameraScript.ShakeCamera();
        }
        else if(player.GetRandomEnemy() != null) // 무작위 공격
        {
            Enemy randEnemy = player.GetRandomEnemy();
            randEnemy.Hit(atk);
            player.cameraScript.ShakeCamera();
        }
        else return; // 공격불가
    }

    public void Hit(int damage)
    {
        if(isAlive)
        {
            hp -= damage;
            particle.Play();
        }
    }

    public void Die()
    {
        // 없애지는 않고 스프라이트만 안 보이게 함
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        hpText.gameObject.SetActive(false);
        atkText.gameObject.SetActive(false);

        isAlive = false;
    }
    public void Spawn()
    { 
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        hpText.gameObject.SetActive(true);
        atkText.gameObject.SetActive(true);

        hp = maxHp;
        isAlive = true;
    }

    private void UpdateUI()
    {
        hpText.text = "hp: " + hp + " / " + maxHp;
        atkText.text = "ATK: " + atk;
    }
}
