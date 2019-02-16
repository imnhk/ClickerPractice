using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class Character : MonoBehaviour
{
    public PlayerManager player;
    public ParticleSystem particle;
    public Text hpText;

    public int maxHp;
    public int hp;
    public int atk;
    public bool isAlive;
    
    public abstract void OnMouseDown();

    public virtual void Die()
    {
        // 스프라이트렌터러만 끄는 방식
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        hpText.gameObject.SetActive(false);
        isAlive = false;
    }

    public virtual void Spawn()
    {
        // Die() 함수의 반대
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        hpText.gameObject.SetActive(true);
        isAlive = true;
        hp = maxHp; // hp만 다 채운다
    }

    public virtual void Hit(int damage)
    {
        if(isAlive)
        {
            hp -= damage;
            particle.Play();
        }
    }

    public virtual void UpdateUI()
    {
        hpText.text = "hp: " + hp + " / " + maxHp;
    }
}
