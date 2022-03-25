using Google.Apis.Sheets.v4;

namespace JiaoMaCupScoreRecorder.Client.Models;

public class GetSheetValuesRequestBodyModel
{
    public string SpreadsheetId { get; set; } = string.Empty;

    public SpreadsheetsResource.ValuesResource.BatchGetRequest.MajorDimensionEnum MajorDimensions { get; set; }

    public string Range { get; set; } = string.Empty;
}