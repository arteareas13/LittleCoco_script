using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScene_start : MonoBehaviour {

	public GameObject player;
	public GameObject item_Canvas;
	public GameObject dialogue_Canvas;
	public GameObject nosaved_Panel;
	public GameObject restart_Check;
	public GameObject[] _Controllers;

	private Item_Controller ic;
	private Text_Importer ti;

	void Awake(){
		Screen.SetResolution (Screen.width, Screen.width * 9 / 16, true);
	}

	public void Change_Scene(string scene_name){
		SceneManager.LoadScene (scene_name);
	}

	public void Load_lastGame(){
		
		if (PlayerPrefs.GetInt ("Restart_SceneNum") != 0) {
			restart_Check.SetActive (true);
		} else {
			nosaved_Panel.SetActive (true);
		}
	}

	public void Restart(){
		if (PlayerPrefs.GetInt ("Restart_SceneNum") != 0 && PlayerPrefs.GetInt("Restart_SceneNum") != 3) {
			player.SetActive (true);
			item_Canvas.SetActive (true);
			DontDestroyOnLoad (dialogue_Canvas);

			SceneManager.LoadScene (PlayerPrefs.GetInt ("Restart_SceneNum"));

			ic = item_Canvas.GetComponent<Item_Controller> ();
			ic._item_name_list = PlayerPrefsX.GetStringArray ("IC_nameList");
			ic._usable_item = PlayerPrefsX.GetBoolArray ("Usable_item");
			ic._the_number_of_items = PlayerPrefsX.GetIntArray ("NumOfItem");
			ic._interaction_object = PlayerPrefsX.GetStringArray ("Interaction");
			ic._consumable = PlayerPrefsX.GetBoolArray ("Consumable");
			for (int xx = 0; xx < ic._item_list.Length; xx++) {
				if (ic._item_name_list [xx] != "") {
					Texture2D assas = (Texture2D)Resources.Load ("ItemPictures/" + ic._item_name_list [xx]);
					Rect r = new Rect (0, 0, assas.width, assas.height);
					ic._item_list [xx].GetComponent<Image> ().sprite = Sprite.Create (assas, r, new Vector2 (0, 0));
					ic._item_list [xx].GetComponent<Image> ().color = new Color (1,1,1,1);
					ic._item_list [xx].transform.parent.GetComponentInChildren<Text> ().text = ic._the_number_of_items [xx].ToString();
					ic._item_list [xx].transform.parent.GetComponentInChildren<Text> ().color = new Color (1, 1, 1, 1);
				}
			}


			ti = dialogue_Canvas.GetComponent<Text_Importer> ();
			if (PlayerPrefs.GetInt ("Restart_SceneNum") >= 4 && PlayerPrefs.GetInt ("Restart_SceneNum") <= 10) {
				ti.Import (4);
				_Controllers [0].SetActive (true);
				DontDestroyOnLoad (_Controllers [0]);
			}
			if (PlayerPrefs.GetInt ("Restart_SceneNum") >= 11 && PlayerPrefs.GetInt ("Restart_SceneNum") <= 14) {
				ti.Import (11);
				_Controllers [1].SetActive (true);
				DontDestroyOnLoad (_Controllers [1]);
				//음악을 달아야 한다.
			}
			ti.currLineArr = PlayerPrefsX.GetIntArray ("CurrArr");

			Stage1_Controller._Stage1_Quest = PlayerPrefsX.GetBoolArray ("Stage1_Quest");
			Stage2_Controller._Stage2_Quest = PlayerPrefsX.GetBoolArray ("Stage2_Quest");
			Stage2_Controller._Stage2_Quest_intArr = PlayerPrefsX.GetIntArray ("Stage2_Quest_INT");

//			if (PlayerPrefs.GetInt ("Restart_SceneNum") >= 11 && PlayerPrefs.GetInt ("Restart_SceneNum") <= 14) {
//				ti.Import (11);
//			}


		}

		if (PlayerPrefs.GetInt ("Restart_SceneNum") == 3) {
			//stage select로 넘어갈 때는
			//안할경우 퀘스트 목록이 지워짐.
			player.SetActive (true);
			item_Canvas.SetActive (true);
			SceneManager.LoadScene (PlayerPrefs.GetInt ("Restart_SceneNum"));
		}
	}


}
