using UnityEngine;
using TMPro;

public class UIManager: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private TextMeshProUGUI hp;

    private void OnEnable()
    {
        MovementController.updateHealthUI.AddListener(UpdateHealthDisplay);
    }

    private void OnDisable()
    {
        MovementController.updateHealthUI.RemoveListener(UpdateHealthDisplay);
    }


    public void resetTimer()
    {
        timer.text = null;
    }

    public void updateTime(int time)
    {
        timer.text = "Time Remaining: " + time + " seconds";
    }

    private void UpdateHealthDisplay(int currentHealth)
    {
        if (hp != null)
        {
            hp.text = "HP: " + currentHealth;
        }
    }

}
