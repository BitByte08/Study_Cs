using UnityEngine;

public class moveCamera : MonoBehaviour
{
    public Transform cameraPosition;
    private void Update()
    {
        this.transform.position = cameraPosition.position;    
    }
}
