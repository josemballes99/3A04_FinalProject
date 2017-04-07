using System;
using System.Collections.Generic;
using UnityEngine;

namespace context
{
    public class FloorType
    {
        public static readonly FloorType Arcade = new FloorType(0, 25, 100);
        public static readonly FloorType Restaurant = new FloorType(1, 30, 100);
        public static readonly FloorType Suite = new FloorType(2, 40, 100);
        public static readonly FloorType Lobby = new FloorType(3, 0, 0);

		public static Dictionary<int, string> floorMap;
		public static Dictionary<int, int> upgrades;

        public Guid InstanceID { get; private set; }
		public static int fid = 0;

        private static int[] floors;
		private int level = 0;
        private int revenue;
        private readonly int type;
        private readonly int cost;
        public int id;
        private bool isVisible;         // whether or not to show the floor on screen

		static FloorType () {
			floorMap = new Dictionary<int, string>();
			floorMap.Add(0, "Arcade");
			floorMap.Add(1, "Restaurant");
			floorMap.Add(2, "Suite");
			floorMap.Add(3, "Lobby");

			upgrades = new Dictionary<int, int>();
			upgrades.Add (1, 2000);
			upgrades.Add (2, 10000);
			upgrades.Add (3, 30000);
		}
			
		public FloorType(int type, int revenue, int cost)
		{
			this.InstanceID = Guid.NewGuid();
			this.type = type;
			this.revenue = revenue;
			this.cost = cost;
			floors = new int[3] { 0, 0, 0 };
			if (type != 3)
			{
				FloorType.floors[type]++;
			}
			this.id = fid;
			fid++;
		}

        public void setVisibility(bool v)
        {
            isVisible = v;
        }

        public bool getVisibility()
        {
            return isVisible;
        }

        public int getCost()
        {
            return cost;
        }

        public int getType()
        {
            return type;
        }

        public int revenues()
        {
            return revenue;
        }

		public int tier(){
			return level;
		}

		public void upgrade(){
			level += 1;
		}

        public override bool Equals(object obj)
        {
            FloorType other = obj as FloorType;
            return this.type == other.type;
        }

        public override int GetHashCode()
        {
            int prime = 17;

            int hash = (prime * 31) % (this.type + 3);
            hash = (hash * 31) % (this.id + 3);
            return hash;
        }

		public override string ToString ()
		{
			return string.Format ("[FloorType: InstanceID={0}]", InstanceID);
		}

    }
}


