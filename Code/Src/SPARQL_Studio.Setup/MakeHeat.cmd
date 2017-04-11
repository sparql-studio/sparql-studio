
SET HarvestFileDir=%1
SET OutputPath=%2
SET ComponentGroup=%3
SET SourceVar=%4

echo ComponentGroup = %3
echo ComponentGroup = %ComponentGroup%
echo "%WIX%\bin\heat.exe" dir "%HarvestFileDir%" -cg %ComponentGroup% -gg -scom -sreg -sfrag -srd -dr INSTALLDIR -var var.%ComponentGroup%.SourceDir -out "%OutputPath%"
"%WIX%\bin\heat.exe" dir %HarvestFileDir% -cg %ComponentGroup% -gg -scom -sreg -sfrag -srd -dr INSTALLDIR -var var.%SourceVar% -out %OutputPath%