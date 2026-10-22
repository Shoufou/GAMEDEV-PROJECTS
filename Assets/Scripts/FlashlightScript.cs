using UnityEngine;
using UnityEngine.InputSystem;


public class FlashlightScript : MonoBehaviour
{
    public Transform spotLight;
    private Light light;
    private bool isOn = false;

    private RaycastHit hit;
    private Ray ray;
    private MeshRenderer buffRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.light = spotLight.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (isOn)
        {
            ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(ray, out hit, 100f))
            {
                checkforHit(hit);
                Debug.DrawLine(ray.origin, hit.point, Color.green);
            }

            else
            {
                Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
            }
        }
        
    }

    private void checkforHit(RaycastHit hit)
    {
        if (hit.transform.tag == "Buffs")
        {
            buffRenderer = hit.transform.GetComponent<MeshRenderer>();
            buffRenderer.enabled = true;
            Debug.Log("Buff found!");
        }
    }

    public void onLeftClick(InputAction.CallbackContext context)
    {

        if (context.started)
        {
            light.enabled = !light.enabled;
            isOn = !isOn;
        }
    }
}
