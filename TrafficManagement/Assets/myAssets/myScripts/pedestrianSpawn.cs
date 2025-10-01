using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pedestrianSpawn : MonoBehaviour
{

    public List <GameObject> pedesTrianPrefab;
    public int pedestriansToSpawn;

    int spawnNum;
    int targetNumi;

    // Start is called before the first frame update
    void Start()
    {
        spawnNum = pedesTrianPrefab.Count-1;

       // Debug.Log(spawnNum);

        StartCoroutine(Spawn());

    }

    IEnumerator Spawn() {

        int count = 0;

       
      //  Debug.Log(targetNumi);

        while (count < pedestriansToSpawn) {

            targetNumi = Random.Range(0, spawnNum);

            // Debug.Log(targetNumi);

            yield return new WaitForSeconds(0.2f);
            GameObject obj = Instantiate(pedesTrianPrefab[targetNumi]);

            int targetNum = Random.Range(count, pedestriansToSpawn);

            if (count == targetNum) {
//though of changing color?
             //  GameObject grandChild = obj.gameObject.transform.GetChild(0).GetChild(0).gameObject;

                //Debug.Log(grandChild.name.ToString());

             //   grandChild.GetComponent<SkinnedMeshRenderer>().materials[0].color = Color.red;
               
                obj.tag = "Untagged";

            }


            Transform child = transform.GetChild(Random.Range(0, transform.childCount - 1));

            obj.GetComponent<waypointNavigator>().currentWaypoint = child.GetComponent<Waypoint>();
            obj.transform.position = child.position;

            yield return new WaitForEndOfFrame();

            count++;



        
        }
    
    }
}
