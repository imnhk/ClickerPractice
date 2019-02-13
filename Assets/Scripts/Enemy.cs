using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public PlayerManager player;
    public PlayerCharacter targetCharacter;
    public ParticleSystem particle;

    public Text hpText;
    public int hp;
    public int maxHp;


    public float respawnTime;
    private float deathTimer;

    public bool isAlive;

    void Start()
    {   
        isAlive = true;
    }

    void Update()
    {
        if(isAlive)
        {
            UpdateUI();

            if(hp <= 0) Die();
        }
        else//(isDead)
        {
            // 죽은 뒤 시간을 센다
            deathTimer += Time.deltaTime;
            if(deathTimer > respawnTime) Spawn();
        }
    }

    void OnMouseDown() // 적을 클릭해서 조준. 다시 클릭하면 취소
    {
        if(player.targetEnemy==this)
            player.CancelTarget();
        else if(isAlive) // 살아있어야 조준 가능
            player.SetTarget(this);
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

        isAlive = false;
        player.CancelTarget(); // 죽으면 조준 취소
    }
    public void Spawn()
    { 
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        hpText.gameObject.SetActive(true);

        maxHp = (int)Random.Range(player.atk * 2.1f, player.atk * 5.5f);
        hp = maxHp;

        deathTimer = 0;
        isAlive = true;
    }
    private void UpdateUI()
    {
        hpText.text = "hp: " + hp + " / " + maxHp;
    }
}
