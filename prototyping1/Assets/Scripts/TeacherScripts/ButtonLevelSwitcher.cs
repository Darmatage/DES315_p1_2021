using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLevelSwitcher : MonoBehaviour
{
	public string GoToScene;

    public void SceneChange(){
		SceneManager.LoadScene("" + GoToScene);
    }
}
