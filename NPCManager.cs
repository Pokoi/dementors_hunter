using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour {

      
    
    public float max_time;
    public float min_time;
    public bool on_pool;
    public enum Type_NPC { lady, dementor, lady_dementor };
    public Type_NPC my_type;
    

    Transform tr;
    Transform right_spawn;
    Transform left_spawn;
    Transform pool;
    Transform npcs_parent;
    bool right_position;


    private void Awake()
    {
        tr = transform;

        if (my_type == Type_NPC.dementor || my_type == Type_NPC.lady_dementor)
        {
            right_spawn = GameManager.Instance.right_spawn_dementor;
            left_spawn = GameManager.Instance.left_spawn_dementor;
        }
        else if (my_type == Type_NPC.lady)
        {
            right_spawn = GameManager.Instance.right_spawn_lady;
            left_spawn = GameManager.Instance.left_spawn_lady;
        }

        pool = GameManager.Instance.pool;
        npcs_parent = GameManager.Instance.npcs_parent;
    }
   


    public void SendToPool(bool first_time)
    {
        on_pool = true;
        tr.position = pool.position;
        tr.SetParent(npcs_parent);

        if (!first_time)
        {
            CancelInvoke("SetToPool");
            GameManager.Instance.NewSpawned();
        }  
          
    }

    public void SetToSpawn(bool right)
    {
        on_pool = false;

        if (right)
        {
            tr.position = right_spawn.position;
            tr.eulerAngles = new Vector3(0, 0, 0);
            right_position = true;            
        }
        else
        {
            tr.position = left_spawn.position;
            tr.eulerAngles = new Vector3(0, 180, 0);
            right_position = false;
        }

        Invoke("SetToPool", Random.Range(min_time, max_time));
    }

    void Update() {        

        if(!on_pool){
            if(!right_position){
                if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) CompareMatch();          

                               
            }
            else {
                if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow)) CompareMatch();                  
            }
        }

    }

    private void CompareMatch()
    {

        if (my_type == Type_NPC.lady || my_type == Type_NPC.lady_dementor)
        {
            GameManager.Instance.SetPoints(GameManager.Instance.points_lady);
            SendToPool(false);
        }
        else if(my_type == Type_NPC.dementor)
        {
            GameManager.Instance.SetPoints(GameManager.Instance.points_dementor);
            SendToPool(false);
        }
    }

    private void SetToPool()
    {
        SendToPool(false);
    }


 
}
