using System.Windows.Forms;  
using System.IO;          

// This script exports all measures and their properties to a CSV file chosen by the user.

// Use a SaveFileDialog to let the user choose the file path
SaveFileDialog saveFileDialog = new SaveFileDialog();
saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
saveFileDialog.Title = "Export Measures";
saveFileDialog.ShowDialog();

// Check if the user selected a file
if (saveFileDialog.FileName != "")
{
    // Get the chosen file path
    string filePath = saveFileDialog.FileName;

    try
    {
        // Create a StreamWriter to write to the file
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            // Write the header row
            writer.WriteLine("Table,Measure Name,DAX Expression,Display Folder,Format String,Description,Is Hidden");

            // Iterate through all tables in the model
            foreach (var table in Model.Tables)
            {
                // Iterate through all measures in the current table
                foreach (var measure in table.Measures)
                {
                    // ... (Rest of the code to write measure properties is the same)
                    writer.WriteLine(
                        $"{table.Name}," +
                        $"\"{measure.Name}\"," + 
                        $"\"{measure.Expression}\"," + 
                        $"\"{measure.DisplayFolder}\"," + 
                        $"\"{measure.FormatString}\"," + 
                        $"\"{measure.Description}\"," +
                        $"{measure.IsHidden}" 
                    );
                }
            }
        }

        // Inform the user that the export is complete
        Info($"Measures exported to: {filePath}");
    }
    catch (Exception ex)
    {
        // Handle potential file system errors
        Error($"Error exporting measures: {ex.Message}"); 
    }
}
else
{
    // Inform the user if no file was selected
    Info("Export cancelled.");
}
