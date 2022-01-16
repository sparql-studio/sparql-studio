
SET HarvestFileDir=%1\HarvestDir
SET HarvestSourceDir=%1\..\SPARQL_Studio\bin\%5
SET OutputPath=%2
SET ComponentGroup=%3
SET SourceVar=%4
SET Configuration=%5

md "%HarvestFileDir%"
xcopy  /E /Y "%HarvestSourceDir%\*.*" "%HarvestFIleDir%\*.*"
del "%HarvestFIleDir%\SPARQL_Studio.exe"
del "%HarvestFIleDir%\Sparql_document.ico"

echo ComponentGroup = %3
echo ComponentGroup = %ComponentGroup%

echo "%WIX%\bin\heat.exe" dir "%HarvestFileDir%" -cg %ComponentGroup% -gg -scom -sreg -sfrag -srd -dr INSTALLDIR -var var.%ComponentGroup%.SourceDir -out "%OutputPath%"
"%WIX%\bin\heat.exe" dir %HarvestFileDir% -cg %ComponentGroup% -gg -scom -sreg -sfrag -srd -dr INSTALLDIR -var var.%SourceVar% -out %OutputPath%