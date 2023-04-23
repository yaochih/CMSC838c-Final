using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genObject : MonoBehaviour
{
    public GameObject gen_region;
    public List<GameObject> bullets = new List<GameObject>();
    private Vector3[] corners = new Vector3[4];
    public Transform target;
    private int bullet_num;
    private float next_fire;

    // Start is called before the first frame update
    void Start()
    {
        MeshFilter meshFilter = gen_region.GetComponent<MeshFilter>();
        Vector3 planeSize = meshFilter.mesh.bounds.size;

        float halfWidth = planeSize.x / 2;
        float halfLength = planeSize.z / 2;
        // top_right
        corners[0] = gen_region.transform.TransformPoint(new Vector3(-halfWidth, 0, -halfLength));
        // top_left
        corners[1] = gen_region.transform.TransformPoint(new Vector3(halfWidth, 0, -halfLength));
        // button_left
        corners[2] = gen_region.transform.TransformPoint(new Vector3(halfWidth, 0, halfLength));
        // button_right
        corners[3] = gen_region.transform.TransformPoint(new Vector3(-halfWidth, 0, halfLength));

        bullet_num =  corners.Length;
        StartCoroutine("Fire");
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= next_fire){
            Fire();
            next_fire = Time.time + 3.0f;
        }
    }

    Vector3 Randpos(){
        Vector3 pos;
        pos = new Vector3(Random.Range(corners[0].x, corners[1].x),  Random.Range(corners[3].y, corners[0].y), corners[0].z-5.0f);

        return pos;
    }

    void Fire() {
        int idx = Random.Range(0, bullet_num-1);
        Debug.Log(idx);
        GameObject bullet;
        Vector3 pos = Randpos();
        bullet = Instantiate(bullets[idx], pos, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = (target.position - bullet.transform.position).normalized * 80.0f;
        // yield return new WaitForSeconds(3);
    } 
}
