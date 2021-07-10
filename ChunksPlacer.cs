using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChunksPlacer : MonoBehaviour
{
    public Chunk[] ChunkPrefabs;
    public Chunk ZeroChunk;
    public Chunk FinalChunk;
    public int FinalDistance;
    public GameObject WrongWay;

    [SerializeField] private GameObject[] bufferWrongWay;
    private Transform player;
    private bool SpawnMode = true;
    private List<Chunk> spawnedChunks = new List<Chunk>();

    private void OnEnable()
    {
        CheckPointFinal.OnFinish += FinalClean;
    }

    private void OnDisable()
    {
        CheckPointFinal.OnFinish -= FinalClean;
    }

    private void Start()
    {
        PlayerPrefs.SetFloat("Distance", FinalDistance); 
        player = CarController.player;
        spawnedChunks.Add(ZeroChunk);
        Chunk newChunk = Instantiate(ZeroChunk);
        newChunk.transform.position = spawnedChunks[0].Begin.position;
        StartCoroutine(CheckDistanceIE());

        bufferWrongWay = new GameObject[WrongWay.transform.childCount];
        int i = 0;
        foreach (Transform child in WrongWay.transform)
        {
            bufferWrongWay[i] = child.gameObject;
            i += 1;
        }
    }

    private IEnumerator CheckDistanceIE() 
    {
        if (player.position.z > spawnedChunks[spawnedChunks.Count - 1].End.position.z - 240)
        {
            SpawnChunk();
        }
        if (player.position.z >= FinalDistance) 
        {
            SpawnMode = false;
            SpawnChunk();
            StopCoroutine(CheckDistanceIE());
            yield break;
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(CheckDistanceIE());
    }

    private void SpawnChunk()
    {
        if (SpawnMode)
        {
            Chunk newChunk = Instantiate(GetRandomChunk());
            newChunk.transform.position = spawnedChunks[spawnedChunks.Count - 1].End.position - newChunk.Begin.localPosition;
            spawnedChunks.Add(newChunk);

            if (spawnedChunks.Count >= 8) 
            {
                bufferWrongWay = new GameObject[WrongWay.transform.childCount];
                int i = 0;
                foreach (Transform child in WrongWay.transform)
                {
                    bufferWrongWay[i] = child.gameObject;
                    i += 1;
                }
                WrongWay.transform.position = spawnedChunks[2].Begin.position; //установка тупика в предпоследний участок
                WrongWay.gameObject.isStatic = true;

                foreach (GameObject child in bufferWrongWay)
                {
                    child.gameObject.isStatic = true;
                }

                Destroy(spawnedChunks[1].gameObject); 
                spawnedChunks.RemoveAt(0); 
            }
        }
        else
        {
            Chunk newChunk = Instantiate(FinalChunk);
            newChunk.transform.position = spawnedChunks[spawnedChunks.Count - 1].End.position;
            return;
        }
        return;
    }

    private Chunk GetRandomChunk()
    {
        List<float> chances = new List<float>();
        for (int i = 0; i < ChunkPrefabs.Length; i++)
        {
            chances.Add(ChunkPrefabs[i].ChanceFromDistance.Evaluate(player.transform.position.z));
        }

        float value = Random.Range(0, chances.Sum());
        float sum = 0;

        for (int i = 0; i < chances.Count; i++)
        {
            sum += chances[i];
            if (value < sum)
            {
                return ChunkPrefabs[i];
            }
        }
        return ChunkPrefabs[ChunkPrefabs.Length];
    }
}
