using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Base : MonoBehaviour, IDamagable
{
    public BaseData data;
    public List<Entity> entities;
    public Base opponentBase;
    public TextMeshPro hpText;
    public Transform spawnPos;

    [SerializeField] private float curHP;
    private float maxHP;
    public int power;
    
    public bool isDead;
    protected bool canSpawn = true;

    protected virtual void Awake()
    {
        curHP = maxHP = data.maxHP;
        isDead = false;
        hpText.text = maxHP + "/" + maxHP;
    }

    private void OnEnable()
    {
        EventDispatcher.Instance.RegisterListener(EventID.On_Player_Win, OnEndGame);
        EventDispatcher.Instance.RegisterListener(EventID.On_Player_Lose, OnEndGame);
    }


    private void OnDisable()
    {
        EventDispatcher.Instance.RemoveListener(EventID.On_Player_Win, OnEndGame);
        EventDispatcher.Instance.RemoveListener(EventID.On_Player_Lose, OnEndGame);
    }

    public Transform GetNearestTarget()
    {
        int id = 0;
        Transform target;
        
        // Nếu hết quái thì chọn nhà chính để đánh
        if (opponentBase.entities.Count == 0)
        {
            return opponentBase.transform;
        }
        
        if (opponentBase.entities.Count == 1)
        {
            target = FindTheNearerTransform(0);
            return target;
        }

        // Nếu có nhiều quái thì chọn con gần nhà mình nhất để đánh
        float min = Vector2.Distance(transform.position, opponentBase.entities[0].transform.position);
        for (int i = 1; i < opponentBase.entities.Count; i++)
        {
            float distance = Vector2.Distance(transform.position, opponentBase.entities[i].transform.position);

            if (distance < min)
            {
                min = distance;
                id = i;
            }
        }

        target = FindTheNearerTransform(id);
        return target;
    }

    private Transform FindTheNearerTransform(int id)
    {
        float dis1 = Vector2.Distance(transform.position, opponentBase.entities[id].transform.position);
        float dis2 = Vector2.Distance(transform.position, opponentBase.transform.position);

        if (dis1 < dis2)
        {
            return opponentBase.entities[id].transform;
        }

        return opponentBase.transform;
    }

    public void TakeDamage(float amount)
    {
        curHP -= amount;
        
        if (curHP <= 0)
        {
            curHP = 0;
            isDead = true;
            this.PostEvent(EventID.Base_Destroyed, this);
        }
        
        hpText.text = curHP + "/" + maxHP;
    }

    protected void GetSumPower()
    {
        power = 0;
        
        for (int i = 0; i < entities.Count; i++)
        {
            power += entities[i].power;
        }
    }

    private void OnEndGame(object param)
    {
        canSpawn = false;
    }
}