using Google.Apis.Sheets.v4.Data;
using JiaoMaCupScoreRecorder.Client.Models;

namespace JiaoMaCupScoreRecorder.Client.Services.Interfaces;

public interface IGoogleSheetService
{
    Task<IList<IList<string>>> GetSheet(GetSheetValuesRequestBodyModel requestBody);

    Task InitializeSheet(IDictionary<string, IList<IList<string>>> totalSheetDictionary,
                         IDictionary<string, IList<IList<string>>> weekSheetDictionary);

    Task BatchUpdateValues(string spreadsheetId, BatchUpdateValuesRequest requestBody);
}