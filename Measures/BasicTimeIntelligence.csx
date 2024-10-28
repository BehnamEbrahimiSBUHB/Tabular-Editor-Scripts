// This script creates basic time comparison measures for all selected measures,
// organizing them into separate folders for each time comparison type.

if (Selected.Measures.Count == 0)
{
    Error("Please select one or more measures.");
    return;
}

string dateTable = "'Calendar'[Date]"; // **Important:** Replace with your date table and column

foreach (var measure in Selected.Measures)
{
    var tableName = measure.Table.Name;
    var measureName = measure.Name;
    var baseFolder = measure.DisplayFolder + "\\Time Comparisons";

    List<Measure> newMeasures = new List<Measure>();


    // --- YTD ---
    string ytdMeasureName = $"{measureName} YTD";
    string ytdDax = $@"CALCULATE({measure.DaxObjectFullName}, DATESYTD({dateTable}))";
    newMeasures.Add(measure.Table.AddMeasure(ytdMeasureName, ytdDax, baseFolder + "\\YTD"));


    // --- QTD ---
    string qtdMeasureName = $"{measureName} QTD";
    string qtdDax = $@"CALCULATE({measure.DaxObjectFullName}, DATESQTD({dateTable}))";
    newMeasures.Add(measure.Table.AddMeasure(qtdMeasureName, qtdDax, baseFolder + "\\QTD"));


    // --- MTD ---
    string mtdMeasureName = $"{measureName} MTD";
    string mtdDax = $@"CALCULATE({measure.DaxObjectFullName}, DATESMTD({dateTable}))";
    newMeasures.Add(measure.Table.AddMeasure(mtdMeasureName, mtdDax, baseFolder + "\\MTD"));


    // --- Previous Period ---
    string prevPeriodMeasureName = $"{measureName} Previous Period";
    string prevPeriodDax = $@"CALCULATE({measure.DaxObjectFullName}, PREVIOUSMONTH({dateTable}))"; // Or PREVIOUSDAY, etc.
    newMeasures.Add(measure.Table.AddMeasure(prevPeriodMeasureName, prevPeriodDax, baseFolder + "\\Previous Period"));


    // --- Same Period Last Year (SPLY) ---
    string splyMeasureName = $"{measureName} SPLY";
    string splyDax = $@"CALCULATE({measure.DaxObjectFullName}, SAMEPERIODLASTYEAR({dateTable}))";
    newMeasures.Add(measure.Table.AddMeasure(splyMeasureName, splyDax, baseFolder + "\\SPLY"));



    // Format all new measures for the current original measure at once
    // FormatDax(newMeasures, shortFormat: true);

    Info($"Created time comparison measures for '{measureName}' in table '{tableName}'.");
}