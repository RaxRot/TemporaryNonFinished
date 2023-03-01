using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBee : Enemy
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform pointToShoot;
    private GameObject _spawnedBullet;

    [SerializeField] private float minTimeBetweenShoot = 3f;
    [SerializeField] private float maxTimeBetweenShoot = 6f;
    private float _timeBetweenShoot;

     protected override void Start()
    {
        base.Start();
        
        StartShoot();
    }

    public void ShootBullet()
    {
        _spawnedBullet = Instantiate(bullet, pointToShoot.position, Quaternion.identity);
    }

    private IEnumerator _StartSootCo()
    {
        _timeBetweenShoot = Random.Range(minTimeBetweenShoot, maxTimeBetweenShoot);
        yield return new WaitForSeconds(_timeBetweenShoot);
        
        Anim.SetTrigger(TagManager.ENEMY_SHOOT_ANIMATION_TRIGGER);
        
        StartShoot();
    }

    private void StartShoot()
    {
        StartCoroutine("_StartSootCo");
    }
}
