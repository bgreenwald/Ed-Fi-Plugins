using EdFi.Dashboards.Resources.Models.CustomGrid;

namespace EdFi.Dashboards.Plugins.NWEA.Resources.Models.CustomGrid
{
    public class GoalStrandCellItem<TValue> : CellItem<TValue>
    {
        /// <summary>
        /// The State for the goal strand.
        /// </summary>
        public int GS { get; set; }
    }
}
