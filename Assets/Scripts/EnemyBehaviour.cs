using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] float maximumEnemyDistanceFromEdgeOfTheBuilding;
    [SerializeField] float delayBetweenShots;
    [SerializeField] int numberOfHousesToAttackInEachStop;
    [SerializeField] int delayBetweenPositionChange;
    [SerializeField] int playerAttackProbabilityOneIn;
    [SerializeField] GameObject gunEndRef;
    [SerializeField] GameObject gunProjectile;
    [SerializeField] float projectileSpeed;
    [SerializeField] GameObject wingRef;
    [SerializeField] GameObject gunRef;
    internal float side;
    GameObject[] objectsToAttack;

    private void Start()
    {

    }

    public void Attack()
    {
        int a = Random.Range(0, playerAttackProbabilityOneIn);
        bool isAttackingPlayer;
        if (a == 0)
            isAttackingPlayer = true;
        else
            isAttackingPlayer = false;
        
        isAttackingPlayer = false;

        if (!isAttackingPlayer)
        {
            objectsToAttack = new GameObject[numberOfHousesToAttackInEachStop];
            int floorNum = 2 + Random.Range(0, 3);
            for (int i = 0; i < numberOfHousesToAttackInEachStop; i++)
            {
                objectsToAttack[i] = FindObjectOfType<BuildingGenerator>().floorParents[floorNum].GetComponentsInChildren<BuildingElement>()[Random.Range(0, 3)].gameObject;
            }
        }
        else
        {
            objectsToAttack = new GameObject[1];
            objectsToAttack[0] = FindObjectOfType<CharecterMovement>().gameObject;
        }
        Vector2 targetPos = GenerateTargetPosition(side, isAttackingPlayer, objectsToAttack[0].transform.position.y);
        GetComponent<EnemyMovement>().Move(targetPos);
    }

    public IEnumerator Shoot()
    {
        for(int i =0;i< numberOfHousesToAttackInEachStop;i++)
        {
            AudioManager.instance.Play("Enemy");

            int index = Random.Range(0, numberOfHousesToAttackInEachStop);
            Vector2 direction = (objectsToAttack[index].transform.position - gunEndRef.transform.position);
            GameObject projectile = Instantiate(gunProjectile, gunEndRef.transform.position, Quaternion.Euler(0, 0, Mathf.Approximately(side , 1) ? 180:0), null);
            projectile.GetComponent<Rigidbody2D>().velocity = direction.normalized * projectileSpeed;
            projectile.GetComponent<EnemyGunProjectile>().targetPosition = objectsToAttack[index].transform.position;
            yield return new WaitForSeconds(delayBetweenShots);
        }
        yield return new WaitForSeconds(delayBetweenPositionChange);
        Attack();
    }

    public void Initialize(float side)
    {
        this.side = side;
        Attack();
    }

    Vector2 GenerateTargetPosition(float side, bool isAttackingPlayer, float yPos)
    {
        return new Vector2(((GameManager.instance.mapElementRadius * 2.25f) + Random.Range(0f, maximumEnemyDistanceFromEdgeOfTheBuilding)) * side , yPos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<ReverseGunProjectile>() != null)
        {
            GetComponent<Rigidbody2D>().gravityScale = 5;
            Destroy(wingRef);
            Destroy(gunRef);
            Destroy(gameObject, 5);
            Destroy(this);
            Destroy(GetComponent<EnemyMovement>());
        }
    }

}
