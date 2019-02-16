using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    public PlayerCharacter targetCharacter;

    public float attackCooltime;
    private float attackTimer;
    public float respawnTime;

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

    public override void OnMouseDown() // 적을 클릭해서 조준. 다시 클릭하면 취소
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

    public override void Die()
    {
        base.Die();
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
    public override void Spawn()
    { 
        base.Spawn();
        maxHp = (int)Random.Range(player.characters[0].atk * 2.1f, player.characters[0].atk * 5.5f);
    }
}