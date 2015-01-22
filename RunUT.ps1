Param([string]$filters)

$configName = "DEV"
$vsConfigName = ""

$vstestconsolepath = "C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe"
$dotcoverpath = "C:\BuildAgent\tools\dotCover\dotCover.exe"
$dotcovertargetexecutable = "/TargetExecutable={0}" -f $vstestconsolepath
$dotcoveroutput = "/Output=C:\BuildAgent\work\7d4bf7a03cac5864\{0}\coverage.dcvr" -f $configName
$dotcoverfilters = "/Filters={0}" -f $filters

$folder = get-location
#look for win store DLLs. If we find one we can't do code coverage on this test assembly
$winStoreDLLs = Get-ChildItem -Recurse -Force $folder.FullName -File | Where-Object { $_.Directory -match "\\bin\\" -and $_.Name -like "*UT.dll" } | Select-Object
$win8Arr = @()
foreach ($dll in $winStoreDLLs)
{
	$win8Arr += $dll.FullName
}

if ($win8Arr.length -gt 0)
{
    & $vstestconsolepath $win8Arr
    
    #build up command for vstest console to pass inside of dotCover command
    $targetarguments = "/TargetArguments=\`"{0}\`" /inIsolation /logger:TeamCityLogger" -f $win8Arr

    #execute dotCover command with arguments
    $finalcommand = "c {0} {1} {2} {3}" -f @($dotcovertargetexecutable, $targetarguments, $dotcoveroutput, $dotcoverfilters)

    & $dotcoverpath 'c' $dotcovertargetexecutable $targetarguments $dotcoveroutput $dotcoverfilters

    #pass message to teamcity to process code coverage
    "##teamcity[importData type='dotNetCoverage' tool='dotcover' path='" + $folder + "\" + $configName + "\coverage.dcvr']"
}