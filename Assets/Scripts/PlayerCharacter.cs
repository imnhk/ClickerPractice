using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : Character
{
    public Text atkText;

    void Update()
    {
        UpdateUI();
        
        if(hp <= 0) Die();
    }

    public override void OnMouseDown() // 캐릭터를 클릭해서 조준. 다시 클릭하면 취소
    {
        if(player.selectedCharacter==this) 
            player.CancelCharacterSelect(); // 선택 취소
        else 
            player.SelectCharacter(this); // 선택
    }

    public void Attack()
    {
        if(player.targetedEnemy != null) // 선택 공격
        {
            player.targetedEnemy.Hit(atk);
            player.cameraScript.ShakeCamera();
        }
        else RandomAttack();
    }

    public void RandomAttack()
    {
        if(player.GetRandomEnemy() != null) // 무작위 공격
        {
            EnemyCharacter randEnemy = player.GetRandomEnemy();
            randEnemy.Hit(atk);
            player.cameraScript.ShakeCamera();
        }
        else return; // 공격불가
    }

    public override void Die()
    {
        base.Die();
        atkText.gameObject.SetActive(false);
    }

    public override void Spawn()
    {
        base.Spawn();
        atkText.gameObject.SetActive(true);
    }

    public override void UpdateUI()
    {
        base.UpdateUI();
        atkText.text = "ATK: " + atk;
    }
}
