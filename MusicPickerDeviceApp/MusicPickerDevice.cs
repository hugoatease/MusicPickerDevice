﻿// ***********************************************************************
// Assembly         : MusicPickerDeviceApp
// Author           : Pierre
// Created          : 06-17-2015
//
// Last Modified By : Pierre
// Last Modified On : 06-21-2015
// ***********************************************************************
// <copyright file="MusicPickerDevice.cs" company="Hutopi">
//     Copyright ©  2015 Hugo Caille, Pierre Defache & Thomas Fossati.
//     Music Picker is released upon the terms of the Apache 2.0 License.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using MusicPickerDeviceApp.App;
using MusicPickerDeviceApp.Properties;
using LiteDB;
using Quobject.SocketIoClientDotNet.Client;

/// <summary>
/// The MusicPickerDeviceApp namespace.
/// </summary>
namespace MusicPickerDeviceApp
{
    /// <summary>
    /// Class MusicPickerDevice.
    /// Represents the device application.
    /// </summary>
    public class MusicPickerDevice : IDisposable
    {
        /// <summary>
        /// The notify icon of the application
        /// </summary>
        private NotifyIcon notifyIcon;
        /// <summary>
        /// The menu of the application
        /// </summary>
        private ContextMenus menu;
        /// <summary>
        /// The database
        /// </summary>
        private LiteDatabase database;
        /// <summary>
        /// The configuration of the device
        /// </summary>
        private Configuration configuration;
        /// <summary>
        /// The API client in order to interact with the Webservice
        /// </summary>
        private ApiClient client;
        /// <summary>
        /// The seeker to find the music files in the selected folders
        /// </summary>
        private Seeker seeker;
        /// <summary>
        /// The library who represents the music collection
        /// </summary>
        private Library library;
        /// <summary>
        /// The music player
        /// </summary>
        private Player player;

        private Socket socket;
        private HubClient hubClient;
        /// <summary>
        /// The file watchers in order to be aware if a file is suppressed or added in the selected folders
        /// </summary>
        private List<FileWatcher> fileWatchers = new List<FileWatcher>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicPickerDevice"/> class.
        /// </summary>
        public MusicPickerDevice()
        {
            database = new LiteDatabase("musicpicker.db");
            configuration = new Configuration();
            library = new Library(database);
            seeker = new Seeker(library, new[] { "mp3", "wav", "m4a", "flac" });
            notifyIcon = new NotifyIcon();
            menu = new ContextMenus()
            {
                SignUpForm = new SignUpForm(SignUp),
                ConnectionForm = new ConnectionForm(Connect),
                LoadForm = new LibraryPathsForm(configuration.Model, UpdateLibraryPaths)
            };

            player = new Player(library);

            client = new ApiClient(new Uri("http://localhost:3000"));
            hubClient = new HubClient(player);
        }

