# JiaoMaCupScoreRecorder
A score recorder for JiaoMa Cup

# Requirement
- .Net 6.0.201 SDK
  - [Windows x64](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-6.0.201-windows-x64-installer)
  - [Linux](https://docs.microsoft.com/zh-tw/dotnet/core/install/linux)
- .Net Worktool (if use AOT Compilation)
  - `dotnet workload install wasm-tools`
- appsettings.json

# Release Build

1. Check `AOT Compilation setting` if the following block is included in JiaoMaCupScoreRecorder.csproj.  
If not need AOT Compilation, set it `false`
```xml
<PropertyGroup>
	<RunAOTCompilation>true</RunAOTCompilation>
</PropertyGroup>
```
2. Check `.Net 6.0.201 SDK` and `.Net Worktool` installed. see [Requirement](#requirement)
3. Copy appsettings.json file to JiaoMaCupScoreRecorder/wwwroot
4. Set Server environment variable
   - `ASPNETCORE_ENVIRONMENT`
     - Development
     - PreProduction
     - Production
5. Open terminal and change directory to JiaoMaCupScoreRecorder.Server.csproj location
6. Enter `dotnet build` to try build
   - Success Message:
    ```
    ....
    建置成功。
    ....
    ```
7. Enter `dotnet publish -c Release -o <output_path>` to build production files
8. If it succeeds, the <output_path> should have files.
