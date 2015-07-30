/// <reference path="/Core_Content/Scripts/Application/EdFiGrid.js"/>

edfiGridPluginColumnTemplateAndClass.push(function (prop, hidden, dataTemplates, dataClasses, headerClasses) {
    if (prop === 'GS') {
        dataTemplates.push($.template("EdFi.Dashboards.Plugins.NWEA.Web.Content.HtmlTemplates.EdFiGrid.goalStrandCellTemplate"));
        dataClasses.push(hidden + "EdFiGrid-table-cell EdFiGrid-table-cell-objective");
        headerClasses.push(hidden + "EdFiGrid-table-cell EdFiGrid-table-cell-objective");
        return true;
    }
    return false;
});