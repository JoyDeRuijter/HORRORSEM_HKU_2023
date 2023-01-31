using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    [Header("Task UI")]
    [SerializeField] private GameObject taskBar;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private Image image;

    [Space(10)]
    [Header("(un)completed sprites")]
    [SerializeField] private Sprite completedSprite;
    [SerializeField] private Sprite uncompletedSprite;

    [Space(10)]
    [Header("Tasks")]
    public Task[] tasks;

    [SerializeField] private GameManager gameManager;
    private Task currentTask;
    private int currentTaskIndex = 0;

    private void Update()
    {
        StartCoroutine(CheckIfTaskIsCompleted());

    }

    private void ShowTaskBar()
    { 
        taskBar.SetActive(true);
    }

    public void HideTaskBar() 
    { 
        taskBar.SetActive(false); 
    }

    public IEnumerator CompleteTask(bool _shouldClose)
    { 
        image.sprite = completedSprite;
        currentTask.isCompleted = true;
        if (_shouldClose)
        { 
            yield return new WaitForSeconds(3f);
            HideTaskBar();        
        }
    }

    public void StartNewTask(int _taskIndex)
    {
        image.sprite = null;
        currentTaskIndex = _taskIndex;
        currentTask = tasks[_taskIndex];
        image.sprite = uncompletedSprite;
        textLabel.text = currentTask.taskDescription;
        ShowTaskBar();
    }

    private IEnumerator CheckIfTaskIsCompleted()
    {
        switch (currentTaskIndex)
        { 
            case 0:
                if (gameManager.playerRoomBlock != null && gameManager.playerRoomBlock.ID == 10)
                    StartCoroutine(CompleteTask(true));
                break;
            case 1:
                if (gameManager.playerRoomBlock != null && gameManager.playerRoomBlock.ID == 2)
                {
                    yield return new WaitForSeconds(6f);
                    StartCoroutine(CompleteTask(false));
                    //yield return new WaitForSeconds(3f);
                    if (tasks[1].isCompleted)
                    { 
                        StartNewTask(2);
                        break;
                    }
                }
                break;
            case 2:
                if (gameManager.playerRoomBlock != null && gameManager.playerRoomBlock.ID == 9)
                    StartCoroutine(CompleteTask(true));
                break;
            default:
                break;
        
        }
    }
}
