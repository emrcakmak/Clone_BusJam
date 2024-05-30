using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class GridCell : MonoBehaviour
{
    public Vector2Int coordinates;
    public bool isWalkable = true;
    public int gCost; 
    public int hCost; 
    public int fCost { get { return gCost + hCost; } } 

    public GridCell parent;

    public CharacterController characters;
    public ClorType ColorType;


    public CharacterController charcterpref;




    public void CheckForChild()
    {
        if (transform.childCount > 0)
        {
            
            isWalkable = false; 
            
        }
        else
        {
            
            isWalkable = true; 
           
        }

    }

    private void Start()
    {
        characters = GetComponentInChildren<CharacterController>();
        CheckForChild();
    }
    private void Update()
    {
        CheckForChild();
    }



    [ContextMenu("Create Character")]
    public void CreateACharacter()
    {
        if (characters != null) return;

        CharacterController character = Instantiate(charcterpref);
        character.transform.position = transform.position;
        character.transform.SetParent(transform);
        character.ColorType = ColorType;
        characters = character;

        isWalkable = false;
       
    }
    [ContextMenu("Delate Character")]
    public void RemoveTheCharacter() 
    {
        if (characters == null) return;

        DestroyImmediate(characters.gameObject);

        isWalkable = true;
    }

}
