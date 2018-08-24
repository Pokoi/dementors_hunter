using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour {


    public List<GameObject> prefabs;
    public float max_time;
    public float min_time;
   
      
    List<GameObject> npcs = new List<GameObject>();



    public void SpawnNPC(float waiting_time)
    {
        Invoke("Spawn", waiting_time);
    }


    void Start () {        
        for (byte index = 0; index < prefabs.Count; index++)
        {
            GameObject instancia = Instantiate(prefabs[index], transform.position, Quaternion.identity);
            instancia.GetComponent<NPCManager>().SendToPool(true);
            npcs.Add(instancia);
        }

        Invoke("Spawn", Random.Range(min_time, max_time));
              
    }
	void Spawn()
    {
        if (gameObject.activeInHierarchy == true)
        {
            int index = Random.Range(0, npcs.Count);
            bool right = (Random.Range(0, 11) % 2 == 0);

            npcs[index].GetComponent<NPCManager>().SetToSpawn(right);
        }
    }

    private void OnEnable()
    {
        Spawn();
    }
}
