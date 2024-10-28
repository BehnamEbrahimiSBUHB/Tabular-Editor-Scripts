// This script creates basic time comparison measures for a selected measure.

if (Selected.Measures.Count != 1)
{
    Error("Please select exactly one measure.");
    return;
}

var measure = SelectMeasure();
var tableName = measure.Table.Name;
var measureName = measure.Name;

string dateTable = "'Calendar'[Date]"; // **Important:** Replace with your date table and column

// Store the newly created measures in a list for batch formatting
List<Measure> newMeasures = new List<Measure>();


// --- YTD ---
string ytdMeasureName = $"{measureName} YTD";
string ytdDax = $@"CALCULATE({measure.DaxObjectFullName}, DATESYTD({dateTable}))";
newMeasures.Add(measure.Table.AddMeasure(ytdMeasureName, ytdDax, measure.DisplayFolder + "\\Time Comparisons")); // Add to list


// --- QTD ---
string qtdMeasureName = $"{measureName} QTD";
string qtdDax = $@"CALCULATE({measure.DaxObjectFullName}, DATESQTD({dateTable}))";
newMeasures.Add(measure.Table.AddMeasure(qtdMeasureName, qtdDax, measure.DisplayFolder + "\\Time Comparisons")); // Add to list


// --- MTD ---
string mtdMeasureName = $"{measureName} MTD";
string mtdDax = $@"CALCULATE({measure.DaxObjectFullName}, DATESMTD({dateTable}))";
newMeasures.Add(measure.Table.AddMeasure(mtdMeasureName, mtdDax, measure.DisplayFolder + "\\Time Comparisons")); // Add to list


// --- Previous Period ---
string prevPeriodMeasureName = $"{measureName} Previous Period";
string prevPeriodDax = $@"CALCULATE({measure.DaxObjectFullName}, PREVIOUSMONTH({dateTable}))"; // Or PREVIOUSDAY, etc.
newMeasures.Add(measure.Table.AddMeasure(prevPeriodMeasureName, prevPeriodDax, measure.DisplayFolder + "\\Time Comparisons")); // Add to list


// --- Same Period Last Year (SPLY) ---
string splyMeasureName = $"{measureName} SPLY";
string splyDax = $@"CALCULATE({measure.DaxObjectFullName}, SAMEPERIODLASTYEAR({dateTable}))";
newMeasures.Add(measure.Table.AddMeasure(splyMeasureName, splyDax, measure.DisplayFolder + "\\Time Comparisons")); // Add to list



// Format all new measures at once efficiently
FormatDax(newMeasures, shortFormat: true);

Info($"Created time comparison measures in table '{tableName}'.");