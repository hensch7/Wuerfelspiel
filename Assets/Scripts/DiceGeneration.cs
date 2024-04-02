using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Scripts
{
    public class DiceGeneration : MonoBehaviour
    {
        public GameObject d6Prefab;
        public GameObject d10Prefab;

        public TMP_InputField prefabCount;
        public float yCoord = 14f;
        public float zCoord = -1f;
        public float minXCoord;
        public float maxXCoord = 6.5f;
        public Vector3 scale = new Vector3(0.2f, 0.2f, 0.2f);
        private List<GameObject> dices = new List<GameObject>();


        public void RollDice()
        {
            if (prefabCount.text is null or "")
            {
                prefabCount.text = "1";
            }

            for (var i = 0; i < int.Parse(prefabCount.text); i++)
            {
                GameObject prefabToSpawn = Random.value > 0.5f ? d6Prefab : d6Prefab;

                Quaternion spawnRotation = Quaternion.Euler(Random.Range(0f, 180f), Random.Range(0f, 180f),
                    Random.Range(0f, 180f));
                float xCoord = Random.Range(minXCoord, maxXCoord);
                Vector3 spawnPosition = new Vector3(xCoord, yCoord, zCoord);
                GameObject spawnedPrefab = Instantiate(prefabToSpawn, spawnPosition, spawnRotation);
                spawnedPrefab.transform.localScale = scale;
                dices.Add(spawnedPrefab);
            }
        }

        public void ClearDice()
        {
            foreach (GameObject dice in dices)
            {
                Destroy(dice);
            }

            dices.Clear();
        }
    }
}