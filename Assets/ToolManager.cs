using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    public GameObject[] tools;
    public bool[] unlockedTools;
    int currentTool;

    private void Update() 
    {
        if(Input.GetKeyDown("1") && unlockedTools[0] == true && currentTool != 0) StartCoroutine("ChangeTool", 0);
        if(Input.GetKeyDown("2") && unlockedTools[1] == true && currentTool != 1) StartCoroutine("ChangeTool", 1);
        if(Input.GetKeyDown("3") && unlockedTools[2] == true && currentTool != 2) StartCoroutine("ChangeTool", 2);
        if(Input.GetKeyDown("4") && unlockedTools[3] == true && currentTool != 3) StartCoroutine("ChangeTool", 3);

    }
    // void ChangeTool(int tool)
    // {
    //     tools[currentTool].SetActive(false);
    //     currentTool = tool;
    //     tools[currentTool].SetActive(true);
    // }
    public void UnlockTool(int tool)
    {
        unlockedTools[tool] = true;
        StartCoroutine("ChangeTool", tool);
    }

    IEnumerator ChangeTool(int tool)
    {
        tools[currentTool].gameObject.GetComponent<Animator>().Play("Disaquip");
        yield return new WaitForSeconds(tools[currentTool].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime);
        tools[currentTool].SetActive(false);
        currentTool = tool;
        tools[currentTool].SetActive(true);
        tools[currentTool].gameObject.GetComponent<Animator>().Play("Equip");



    }
}