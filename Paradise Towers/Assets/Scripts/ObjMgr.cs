using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Diagnostics;

using Mono.Data.Sqlite;
using System.Data;
using System.Linq;

namespace context {

	public class ObjMgr : MonoBehaviour {

		public GameObject window;
		public Text currObj;
		public Text currProgress;

		public Text protoItem;
		public GameObject listView;	
		public static List<Text> boxes = new List<Text>();

		public static Stopwatch timer = new Stopwatch();
		public static long lastTime = 0;

		void Awake(){

//			if (firstRun == true) {
//				firstRun = false;
//				ObjMgr.objects.Clear ();
//				var objectives = Queries.loadObjectives ();
//
//				// Loads the objectives into the scrollview
//				if (objectives != null) {
//					int i = 0;
//					bool first = true;
//					foreach (Objective obj in objectives) {
//						objects.Add (obj);
//						if (i == 0) {
//							i++;
//							continue;
//						}
//
//						Text box;
//						if (first == true) {
//							box = protoItem;
//							first = false;
//						} else {
//							box = Instantiate (protoItem);
//						}
//
//						box.transform.SetParent (listView.transform);
//						box.text = obj.caption + "\t\t\t\t" + obj.progress + "%";
//						boxes.Add (box);
//					}
//				}
//			}
		}

		// Use this for initialization
		void Start () {
			timer.Start ();
//			var objectives = Queries.loadObjectives ();
//			
//							// Loads the objectives into the scrollview
//			if (objectives != null) {
//				int i = 0;
//				bool first = true;
//				foreach (Objective obj in objectives) {
//					objects.Add (obj);
//					if (i == 0) {
//						i++;
//						continue;
//					}
//			
//					Text box;
//					if (first == true) {
//						box = protoItem;
//						first = false;
//					} else {
//						box = Instantiate (protoItem);
//					}
//			
//					box.transform.SetParent (listView.transform);
//					box.text = obj.caption + "\t\t\t\t" + obj.progress + "%";
//					boxes.Add (box);
//				}
//			}
//			currProgress.text = objects.ElementAt (0).progress + "%";
//			currObj.text = objects.ElementAt (0).caption;
		}

		// Update is called once per frame
		void Update () {
			
			if (timer.ElapsedMilliseconds - lastTime < 3000) {
				return;
			}
			lastTime = timer.ElapsedMilliseconds;


//			for (int i = 1; i < objects.Count (); i++) {
//				Objective obj = objects.ElementAt (i);
//				string stmt = objects.ElementAt (i).query;
//				int result = Queries.execute (stmt);
//				UnityEngine.Debug.Log (stmt);
//				UnityEngine.Debug.Log ("result" + result.ToString());
//
//				int status;
//				if (result > 0)
//					status = result / obj.condition;
//				else
//					status = 0;
//
//				Text box = Instantiate (protoItem);
//				box.transform.SetParent (listView.transform);
//				boxes.Add (box);
//				if (status >= 1) {
//					objects.ElementAt (i).progress = 100;
//					box.text = obj.caption + "\t\t\t\t100%";
//				} else {
//					objects.ElementAt (i).progress = status * 100;
//					box.text = obj.caption + "\t\t\t\t" + (status * 100).ToString() + "%";
//				}
			// Loads the objectives into the scrollview

			List<Objective> objectives = Queries.loadObjectives ();
			//var objectives = Queries.loadTransactions ();
			listView.transform.DetachChildren();

			if (objectives != null) {
				int i = 0;
				bool first = true;
				foreach (Objective obj in objectives){
					string stmt = obj.query;
					int result = Queries.execute (stmt);
					int status;
					if (result > 0)
						status = result / obj.condition;
					else
						status = 0;
					Text box;
					if (first == true) {
						box = protoItem;
						first = false;
					} else {
						box = Instantiate (protoItem);
						box.transform.SetParent (listView.transform);
						boxes.Add (box);
					}
					if (status >= 1)
						box.text = obj.caption + "\t\t\t\t100%";
					else
						box.text = obj.caption + "\t\t\t\t" + (status * 100).ToString() + "%";
					i++;
				}
			
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
	public string query;
	public int id;
	public int condition;

	public Objective(int id, string caption, int progress, int reward, string query, int condition){
		this.id = id;
		this.caption = caption;
		this.progress = progress;
		this.query = query;
		this.reward = reward;
		this.condition = condition;

	}

}
