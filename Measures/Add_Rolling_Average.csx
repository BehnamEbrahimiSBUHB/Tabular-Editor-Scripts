if (Selected.Measures.Count != 1)
{
    Error("Please select exactly one measure.");
    return;
}

var measure = SelectMeasure();

int[] periods = { 5, 10, 20 };

foreach (int period in periods)
{
    string newMeasureName = $"{period}-Day Rolling Average of {measure.Name}";
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
        measure.Table.AddMeasure(
            newMeasureName,
            FormatDax(daxExpression,shortFormat:true),
            measure.DisplayFolder + "\\" + "RollingAverage"
        );
    }
    catch (Exception ex)
    {
        Error($"Error creating measure '{newMeasureName}': {ex.Message}");
    }
}


Info($"Created {periods.Length} rolling average measures in table '{measure.Table.Name}'.");
