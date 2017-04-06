using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace context
{
    /// <summary>
    /// Controller class for management of floors. Also holds list of all floors.
    /// </summary>
    public class FloorManager : MonoBehaviour
    {
        public static int fid = 1;
        private List<FloorType> floors = new List<FloorType>();     // list of all floors in hotel

        void Awake()
        {
            DontDestroyOnLoad(gameObject);  // global gameObject across all scenes that never gets destroyed

            if (FindObjectsOfType(GetType()).Length > 1)
            {
                Destroy(gameObject);
            }
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
            floors.Add(floor);
            FinanceMgr.addRevenueSource(FinanceMgr.Floor, floor.revenues());
            //Queries.addFloor(fid, floor.getType(), FinanceMgr.tid, floor.getCost());
            fid++;

            //INSERT INTO FLOORS (pos, type, level) VALUES (floors.size() -1, floorType.type(), 0)
            //INSERT INTO Occupants (pos, num) VALUES(pos, 0)
            //tid += 1; //source = {floor : 0, upgrade : 1}
            //INSERT INTO Cost(tid, source, amt) VALUES (tid, 0, floorType.cost())
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
                if (floor != selectedFloor)
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
            int position = 0;
            Queries.removeFloor(position);
        }

    }
}