# LibrePatch-FFXIVQuickLauncherCN
## 中文说明
由于某些未知原因，[FFXIVQuickLauncher](https://github.com/ottercorp/FFXIVQuickLauncher)禁止了一部分`Dalamud插件的加载`，本项目旨在解除这一限制。

### 使用方法
> 注意事项：本项目目前需要配合 [UnbanPluginsCN](https://github.com/NightmareXIV/UnbanPluginsCN)这个项目一起使用，请保证在启动**FFXIVQuickLauncher**之前已经启用了`UnbanPluginsCN 2.0.0.0`
> 您在本项目release页面中下载的安装包不包含`UnbanPluginsCN`，需要您自行下载。

1. 保证`UnbanPluginsCN`已经启动。
2. 在本项目[release](https://github.com/yuwenhuisama/LibrePatch-FFXIVQuickLauncherCN/releases)页面下载本项目编译好的压缩包，解压到任意目录，例如`LibrePatch`。
3. 打开`LibrePatch/Config.json`，编辑`XIVLauncherBasePath`这个字段，将其修改为您`FFXIVQuickLauncherCN`的安装目录，例如`D:/FFXIVLuncher/XIVLauncherCN`，修改完后保存。
4. 右键以管理员权限运行`libre-patch.bat`，确认后回车，等待其运行。
5. 当在命令行窗口中看到`Start mock http server, you can safely close this window after injection completed.`字样时，此时您可以正常启动`FFXIVQuickLauncherCN`进入游戏。
6. 当确认`Dalamud`插件已经正常加载后，可以手动关闭`libre-patch.bat`的命令行窗口。

## English Description
For some unknown reason, [FFXIVQuickLauncher](https://github.com/ottercorp/FFXIVQuickLauncher) has banned some `Dalamud` plugins, this project aims to remove this restriction.

### Usage
> Note: This project currently needs to be used with [UnbanPluginsCN](https://github.com/NightmareXIV/UnbanPluginsCN), Please make sure that `UnbanPluginsCN` has been enabled before starting **FFXIVQuickLauncher**.
> The installation package you downloaded in the release page of this project does not include `UnbanPluginsCN`, you need to download it yourself.

1. Make sure that `UnbanPluginsCN` has been started.
2. Download the compiled zip package of this project from the [release](https://github.com/yuwenhuisama/LibrePatch-FFXIVQuickLauncherCN/releases) page, and unzip it to any directory, such as `LibrePatch`.
3. Open `LibrePatch/Config.json`, edit the `XIVLauncherBasePath` field by modifying it to the installation directory of your `FFXIVQuickLauncherCN` such as `D:/FFXIVLuncher/XIVLauncherCN`, and save after modification.\
4. Right-click to run `libre-patch.bat` with administrator privileges, press enter after confirmation, and wait for it to run.
5. When you see the words `Start mock http server, you can safely close this window after injection completed.` in the command line window, you can start `FFXIVQuickLauncherCN` normally to start the game.
6. When you confirm that the `Dalamud` plugin has been loaded normally, you can manually close the command line window of `libre-patch.bat`.