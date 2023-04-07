$Path = Get-Location
$To = "${Path}\..\temp"
$SourceExtension = "*.pot"

ForEach ($file in Get-ChildItem -Filter $SourceExtension) {
    $baseName = $file.BaseName
    & msginit --input="${Path}\${baseName}.pot" --output="${To}\${baseName}.po" --locale=zh_CN.UTF-8
}
Read-Host "Press ENTER to continue"
