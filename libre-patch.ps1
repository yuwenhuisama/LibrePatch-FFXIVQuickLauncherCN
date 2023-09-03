Write-Output "Please run this script with administrator privileges."
Write-Output "Do right click - run as administrator."
Write-Output "Press any key if you're running it as administrator."
Write-Output "Also please ensure the Config.json has been configured correctly."
pause

Start-Process -FilePath "./bin/LibrePatch.exe" -Verb RunAs