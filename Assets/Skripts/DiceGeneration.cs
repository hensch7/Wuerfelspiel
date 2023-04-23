using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiceGeneration : MonoBehaviour
{
    public GameObject d6Prefab;
    public GameObject d10Prefab;

    public TMP_InputField prefab_count;
    public float yCoord = 14f;
    public float zCoord = -1f;
    public float minXCoord = 0f;
    public float maxXCoord = 6.5f;
    public Vector3 scale = new Vector3(0.2f, 0.2f, 0.2f);
    private List<GameObject> dices = new List<GameObject>();

    void Start()
    {
    }

    public void RollDice() 
    {
        if (prefab_count.text is null || prefab_count.text == "") {
            prefab_count.text = "1";
        }
        for (int i = 0; i < int.Parse(prefab_count.text); i++)
        {
            GameObject prefabToSpawn;
            if (Random.value > 0.5f)
            {
                prefabToSpawn = d6Prefab;
            }
            else
            {
                prefabToSpawn = d6Prefab;
            }
            Quaternion spawnRotation = Quaternion.Euler(Random.Range(0f, 180f), Random.Range(0f, 180f), Random.Range(0f, 180f));
            float xCoord = Random.Range(minXCoord, maxXCoord);
            Vector3 spawnPosition = new Vector3(xCoord, yCoord, zCoord);
            GameObject spawnedPrefab = Instantiate(prefabToSpawn, spawnPosition, spawnRotation);
            spawnedPrefab.transform.localScale = scale;
            dices.Add(spawnedPrefab);
        }
    }

    public void ClearDice()
    {
        for (int i = 0; i < dices.Count; i++)
        {
            Destroy(dices[i]);
        }
        dices.Clear();
    }
}