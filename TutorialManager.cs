using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    public List<GameObject> phases;
    public GameObject next_button;


    private byte phase_index;

    
    public void IncrementPhase()
    {
        phase_index++;
        Activate(phase_index);
    }

    public void ResetPhase(){
        phase_index = 0;
        phases[phases.Count-1].SetActive(false);
        phases[phase_index].SetActive(true);
        next_button.SetActive (true);
    }

	
    private void Activate(byte index)
    {
        phases[index - 1].SetActive(false);
        phases[index].SetActive(true);
        if (index == phases.Count - 1) next_button.SetActive(false);       
    }
   

}
