# JiaoMaCupScoreRecorder.Server

## Step

1. Open terminal
2. Copy appsettings.json file to JiaoMaCupScoreRecorder/wwwroot
3. Set Server environment variable
   - `ASPNETCORE_ENVIRONMENT`
     - Development
     - PreProduction
     - Production
4. Change directory to JiaoMaCupScoreRecorder.Server.csproj location
5. Enter `dotnet add reference <JiaoMaCupScoreRecorder.csproj path>`
   - Success Message: ``參考 `../../../../JiaoMaCupScoreRecorder.csproj` 已新增至專案。``
5. Enter `dotnet build` to try build
   - Success Message:
    ```
    ....
    建置成功。
    ....
    ```

6. Enter `dotnet publish -c Release -o <output_path>` to build production files
7. If it succeeds, the <output_path> should have files.
