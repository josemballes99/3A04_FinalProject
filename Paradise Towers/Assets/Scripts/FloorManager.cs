using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

using Mono.Data.Sqlite;
using System.Data;

namespace context
{
    /// <summary>
    /// Controller class for management of floors. Also holds list of all floors.
    /// </summary>
    public class FloorManager : MonoBehaviour
    {
        public static int fid = 1;
        private List<FloorType> floors = new List<FloorType>();     // list of all floors in hotel
        public List<Tuple<string, string>> customers = new List<Tuple<string, string>>();     // key = floor, value = customer

        void Awake()
        {
            DontDestroyOnLoad(gameObject);  // global gameObject across all scenes that never gets destroyed

            if (FindObjectsOfType(GetType()).Length > 1)
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            floors.Add(FloorType.Lobby);
        }

        /**
         * Returns list of all floors in the hotel.
         */
        public List<FloorType> getFloors()
        {
            return floors;
        }

        /**
         * Adds new floor to hotel.
         */
        public void addFloor(FloorType floor)
        {
            int arcadeNum = 0;
            int restaurantNum = 0;
            int suiteNum = 0;

			IDbConnection connection2 = Queries.connect (Queries.dbURL);
			IDbCommand command2 = connection2.CreateCommand();
			command2.CommandText = "SELECT SUM(amt) FROM Income";
			command2.ExecuteNonQuery();
			IDataReader reader = command2.ExecuteReader();
			int sum = 0;
			while (reader.Read ()) {
				if (!reader.IsDBNull(0)) {
					sum = reader.GetInt32 (0);
				}
			}
			command2.Dispose();
			command2 = null;
			connection2.Close();
			if (sum < floor.getCost ()) {
				new MobileNativeMessage("Insufficient funds", "Expand your hotel to make more money");
				return;
			}


            foreach(FloorType f in floors)
            {
                if (f.getType() == 0)
                {
                    arcadeNum++;
                } else if (f.getType() == 1)
                {
                    restaurantNum++;
                } else if (f.getType() == 2)
                {
                    suiteNum++;
                }
            }

            if (floor.getType() == 0 && arcadeNum == 0)
            {
                floors.Add(floor);
                Queries.addFloor(fid, floor.getType(), FinanceMgr.tid, floor.getCost());
                FinanceMgr.addFloor(floor);
                fid++;
                new MobileNativeMessage("Floor Added", "Arcade floor added.");
            } else if (floor.getType() == 1 && restaurantNum == 0)
            {
                floors.Add(floor);
                Queries.addFloor(fid, floor.getType(), FinanceMgr.tid, floor.getCost());
                FinanceMgr.addFloor(floor);
                fid++;
                new MobileNativeMessage("Floor Added", "Restaurant floor added.");
            }
            else if (floor.getType() == 2 && suiteNum < 2)
            {
                floors.Add(floor);
                Queries.addFloor(fid, floor.getType(), FinanceMgr.tid, floor.getCost());
                FinanceMgr.addFloor(floor);
                fid++;
                new MobileNativeMessage("Floor Added", "Suite floor added.");
            } else
            {
                new MobileNativeMessage("Excessive Number of Floors", "The floor was not added as the number of floors is already at maximum capacity.");
            }
        }

        /**
         * Sets the selected floor to be active/visible.
         */
        public void selectFloor(FloorType selectedFloor)
        {
            selectedFloor.setVisibility(true);

            // deactivate all other floors
            foreach (FloorType floor in floors)
            {
                if (selectedFloor != floor)
                {
                    floor.setVisibility(false);
                }
            }
        }

        /**
         * Removes the floor that is given to it.
         */
        public void removeFloor(FloorType floor)
        {
            floors.Remove(floor);
			Queries.removeFloor(floor.id);
        }

        public void removeCustomers(string floorName)
        {
            customers.RemoveAll(item => item.Item1 == floorName);
        }

    }
}