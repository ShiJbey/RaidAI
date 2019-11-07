using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActorController : MonoBehaviour
{

    public float attackRange = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position += new Vector3(Input.GetAxis("Horizontal") * 0.1f, 0.0f, 0.0f);
        this.gameObject.transform.position += new Vector3(0.0f, 0.0f, Input.GetAxis("Vertical") * 0.1f);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> enemiesInRange = new List<GameObject>();

        if (Input.GetKeyUp("tab"))
        {
            foreach (GameObject g in enemies)
            {
                if (Vector3.Distance(this.transform.position, g.transform.position) <= attackRange)
                {
                    //gameObject.SetActive(false);
                    //enemiesInRange.Add(g);
                    Destroy(g);
                }
            }
        }
    }
}
