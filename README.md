# MusicPickerDevice
MusicPicker device client allows remote music playback and database collection on a computer or smart objects.
Client scans its local music library, establishes connection with 
[Musicpicker](https://github.com/musicpicker/musicpicker) and submit its metadata database.

MusicPickerDevice acts as a tray icon and provides user controls for service login, signup and music path's selection.
Music is played in background, playback can be managed on [musicpicker.net](http://musicpicker.net).

Downloads
---------
Precompiled binaries configured to access [musicpicker.net](http://musicpicker.net)
are available on [Github Releases](https://github.com/musicpicker/MusicPickerDevice/releases).

Features
==========
MusicPickerDevice implements features essential to music playback and remote control.

- User login and signup
- User account and device name switching
- Multiple music library paths selection
- Local database metadata caching
- Support for MP3, WAV and FLAC files
- Database submission to webservice
- Webservice remote control via SignalR

Dependencies
============
MusicPickerDevice is a Windows Forms project built in C# on .NET Framework 4.5.

It relies on the following dependencies to properly work:

- [NAudio](https://github.com/naudio/NAudio) for music playback.
- [LiteDB](https://github.com/mbdavid/LiteDB) for local metadata database.
- [Taglib#](https://github.com/mono/taglib-sharp) for metadata reading.
- [SocketIoClientDotNet](https://github.com/Quobject/SocketIoClientDotNet) for WebSocket communication.
- [Json.NET](https://github.com/JamesNK/Newtonsoft.Json) for JSON (de)serialization.

Dependencies should be retrieved by calling Nuget's restore command.

    nuget restore

Change service URL
------------------
Whereas [precompiled binaries](https://github.com/musicpicker/MusicPickerDevice/releases) are built to
reach [musicpicker.net](http://musicpicker.net), source controlled app is configured to
access service at [http://localhost:3000](http://localhost:3000), which is the default bind for Musicpicker
in source control.

Service URL is bundled in MusicPickerDevice's source code, in *MusicPickerDevice.cs*. 

You can change it to another URL as you need.

    player = new Player(library);
    client = new ApiClient(new Uri("http://localhost:3000"));
    hubConnection = new HubConnection("http://localhost:3000");

License
===========
© 2015 Hugo Caille, Pierre Defache & Thomas Fossati. 

Musicpicker is released upon the terms of the Apache 2.0 License.
