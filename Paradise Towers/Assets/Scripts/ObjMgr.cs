using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace context {

public class ObjMgr : MonoBehaviour {

	public GameObject window;
	public Text currProgress;
	public ScrollRect rect;

	public int current;
	
	public static Dictionary<int, Objective> objects = new Dictionary<int, Objective> ();

	
	void Awake(){
			TextAsset data = Resources.Load("objectives.csv") as TextAsset;
			string[] objList = data.text.Split (new string[] { Environment.NewLine }, StringSplitOptions.None);

			foreach (string obj in objList) {
				string[] fields = obj.Split (',');
				int id, progress, reward;

				int.TryParse (fields [0], out id);
				string caption = fields [1];
				int.TryParse (fields [2], out progress);
				int.TryParse (fields [3], out reward);
			}

//			var Lines = File.ReadLines("test.csv").Select(a => a.Split(';'));
//			var CSV = from line in Lines 
//				select (line.Split(',')).ToArray();
	}

		public void test(){
			var x = "";
		}

	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {
		
	}

		public void Save(){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/object.log", FileMode.Create);
			Objective newData = new Objective();
			newData.caption;
			newData.progress;
			newData.reward;
			newData.id;

			bf.Serialize(file, newData);
			file.Close();
		}

		public void Load(){

			if (File.Exists(Application.persistentDataPath + "/object.log")){
				BinaryFormatter bf = new BinaryFormatter();
				FileStream file = File.Open(Application.persistentDataPath + "/object.log", FileMode.Open);
				Objective newData = (Objective)bf.Deserialize(file);
				file.Close();
				caption;
				progress;
				reward;
				id;
			}
		}
	}
}

[Serializable]
public class Objective
{

	public string caption;
	public int reward;
	public int progress;
	public int id;

	public Objective(int id, string caption, int progress, int reward){
		this.id = id;
		this.caption = caption;
		this.progress = progress;
		this.reward = reward;
	}

}