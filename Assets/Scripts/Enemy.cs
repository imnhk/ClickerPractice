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

    public int maxHp;
    public int hp;
    public int atk;

    public float attackCooltime;
    private float attackTimer;
    public float respawnTime;
    public bool isAlive;

    void Update()
    {
        if(isAlive)
        {
            UpdateUI();

            // 공격 시간간격 확인
            attackTimer += Time.deltaTime;
            if(attackTimer > attackCooltime)
            {
                RandomAttack();
                attackTimer = 0;
            }

            if(hp <= 0) // 죽음ㅠ
            {
                Die();
                StartCoroutine(SpawnIn(respawnTime));
            }
        }
    }

    void OnMouseDown() // 적을 클릭해서 조준. 다시 클릭하면 취소
    {
        if(player.targetedEnemy==this)
            player.CancelTarget();
        else if(isAlive) // 살아있어야 조준 가능
            player.SetTarget(this);
    }

    public void RandomAttack()
    {
        if(player.GetRandomCharacter() != null) // 무작위 공격
        {
            PlayerCharacter randCharacter = player.GetRandomCharacter();
            randCharacter.Hit(atk);
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

        isAlive = false;
        player.CancelTarget(); // 죽으면 조준 취소
    }

    public IEnumerator SpawnIn(float sec) // 몇 초 뒤에 Spawn() 실행
    {
        float timer = 0f;
        while(timer < sec)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        Spawn();
    }
    public void Spawn()
    { 
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        hpText.gameObject.SetActive(true);

        maxHp = (int)Random.Range(player.characters[0].atk * 2.1f, player.characters[0].atk * 5.5f); // 이제 플레이어캐릭터가 여럿임. 그런데 어차피 공격력은 같음
        hp = maxHp;

        isAlive = true;
    }
    private void UpdateUI()
    {
        hpText.text = "hp: " + hp + " / " + maxHp;
    }
}