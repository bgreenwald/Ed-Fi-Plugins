function getGoalStrandCSS(state) {
    switch (state) {
        case 1:
            return "EdFiGrid-legend-circle EdFiGrid-legend-good";
        case 2:
            return "EdFiGrid-legend-rhombus EdFiGrid-legend-caution";
        case 3:
            return "EdFiGrid-legend-rhombus EdFiGrid-legend-bad";
        case 7:
            return "EdFiGrid-legend-square EdFiGrid-legend-verybad";
        case 6:
            return "EdFiGrid-legend-circle EdFiGrid-legend-verygood";
        default:
            return "RoundStateNone";
    }
}
