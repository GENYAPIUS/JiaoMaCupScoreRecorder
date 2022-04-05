using System.Net;
using Google.Apis.Sheets.v4.Data;
using JiaoMaCupScoreRecorder.Client.Const;
using JiaoMaCupScoreRecorder.Client.Extensions;
using JiaoMaCupScoreRecorder.Client.Models;
using JiaoMaCupScoreRecorder.Client.Services.Interfaces;
using JiaoMaCupScoreRecorder.Client.Utils;
using Microsoft.JSInterop;

namespace JiaoMaCupScoreRecorder.Client.Services;

public class GoogleSheetService : IGoogleSheetService
{
    private readonly IJSRuntime _js;
    private readonly Dictionary<string, SheetInfoModel> _spreadsheet;

    public GoogleSheetService(IJSRuntime js, Dictionary<string, SheetInfoModel> spreadsheet)
    {
        _js = js;
        _spreadsheet = spreadsheet;
    }

    public async Task<IList<IList<string>>> GetSheet(GetSheetValuesRequestBodyModel requestBody)
    {
        var requestBodyJson = JsonSerializerUtils.Serialize(requestBody);
        var result = await _js.InvokeAsync<IList<IList<string>>>("getSheetData", requestBodyJson);

        return result;
    }

    public async Task InitializeSheet(IDictionary<string, IList<IList<string>>> totalSheetDictionary,
                                      IDictionary<string, IList<IList<string>>> weekSheetDictionary)
    {
        foreach (var (gameName, sheetInfo) in _spreadsheet)
        {
            if (gameName == GamesConst.Players) continue;
            await AddOrUpdateWeekSheet(gameName, weekSheetDictionary);
            await AddOrUpdateTotalSheet(gameName, totalSheetDictionary);
        }
    }

    public async Task BatchUpdateValues(string spreadsheetId, BatchUpdateValuesRequest requestBody)
    {
        var requestBodyJson = JsonSerializerUtils.Serialize(requestBody);

        var statusCode =
            await _js.InvokeAsync<HttpStatusCode>("batchUpdateValues", spreadsheetId, requestBodyJson);

        if (statusCode != HttpStatusCode.TooManyRequests) return;
        await Task.Delay(60000);
        _ = await _js.InvokeAsync<HttpStatusCode>("batchUpdateValues", spreadsheetId, requestBodyJson);
    }

    private async Task BatchUpdateSheet(string gameName, BatchUpdateSpreadsheetRequest requestBody)
    {
        var requestBodyJson = JsonSerializerUtils.Serialize(requestBody);

        var statusCode =
            await _js.InvokeAsync<HttpStatusCode>("batchUpdateSheetData", _spreadsheet[gameName].SpreadsheetId,
                requestBodyJson);

        if (statusCode != HttpStatusCode.TooManyRequests) return;
        await Task.Delay(60000);

        _ = await _js.InvokeAsync<HttpStatusCode>("batchUpdateSheetData", _spreadsheet[gameName].SpreadsheetId,
            requestBodyJson);
    }

    private async Task AddOrUpdateTotalSheet(string gameName,
                                             IDictionary<string, IList<IList<string>>> totalSheetDictionary)
    {
        var isTotalSheetExist = await IsSheetExist(gameName, 0);

        var requestData = new SheetRequestDataModel
        {
            SheetId = 0,
            Title = "總積分",
            Index = 0,
            Rows = totalSheetDictionary[gameName].ToRows(),
            FrozenRowCount = 1,
            StartRowIndex = 0,
            EndRowIndex = totalSheetDictionary[gameName].Count,
            StartColumnIndex = 0,
            EndColumnIndex = 4
        };

        if (isTotalSheetExist)
        {
            var requestBody = requestData.ToUpdateSheetsRequestBodyModel();
            await BatchUpdateSheet(gameName, requestBody);
        }
        else
        {
            var requestBody = requestData.ToNewSheetsRequestBodyModel();
            await BatchUpdateSheet(gameName, requestBody);
        }
    }

    private async Task AddOrUpdateWeekSheet(string gameName,
                                            IDictionary<string, IList<IList<string>>> weekSheetDictionary)
    {
        var weekSheetExistList = await IsWeekSheetExist(gameName);
        var columnNumber = weekSheetDictionary[gameName][0].Count;

        for (var i = 0; i < weekSheetExistList.Count; i++)
        {
            var index = i + 1;

            var requestData = new SheetRequestDataModel
            {
                SheetId = index,
                Title = StringConst.Sheets[index],
                Index = index,
                Rows = weekSheetDictionary[gameName].ToRows(),
                FrozenRowCount = 1,
                StartRowIndex = 0,
                EndRowIndex = weekSheetDictionary[gameName].Count,
                StartColumnIndex = 0,
                EndColumnIndex = columnNumber
            };

            if (weekSheetExistList[i])
            {
                var requestBody = requestData.ToUpdateSheetsRequestBodyModel();
                await BatchUpdateSheet(gameName, requestBody);
            }
            else
            {
                var requestBody = requestData.ToNewSheetsRequestBodyModel();
                await BatchUpdateSheet(gameName, requestBody);
            }
        }
    }

    private async Task<IList<bool>> IsWeekSheetExist(string gameName)
    {
        IList<bool> result = new List<bool>();

        for (var i = 1; i < StringConst.Sheets.Length; i++)
        {
            var isExist = await IsSheetExist(gameName, i);
            result.Add(isExist);
        }

        return result;
    }

    private async Task<bool> IsSheetExist(string gameName, int sheetId)
    {
        var requestBody = GetSpreadsheetByDataFilterRequestBodyModel(sheetId);
        var requestBodyJson = JsonSerializerUtils.Serialize(requestBody);

        var statusCode =
            await _js.InvokeAsync<HttpStatusCode>("checkSheetExist", _spreadsheet[gameName].SpreadsheetId,
                requestBodyJson);

        if (statusCode == HttpStatusCode.TooManyRequests)
        {
            await Task.Delay(60000);

            statusCode =
                await _js.InvokeAsync<HttpStatusCode>("checkSheetExist", _spreadsheet[gameName].SpreadsheetId,
                    requestBodyJson);
            var result = statusCode == HttpStatusCode.OK;

            return result;
        }
        else
        {
            var result = statusCode == HttpStatusCode.OK;

            return result;
        }
    }

    private GetSpreadsheetByDataFilterRequest GetSpreadsheetByDataFilterRequestBodyModel(int sheetId) =>
        new GetSpreadsheetByDataFilterRequest
        {
            DataFilters = new List<DataFilter>
            {
                new()
                {
                    GridRange = new GridRange
                    {
                        SheetId = sheetId
                    }
                }
            }
        };
}