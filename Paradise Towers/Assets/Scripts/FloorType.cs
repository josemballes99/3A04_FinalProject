using System;
using UnityEngine;

namespace context
{
    public class FloorType
    {
        public static readonly FloorType Arcade = new FloorType(0, 25, 100);
        public static readonly FloorType Restaurant = new FloorType(1, 30, 100);
        public static readonly FloorType Suite = new FloorType(2, 40, 100);

        private static int[] floors;

        private int revenue;
        private readonly int type;
        private readonly int cost;
        private int id;
        private bool isVisible;         // whether or not to show the floor on screen

        public FloorType(int type, int revenue, int cost)
        {
            this.type = type;
            this.revenue = revenue;
            this.cost = cost;
            floors = new int[3] { 0, 0, 0 };
            FloorType.floors[type]++;
            this.id = FloorType.floors[type];
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

    }
}


