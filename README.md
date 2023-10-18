## NO LONGER MANTAINED
THIS PROJECT IS NO LONGER MANTAINED.

> At this moment, I no longer have access to Samsung TVs, and I cannot test their latest models. For this reason, this project cannot provide further updates.
> Fortunately, there are currently better options, such as [ChanSort](https://github.com/PredatH0r/ChanSort).

# SamsChannelEditor


SamsChannelEditor is a desktop application for editing a channel list from your Samsung TV. Now sorting channels should be much easier.


* Support Samsung C and D Series.
* Works with scm files directly.
* Support for multiple channels configurations:
  * map-AirD
  * map-AirA
  * map-CableD
  * map-CableA
  * map-SateD
  * map-CDTVVD
  * AstraHDPlusD
* Support for configuration files (read-only)
  * CloneInfo
  * SatDataBase
* Easy to use: Just drag & drop channels.

! USE AT YOUR OWN RISK !
--------------------------
This software was written without support from TV manufacturers or access to any official documentation about the file formats
and is suppliedded "as is" without warranty of any kind, either express or implied. Use at your own risk.

Installation
------------
There's no setup package, just download the zip file from [SamsChannelEditor](http://sourceforge.net/projects/samschanneledit/files),
extract all in a folder and execute ''samschanneleditor.exe''. 

This application is written in C# so you need the .Net framework 4.0 (or newer) to be installed on your computer.

Source Code
------------
Source code is aviable from [GitHub](https://github.com/imasm/samschanneledit) 

__Build instructions:__
*Requieres .NET Framework 4.8 Sdk*
```bash
projectPath\source> nuget.exe restore
projectPath\source> msbuild SamsChannelEditor.sln /property:Configuration=Release
```

Bad file size error
-------------------
Since version 0.9 you can define new sizes in SamsChannelEditor.exe.config. Just open with a text editor like notepad and add a new size for your file in section.

key: starts with "fs_" and after comes the channel file name (without "-" after map) value: is a number list separated by commas. Each number specifies a possible record size.

To calculate your record size:

* change extension .scm to .zip
* Extract all files.
* Select file you are opening and look the size in file properties.
* record size is a 1000 multiple (divide your size by 1000 or 2000)

### Example: ###

```xml
<appSettings> 
   <add key="fs_default" value="292,320" /> 
   <add key="fs_mapAirA" value="40,64" /> 
   <add key="fs_mapCableA" value="40,64" /> 
   <add key="fs_mapSateD" value="168,172,194" /> 
   <add key="fs_mapAstraHDPlusD" value="212" /> 
</appSettings>
```

Libraries & Resources used
--------------------------
* [\#ziplib](http://www.icsharpcode.net/opensource/sharpziplib)  
* [log4net](http://logging.apache.org/log4net)  
* [Free Icons by Axialis Software](http://www.axialis.com/free/icons)  

Developers and Collaborators
-----------------------------
* Ivan Masmitja(https://github.com/imasm)
* Andrey(https://github.com/andrew097) 
* Hinni(https://github.com/Hinni)

License
-------
    Copyright (C) 2011  Ivan Masmitjà Dagas

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.

Last changelog
--------------
### Version 0.13 ###

* Fix removing encrypted channels.

### Version 0.12 ###

* Fix Drag&Drop. Channel still enabled.
* Fix rename. Short names will appear correctly.

### Version 0.11 ###

* Fixed channel names output. Names in Russian output correctly. Other languages must do so
* Channel data edit feature realized. You can edit channel name and favourites.

### Version 0.10 ###

* Applied a patch created by Andreas Winter to save sort order to file and restore after.
* Applied a patch created by Andreas Mayer to remove encrypted channels.

### Version 0.9 ###

* Moved record sizes to SamsChannelEditor.exe.config. Now are easy to add new record sizes for new models.

### Version 0.8 ###

* BUG [3468470] : Open map-SateD 144bytes length (LE37C670)
* MoveTo problems detected when out of range number is introduced.

### Version 0.7 ###

* Works on D Series
* Drag and Drop with auto scroll
* Added support for analog channels map-AirA and map-CableA
* Context menu: MoveTo
* Context menu: Renumber All
