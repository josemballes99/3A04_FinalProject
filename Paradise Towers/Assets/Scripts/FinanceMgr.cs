using UnityEngine;
using System.Collections;
using System;

namespace context {

	public class FinanceMgr : MonoBehaviour
	{

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

		// Use this for initialization
		void Start ()
		{

		}


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
	
		}
	}

}