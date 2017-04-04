using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

namespace context {

	public class FinanceMgr : MonoBehaviour
	{
		public GameObject window;
		public Text revText, expText, incText;

		private readonly TimeSpan origin = DateTime.Now.TimeOfDay;

		public static int Floor = 0;
		public static int Customer = 1;

		static int[] revenueSource = new int[2]{0,0};
		static int[] expenseSource = new int[2]{0,0};
		static int revenuePerMinute = 0;
		static int expensePerMinute = 0;
	
		public static int incomePerMinute (){
			return (revenuePerMinute - expensePerMinute);
		}

		public static int revenues(){
			return revenuePerMinute; // * time elapsed
		}

		public static int expenses(){
			return expensePerMinute; // * time elapsed
		}

		void Awake (){
			//window.SetActive (false);
		}

		// Use this for initialization
		void Start ()
		{
			revText.text = revenues().ToString();
			expText.text = expenses().ToString();
			incText.text = (revenues() - expenses ()).ToString();
		}

		//Add data structure to track financial events so we can put them in the scrollview

		public static void addRevenueSource(int source, int amt){
			revenuePerMinute += amt;
			revenueSource [source]++;
		}

		public static void addExpenseSource(int source, int amt){
			expensePerMinute += amt;
			expenseSource [source]++;
		}

		// Update is called once per frame
		void Update ()
		{
			revenuePerMinute+=2;
			expensePerMinute++;
			revText.text = revenues().ToString();
			expText.text = expenses().ToString();
			incText.text = (revenues() - expenses ()).ToString();
		}
	}

}