using UnityEngine;

public class FaceCamY : MonoBehaviour
{
    Transform camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.transform;
    }

    // Update is called once per frame
    //void LateUpdate()
    //{
    //    if (camera == null)
    //    {
    //        camera = Camera.main.transform;
    //    }
    //    transform.rotation = Quaternion.Euler(transform.rotation.x, Quaternion.LookRotation(camera.position - transform.position).y, transform.rotation.z);
    //}

    private void Update()
    {
        if (camera == null)
        {
            camera = Camera.main.transform;
        }
        transform.rotation = camera.rotation;
    }
}