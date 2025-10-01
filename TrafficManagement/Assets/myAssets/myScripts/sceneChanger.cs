using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChanger : MonoBehaviour
{
    // Start is called before the first frame update

    private void Start()
    {
        //overridestart
    //    CallScene(1);
    }
    public void CallScene(int sceneNum)

    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(sceneNum);

    }

    public void SetPos(int pos){

        PlayerPrefs.SetInt("pos", pos);
        Debug.Log(PlayerPrefs.GetInt("pos"));
    }
}
