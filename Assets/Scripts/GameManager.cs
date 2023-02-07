using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float mapElementRadius = 2;
    internal int floorNumber = 1;// the lowest floor visible in the camera
    public float reverseGunFiringRange;

    bool lose = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }


    public void IncreaceFloorNumber()
    {
        floorNumber++;
        GetComponent<BuildingGenerator>().GenerateNextFloor();
    }

    public void Lose()
    {
        if (!lose)
        {
            lose = true;
            AudioManager.instance.Play("Lose");
            Debug.Log("You Lost");

            Camera.main.GetComponent<CameraMovement>().cameraSpeed = 0;
            //FindObjectOfType<CharecterMovement>().gameObject.SetActive(false);

            UIManager.instance.ShowEndMenu();
        }
    }
}