        private void socketInitialize()
        {
            if (configuration.Model.Registered)
            {
                socket.Emit("authentication", configuration.Model.Bearer);
                socket.On("authenticated", () =>
                {
                    socket.Emit("RegisterDevice", configuration.Model.DeviceId);
                });
                hubClient.AttachToSocket(socket);
            }
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public async void Initialize()
        {
            Display();

            if (configuration.Model.Registered)
            {
                menu.ShowAuthenticatedMenu(configuration.Model.DeviceName, false, Disconnect);
                client.ProvideBearer(configuration.Model.Bearer);
                socket = IO.Socket("http://localhost:3000");
                socketInitialize();
                socket.On("reconnect", socketInitialize);
            }
            else
            {
                menu.ShowUnauthenticatedMenu();
            }

            if (configuration.Model.Registered)
            {
                player.AttachNextCallback(() => socket.Emit("Next", configuration.Model.DeviceId));
                await UpdateLibrary();

                foreach (var path in configuration.Model.Paths)
                {
                    fileWatchers.Add(new FileWatcher(path, AddTrack, DeleteTrack));
                }
            }
        }

        /// <summary>
        /// Displays this instance.
        /// </summary>
        public void Display()
        {
            notifyIcon.Icon = Resources.icon;
            notifyIcon.Text = "Music Picker";
            notifyIcon.Visible = true;


            notifyIcon.ContextMenuStrip = menu.Menu;
        }

        /// <summary>
        /// Signs up.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="confirmpassword">The confirmpassword.</param>
        private void SignUp(string username, string password, string confirmpassword)
        {
            if (password == confirmpassword)
            {
                if (client.SignUp(username, password))
                {
                    notifyIcon.ShowBalloonTip(2000, "Successful registration", string.Format("Welcome {0} !", username),
                        ToolTipIcon.Info);
                }
                else
                {
                    notifyIcon.ShowBalloonTip(2000, "Registration failed", "Server error",
                        ToolTipIcon.Error);
                }
            }
            else
            {
                notifyIcon.ShowBalloonTip(2000, "Registration failed", "The passwords are different",
                        ToolTipIcon.Warning);
            }

        }

        /// <summary>
        /// Connects the specified device with the username account.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="deviceName">Name of the device.</param>
        /// <param name="password">The password.</param>
        private async void Connect(string username, string deviceName, string password)
        {
            if (client.LogIn(username, password))
            {
                int searchId = client.DeviceGetIdByName(deviceName);
                int deviceId = 0;
                if (searchId != -1)
                {
                    deviceId = searchId;
                }
                else
                {
                    deviceId = client.DeviceAdd(deviceName);
                }

                if (deviceId != -1)
                {
                    configuration.Model.Registered = true;
                    configuration.Model.DeviceName = deviceName;
                    configuration.Model.DeviceId = deviceId;
                    configuration.Model.Bearer = client.RetrieveBearer();
                    configuration.Save();

                    menu.ShowAuthenticatedMenu(deviceName, false, Disconnect);

                    notifyIcon.ShowBalloonTip(2000, "Successful connection", string.Format("Welcome {0} !", username),
                        ToolTipIcon.Info);

                    await UpdateLibrary();

                    socket = IO.Socket("http://localhost:3000");
                    socketInitialize();
                    socket.On("reconnect", socketInitialize);
                }
            }
            else
            {
                notifyIcon.ShowBalloonTip(2000, "Connection failed", "Error.",
                        ToolTipIcon.Error);
            }
        }

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        private void Disconnect()
        {
            ResetConfiguration();
            menu.LoadForm = new LibraryPathsForm(configuration.Model, UpdateLibraryPaths);
            menu.SignUpForm = new SignUpForm(SignUp);
            menu.ConnectionForm = new ConnectionForm(Connect);
            menu.ShowUnauthenticatedMenu();
        }

        /// <summary>
        /// Resets the configuration of the device model.
        /// </summary>
        private void ResetConfiguration()
        {
            configuration.Model.Registered = false;
            configuration.Model.Bearer = "";
            configuration.Model.DeviceId = 0;
            configuration.Model.DeviceName = "";
            configuration.Save();
        }

        /// <summary>
        /// Updates the library paths.
        /// </summary>
        /// <param name="paths">The paths.</param>
        private async void UpdateLibraryPaths(List<string> paths)
        {
            configuration.Model.Paths = paths;
            configuration.Save();
            await UpdateLibrary();
        }

        /// <summary>
        /// Updates the library.
        /// </summary>
        /// <returns>Task.</returns>
        private async Task UpdateLibrary()
        {
            library.Erase();
            menu.ShowAuthenticatedMenu(configuration.Model.DeviceName, true, Disconnect);
            foreach (string path in configuration.Model.Paths)
            {
                await Task.Run(() => seeker.GetTracks(path));
            }
            
            await client.DeviceCollectionSubmit(configuration.Model.DeviceId, library.Export());
            menu.ShowAuthenticatedMenu(configuration.Model.DeviceName, false, Disconnect);
        }

        /// <summary>
        /// Adds the track to the service.
        /// Not implemented.
        /// </summary>
        /// <param name="path">The path.</param>
        public void AddTrack(string path)
        {

        }

        /// <summary>
        /// Deletes the track of the service.
        /// Not implemented.
        /// </summary>
        /// <param name="path">The path.</param>
        public void DeleteTrack(string path)
        {

        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            notifyIcon.Dispose();
        }

    }
}
