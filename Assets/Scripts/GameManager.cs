using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] List<playerScript_ex00> characterControllers;

    int currentCharacter = 1;

    [SerializeField] string nextLevel = "";

    // Start is called before the first frame update
    void Start()
    {
        ChooseCharacter(1);
    }

    // Update is called once per frame
    void Update()
    {
        CameraFollow();

        CheckIfPlayersAreAtExits();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChooseCharacter(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChooseCharacter(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChooseCharacter(3);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void ChooseCharacter(int charNum)
    {
        characterControllers[currentCharacter - 1].enabled = false;
        currentCharacter = charNum;
        characterControllers[currentCharacter - 1].enabled = true;
    }

    void CameraFollow()
    {
        mainCamera.transform.position = new Vector3(characterControllers[currentCharacter - 1].transform.position.x,
                                            characterControllers[currentCharacter - 1].transform.position.y,
                                            mainCamera.transform.position.z);
    }

    void CheckIfPlayersAreAtExits()
    {
        foreach (playerScript_ex00 controller in characterControllers)
        {
            if (controller.atExit == false)
                return;
        }
        Debug.Log("All players have reached their exit!");

        if (nextLevel != "")
        {
            SceneManager.LoadScene(nextLevel);
        }
    }
}
