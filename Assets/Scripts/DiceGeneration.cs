using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiceGeneration : MonoBehaviour
{
    public GameObject d6Prefab;
    public GameObject d8Prefab;
    public GameObject d12Prefab;
    public GameObject d14Prefab;

    private int diceAmount;
    private int currentDiceAmount;
    public float yCoord = 14f;
    public float zCoord = -0.5f;
    public float minXCoord;
    public float maxXCoord = 6.5f;
    public Vector3 scale = new Vector3(30f, 30f, 30f);
    private List<GameObject> dices = new List<GameObject>();
    private List<GameObject> clickedDices = new List<GameObject>();

    private GameObject prefabToSpawn;

    private void Awake()
    {
        diceAmount = PlayerPrefs.GetInt("diceAmount", 2);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentDiceAmount = diceAmount;
            RollDice();
            Debug.Log("Rolled dice");
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            ClearDice();
            currentDiceAmount = diceAmount - dices.Count;
            RollDice();
            Debug.Log("Select re-roll");
        }
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            ClearAllDice();
            Debug.Log("Removed all dices");
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            CheckHit();
        }
    }

    private void RollDice()
    {
        string diceType = PlayerPrefs.GetString("diceType", "D6");
        // Determine which prefab to spawn based on the PlayerPrefs variable
        switch (diceType)
        {
            case "D6":
                prefabToSpawn = d6Prefab;
                break;
            case "D8":
                prefabToSpawn = d8Prefab;
                break;
            case "D12":
                prefabToSpawn = d12Prefab;
                break;
            case "D14":
                prefabToSpawn = d14Prefab;
                break;
            default:
                Debug.LogWarning("Unrecognized dice type in PlayerPrefs. Defaulting to D6.");
                prefabToSpawn = d6Prefab;
                break;
        }
        
        // Spawn the cubes
        for (var i = 0; i < currentDiceAmount; i++)
        {
            // Randomness in spawn place
            Quaternion spawnRotation = Quaternion.Euler(Random.Range(0f, 180f), Random.Range(0f, 180f),
                Random.Range(0f, 180f));
            float xCoord = Random.Range(minXCoord, maxXCoord);
            Vector3 spawnPosition = new Vector3(xCoord, yCoord, zCoord);
            GameObject spawnedPrefab = Instantiate(prefabToSpawn, spawnPosition, spawnRotation);
            
            // Save objects for later reference
            dices.Add(spawnedPrefab);
        }
    }

    private void CheckHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Check Cast for hit
        if (!Physics.Raycast(ray, out hit)) return;
        // Check if hit was a dice
        if (hit.transform.CompareTag("Dice"))
        {
            var diceObject = hit.transform.gameObject;
            // Mark dice as hit.
            Debug.Log(hit.transform.name + " wurde ausgewählt.");
            
            // Mark and color dice
            if (diceObject.GetComponent<Renderer>().material.color == Color.red)
            {
                clickedDices.Remove(diceObject);
                diceObject.GetComponent<Renderer>().material.color = Color.white;
            }
            else
            {
                clickedDices.Add(diceObject);
                diceObject.GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }
    
    // Clear selected dices
    private void ClearDice()
    {
        foreach (GameObject dice in clickedDices)
        {
            dices.Remove(dice);
            Destroy(dice);
        }
        clickedDices.Clear();
    }
    
    // Clear all dices
    public void ClearAllDice()
    {
        foreach (GameObject dice in dices)
        {
            Destroy(dice);
        }

        dices.Clear();
    }
}