$Path = Get-Location
$To = "${Path}\..\temp"
$Extension = "*.pot"

ForEach ($file in Get-ChildItem -Filter $Extension) {
    $baseName = $file.BaseName
    & msginit --input="${Path}\${baseName}.pot" --output="${To}\${baseName}-zh_CN.po" --locale=zh_CN.UTF-8
}
Read-Host "Press ENTER to continue"
