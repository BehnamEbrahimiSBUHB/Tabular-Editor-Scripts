// This script creates a Year-over-Year (YoY) measure for a selected measure.

if (Selected.Measures.Count != 1)
{
    Error("Please select exactly one measure.");
    return;
}

var measure = SelectMeasure(); // Prompt user to select a measure

string yoyMeasureName = $"{measure.Name} YoY";

// Construct the DAX expression for the YoY calculation
// Uses the 'Calendar'[Date] column. Adjust if your date table/column names are different.
string daxExpression = FormatDax($@"
VAR CurrentYearValue = CALCULATE({measure.DaxObjectFullName}, SAMEPERIODLASTYEAR('Calendar'[Date]))
VAR PreviousYearValue = CALCULATE({measure.DaxObjectFullName}, PARALLELPERIOD('Calendar'[Date], -1, YEAR))
RETURN
DIVIDE(CurrentYearValue - PreviousYearValue, PreviousYearValue)
", shortFormat: true);

try
{
    measure.Table.AddMeasure(
        yoyMeasureName,
        daxExpression,
        measure.DisplayFolder + "\\" + "YoY" // Place the measure in a "YoY" subfolder
    );


    // Set the format string to percentage
    var yoyMeasure = measure.Table.Measures[yoyMeasureName]; // Get the newly created measure
    yoyMeasure.FormatString = "0.00%"; // Format as percentage


}
catch (Exception ex)
{
    Error($"Error creating measure '{yoyMeasureName}': {ex.Message}");
}

Info($"Created YoY measure '{yoyMeasureName}' in table '{measure.Table.Name}'.");