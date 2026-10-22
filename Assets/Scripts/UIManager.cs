using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private TextMeshProUGUI detectionStatus;

    public void resetTimer()
    {
        timer.text = null;
    }

    public void resetdetectionStatus()
    {
        detectionStatus.text = null;
    }



    public void updateTime(int time)
    {
        timer.text = "Time Remaining: " + time + " seconds";
    }

    public void updatedetectionStatus(int time)
    {
        detectionStatus.text = "You've been spotted! You'll be found in: " + time + " seconds!";
    }
}
