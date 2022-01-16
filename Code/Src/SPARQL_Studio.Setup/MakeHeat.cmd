SET HarvestFileDir=%1HarvestDir
SET HarvestSourceDir=%1\..\SPARQL_Studio\bin\%5
SET OutputPath=%2
SET ComponentGroup=%3
SET SourceVar=%4
SET Configuration=%5

echo creating folder "%HarvestFileDir%"...
md "%HarvestFileDir%"

echo xcopy  /E /Y "%HarvestSourceDir%" "%HarvestFileDir%"
xcopy  /E /Y "%HarvestSourceDir%" "%HarvestFileDir%"

del "%HarvestFileDir%\SPARQL_Studio.exe"
del "%HarvestFileDir%\Sparql_document.ico"

echo ComponentGroup = %3
echo ComponentGroup = %ComponentGroup%

echo "%WIX%\bin\heat.exe" dir %HarvestFileDir% -cg %ComponentGroup% -gg -scom -sreg -sfrag -srd -dr INSTALLDIR -var var.%ComponentGroup%.SourceDir -out %2
"%WIX%\bin\heat.exe" dir %HarvestFileDir% -cg %ComponentGroup% -gg -scom -sreg -sfrag -srd -dr INSTALLDIR -var var.%SourceVar% -out "%2"

REM echo "%WIX%\bin\heat.exe" project "%1\..\SPARQL_Studio\SPARQL_Studio.csproj" -cg %ComponentGroup% -gg -scom -sreg -sfrag -srd -dr INSTALLDIR -var var.%SourceVar% -out %OutputPath%
REM "%WIX%\bin\heat.exe" project "%1\..\SPARQL_Studio\SPARQL_Studio.csproj" -cg %ComponentGroup% -gg -scom -sreg -sfrag -srd -dr INSTALLDIR -var var.%SourceVar% -out %OutputPath%
