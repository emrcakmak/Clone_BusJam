using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bus : MonoBehaviour
{

    public List<GameObject> SeatHuman;
    public int passengers = 0;
    public float moveSpeed = 5.0f;
    public int busCapacity = 3;
    public Material redMaterial;
    public Material blueMaterial;
    public Material GreenMaterial;
    public Material Yellowaterial;
    public Renderer rnd;
    public ClorType ColorType;
    public bool IsFull => passengers >= busCapacity;

    public bool isPassengerýncrease = false;
    void Start()
    {
        rnd = GetComponent<Renderer>();

        switch (ColorType)
        {
            case ClorType.Red:
                rnd.material = redMaterial;
                break;
            case ClorType.Blue:
                rnd.material = blueMaterial;
                break;
            case ClorType.green:
                rnd.material = GreenMaterial;
                break;
            case ClorType.yellow:
                rnd.material = Yellowaterial;
                break;

        }
        foreach (GameObject obj in SeatHuman)
        {
            
            obj.GetComponent<Renderer>().material = rnd.material;
        }
    }

    
    void Update()
    {
        if(passengers<= busCapacity)
        {
            if (isPassengerýncrease)
            {
                for(int i=0;i< passengers;i++)
                    SeatHuman[i].transform.parent.gameObject.SetActive(true);
                isPassengerýncrease = false;
            }
        }
        
    }
}
