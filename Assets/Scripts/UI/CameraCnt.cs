using Cinemachine;
using UnityEngine;

public class CameraCnt : MonoBehaviour
{
    private CinemachineBrain cinemachine;
    void Start()
    {
        cinemachine = GetComponent<CinemachineBrain>();
        cinemachine.enabled = false;
    }

    void Update()
    {

        if (Input.GetMouseButton(1))
        {
            cinemachine.enabled = true;
            return;
        }

        cinemachine.enabled = false;

    }
}
