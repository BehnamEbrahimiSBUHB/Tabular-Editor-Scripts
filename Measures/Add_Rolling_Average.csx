// This script creates rolling average measures for a selected measure.
// It calculates 5-day, 10-day, and 20-day rolling averages.

if (Selected.Measures.Count != 1)
{
    Error("Please select exactly one measure.");
    return;
}

// Prompt the user to select a measure (even if one is pre-selected)
var measure = SelectMeasure();


int[] periods = { 5, 10, 20 }; // Array of rolling average periods

foreach (int period in periods)
{
    // Construct the new measure name
    string newMeasureName = $"{period}-Day Rolling Average of {measure.Name}";


    // Construct the DAX expression for the rolling average
    // Using the 'Calendar' table and 'Date' column – adjust if your table/column names are different    
    string daxExpression =
        $@"
        CALCULATE(
            AVERAGEX(
        DATESINPERIOD('Calendar'[Date], LASTDATE('Calendar'[Date]), -{period}, DAY),
                {measure.DaxObjectFullName}
            )
            )";


    try
    {
        // Add the new measure to the model
        measure.Table.AddMeasure(
            newMeasureName,
            FormatDax(daxExpression,shortFormat:true), // Format DAX using short format
            measure.DisplayFolder + "\\" + "RollingAverage" // Place the measure in a "RollingAverage" subfolder
        );
    }
    catch (Exception ex)
    {
        // Handle potential errors during measure creation
        Error($"Error creating measure '{newMeasureName}': {ex.Message}");
    }
}

// Inform the user that the measures have been created
Info($"Created {periods.Length} rolling average measures in table '{measure.Table.Name}'.");
