using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public CameraMovement cameraScript;
    public Enemy targetedEnemy;
    public GameObject targetSprite;
    public List<Enemy> enemies = new List<Enemy>(3);
    // UI 텍스트
    public Text goldText;
    public Text atkText;
    public Text hpText;
    // Pleyer 정보
    public int gold;
    public int atk;
    public int atkPerUpgrade;
    public int maxHp;
    public int hp;

    void Start()
    {

    }
    void Update()
    {
        UpdateUI();
    }

    public void MakeGold(int gold)
    {
        this.gold += gold;
    }
    public void Attack()
    {
        if(targetedEnemy != null)
        {
            targetedEnemy.Hit(atk);
            ShakeCamera();
        }
        else if(GetRandomEnemy() != null) // 무작위 공격
        {
            Enemy randEnemy = GetRandomEnemy();
            randEnemy.Hit(atk);
            ShakeCamera();
        }
        else return; // 공격불가

    }
    public void UpgradeWeapon(int price)
    {
        if(gold >= price)
        {
            atk += atkPerUpgrade;
            gold -= price;
        }
    }
    public void SetTarget(Enemy enemy) // 조준
    {
        targetedEnemy = enemy;
        targetSprite.GetComponent<SpriteRenderer>().enabled = true;
        targetSprite.transform.position = enemy.transform.position;
    }
    public void CancelTarget() // 조준 취소
    {
        targetedEnemy = null;
        targetSprite.GetComponent<SpriteRenderer>().enabled = false;
    }

    private Enemy GetRandomEnemy()
    {
        // 살아있는 적 리스트를 만든다
        List<Enemy> aliveEnemies = new List<Enemy>();
        for(int i = 0; i<enemies.Count; i++)
            if(enemies[i].isAlive) 
                aliveEnemies.Add(enemies[i]);

        if(aliveEnemies.Count == 0) // 다 죽어서 못 고를 때
            return null; 
        else // 살아있는 적 리스트 중 무작위로 반환
            return aliveEnemies[Random.Range(0, aliveEnemies.Count)];
    }
    private void ShakeCamera()
    {
        StartCoroutine(cameraScript.Shake(0.1f, 0.03f));
    }
    private void UpdateUI()
    {
        goldText.text = "Gold: " + gold;
        atkText.text = "ATK: " + atk;
        hpText.text = "hp: " + hp + " / " + maxHp;
    }
}
