# LibrePatch-FFXIVQuickLauncherCN
## 中文说明
由于某些未知原因，[FFXIVQuickLauncher](https://github.com/ottercorp/FFXIVQuickLauncher)禁止了一部分`Dalamud`插件的加载，本项目旨在解除这一限制。

### 使用方法
首先请按照以下步骤下载`LibrePatch`并进行配置。
1. 在本项目[release](https://github.com/yuwenhuisama/LibrePatch-FFXIVQuickLauncherCN/releases)页面下载本项目编译好的压缩包，解压到任意目录，例如`LibrePatch`。
2. 打开`LibrePatch/Config.json`，编辑`XIVLauncherBasePath`这个字段，将其修改为您`FFXIVQuickLauncherCN`的安装目录，例如`D:/FFXIVLuncher/XIVLauncherCN`，修改完后保存。

需要注意的是，目前`LibrePatch`提供两种策略来解除`FFXIVQuickLauncher`对`Dalamud`插件的限制，分为`v1`版本和`v2`版本，您可以在`Config.json`中的`HackStrategyVersion`字段中修改，如果设置为`1`则启用`v1`版本，如果设置为`2`则启用`v2`版本，两种版本各有优劣，您可以根据自己的需求来选择。
具体区别如下：
- `v1`版本通过拦截`FFXIVQuickLauncher`拉取远端插件列表的请求，将相关请求的URL替换为本地URL，通过启动本地服务器过滤掉请求中的相关字段，从而解除插件被禁止的限制。这种做法的优点在于比较稳定，但是需要配合`UnbanPlugingsCN`使用，且需要在启动`FFXIVQuickLauncher`之前先启动`LibrePatch`以保证本地服务器已经被拉起。
- `v2`版本通过直接修改`Dalamud`的Assembly，直接让`Dalamud`忽略检测被禁止掉的插件，从而解除插件被禁止的限制。这种做法的优点在于不需要配合`UnbanPlugingsCN`使用，且只要`Dlamud`不更新，就不需要再次启动`LibrePatch`，但是由于直接根据特征IL去修改`Dalamud`的Assembly，如果`Dalamud`有较大的改动的话，可能会导致`LibrePatch`失效，需要等待`LibrePatch`重新适配。

#### V1版本使用方法
> 注意事项：本项目目前需要配合 [UnbanPluginsCN](https://github.com/NightmareXIV/UnbanPluginsCN)这个项目一起使用，请保证在启动`FFXIVQuickLauncher`之前已经启用了`UnbanPluginsCN 2.0.0.0`
> 您在本项目release页面中下载的安装包不包含`UnbanPluginsCN`，需要您自行下载。

1. 保证`UnbanPluginsCN`已经启动。
2. 右键以管理员权限运行`libre-patch.bat`，确认后回车，等待其运行。
3. 当在命令行窗口中看到`Start mock http server, you can safely close this window after injection completed.`字样时，此时您可以正常启动`FFXIVQuickLauncherCN`进入游戏。
4. 当确认`Dalamud`插件已经正常加载后，可以手动关闭`libre-patch.bat`的命令行窗口。

#### V2版本使用方法
1. 右键以管理员权限运行`libre-patch.bat`，确认后回车，等待其运行。
2. 当在命令行窗口中看到`Assembly rewrite completed, you can safely close this window.`字样时，此时您可以正常启动`FFXIVQuickLauncherCN`进入游戏。
3. 如果发现`Dalamud`在更新后注入失效，可以重新运行`libre-patch.bat`来重新对Assembly打Patch。

> 如果您想要紧急还原`LibrePatch`对`Dalamud`的修改，可以在`FFXIVQuickLauncher根目录/Roaming/addon/Hooks`目录下找到一系列文件夹，将每个文件夹里面的`Dalamud.dll`删除，再将`Dalamud.dll.bak`，将其重命名为`Dalamud.dll`即可。

## English Description
For some unknown reason, [FFXIVQuickLauncher](https://github.com/ottercorp/FFXIVQuickLauncher) has banned some `Dalamud` plugins, this project aims to remove this restriction.

### Usage
> Note: This project currently needs to be used with [UnbanPluginsCN](https://github.com/NightmareXIV/UnbanPluginsCN), Please make sure that `UnbanPluginsCN` has been enabled before starting **FFXIVQuickLauncher**.
> The installation package you downloaded in the release page of this project does not include `UnbanPluginsCN`, you need to download it yourself.

Please note that currently `LibrePatch` supports two strategies to remove the restriction of `FFXIVQuickLauncher` on `Dalamud` plugins, which are `v1` version and `v2` version. You can modify it in the `HackStrategyVersion` field in `Config.json`. If it is set to `1`, the `v1` version will be enabled, and if it is set to `2`, the `v2` version will be enabled. Each version has its own advantages and disadvantages, you can choose according to your needs. The differences are as follows:
- The `v1` version intercepts the request of `FFXIVQuickLauncher` to pull the remote plugin list, replaces the URL of the relevant request with the local URL, and filters the relevant fields in the request through the local server, thereby removing the restriction of the plugin being banned. The advantage of this approach is that it is relatively stable, but it needs to be used with `UnbanPlugingsCN`, and `LibrePatch` needs to be started before starting `FFXIVQuickLauncher` to ensure that the local server has been pulled up.
- The `v2` version directly modifies the Assembly of `Dalamud`, directly allowing `Dalamud` to ignore the detection of banned plugins, thereby removing the restriction of the plugin being banned. The advantage of this approach is that it does not need to be used with `UnbanPlugingsCN`, and as long as `Dlamud` is not updated, `LibrePatch` does not need to be started again, but because it directly modifies the Assembly of `Dalamud` based on the feature IL, if `Dalamud` has a major change, it may cause `LibrePatch` to fail to hack and need to wait for `LibrePatch` to be re-adapted.

#### V1 Version Usage
1. Make sure that `UnbanPluginsCN` has been started.
2. Right-click to run `libre-patch.bat` with administrator privileges, press enter after confirmation, and wait for it to run.
3. When you see the words `Start mock http server, you can safely close this window after injection completed.` in the command line window, you can start `FFXIVQuickLauncherCN` normally to start the game.
4. When you confirm that the `Dalamud` plugin has been loaded normally, you can manually close the command line window of `libre-patch.bat`.

#### V2 Version Usage
1. Right-click to run `libre-patch.bat` with administrator privileges, press enter after confirmation, and wait for it to run.
2. When you see the words `Assembly rewrite completed, you can safely close this window.` in the command line window, you can start `FFXIVQuickLauncherCN` normally to start the game.
3. If you find that injection is invalid after `Dalamud` updated, you can rerun `libre-patch.bat` to re-patch the Assembly.

> If you want to urgently restore the modification from `LibrePatch` on `Dalamud`, you can find a series of folders in the `FFXIVQuickLauncher root directory/Roaming/addon/Hooks` directory, delete the `Dalamud.dll` in each folder, and then rename `Dalamud.dll.bak` to `Dalamud.dll`.