﻿// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

//Opens shortcut
ProcessStartInfo info = new ProcessStartInfo("Bot - Chrome.lnk");
info.UseShellExecute = true;
Process.Start(info);

Console.WriteLine("Starting the bot...");
Thread.Sleep(1000);

Driver chromeBrowser = new Driver();

chromeBrowser.FirstPage();
chromeBrowser.SecondPage();
chromeBrowser.ThirdPage();