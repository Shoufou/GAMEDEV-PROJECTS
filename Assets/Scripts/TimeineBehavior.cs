using UnityEngine;
using UnityEngine.Playables;

public class TimeineBehavior : MonoBehaviour
{
    private PlayableDirector director;

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }

    private void OnEnable()
    {
        director.stopped += OnTimelineStopped;
    }

    private void OnDisable()
    {
        director.stopped -= OnTimelineStopped;
    }

    private void OnTimelineStopped(PlayableDirector obj)
    {
       
        Time.timeScale = 0f;
    }
}
