using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingElement : MonoBehaviour
{
    BuildingBrick[] children;
    public float brickBreakForce;
    public float brickMaximumTorque;
    public float repairSmoothAmount;
    internal bool isBroken;

    private void Start()
    {
        children = GetComponentsInChildren<BuildingBrick>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ReverseGunProjectile>() != null)
        {
            if(isBroken)
                Repair();
        }
        else if (collision.GetComponent<EnemyGunProjectile>() != null)
        {
            if(!isBroken)
                Break(collision.gameObject.transform.position);
        }
    }

    private void Break(Vector2 hitPos)
    {
        AudioManager.instance.Play("Break");

        foreach (BuildingBrick brick in children)
        {
            isBroken = true;
            brick.Break(hitPos);
        }
        BuildingElement[] floor = gameObject.transform.parent.GetComponentsInChildren<BuildingElement>();
        int temp = 0;
        foreach (BuildingElement element in floor)
        {
            if (element.isBroken == true)
            {
                temp++;
            }
        }
        if (temp == 3)
        {
            GameManager.instance.Lose();
            FindObjectOfType<Camera>().GetComponentInChildren<BoxCollider2D>().enabled = false;
            foreach (GameObject buildingElement in FindObjectOfType<BuildingGenerator>().buildingElements)
            {
                foreach (BuildingBrick brick in buildingElement.GetComponentsInChildren<BuildingBrick>())
                {
                    brick.Break((Vector2)buildingElement.transform.position + Vector2.right * Random.Range(-1f, 1f) + Vector2.up * Random.Range(-1f, 1f));
                    Destroy(brick.gameObject, 5);
                }
            }
        }
    }
    private void Repair()
    {
        AudioManager.instance.Play("Repair");
        foreach (BuildingBrick brick in children)
        {
            isBroken = false;
            brick.StartCoroutine(brick.Repair());
        }
    }
}
