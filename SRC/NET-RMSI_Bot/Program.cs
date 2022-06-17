﻿using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;





namespace NETRMSI_Bot
{
    class Program
    {
        public static void Main(string[] args)
        => new Program().MainAsync().GetAwaiter().GetResult();

        private DiscordSocketClient client;
        public async Task MainAsync()
        {
            client = new DiscordSocketClient();
            client.MessageReceived += CommandHandler;
            client.Log += Log;

            YamlSerialization.Config_Yaml();

            //var token = File.ReadAllText("Token.txt");
            //var token = "NTExMTk5MzU3MjA3NTc2NTc4.W-hJgA.Bcs2Jf8Vzgqf_zvJNS0gaMROgHk";
            




            await client.LoginAsync(TokenType.Bot, YamlSerialization.token);
            await client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private Task CommandHandler(SocketMessage message)
        {
            string command = "";
            int lengthOfCommand = -1;


            if (!message.Content.StartsWith("!"))
                return Task.CompletedTask;

            if (message.Author.IsBot)
                return Task.CompletedTask;

            if (message.Content.Contains(' '))
                lengthOfCommand = message.Content.IndexOf(' ');
            else
                lengthOfCommand = message.Content.Length;

            command = message.Content.Substring(1, lengthOfCommand - 1).ToLower();


            if (command.Equals("nextcloud"))
            {
                Serverstatus.Serverping_Nextcloud();

                message.Channel.SendMessageAsync(message.Author.Mention + " Pinging Server... " + "\n" + Serverstatus.serverstat_flarenet_nextcloud);
                message.Channel.SendMessageAsync(Serverstatus.serverstat_flarenet_nextcloud_advstats);


            }

            if (command.Equals("minecraftjava-personalsrv"))
            {
                Serverstatus.Serverping_minecraftjava_personalsrv();
                message.Channel.SendMessageAsync(message.Author.Mention + " Pinging Server... " + "\n" + Serverstatus.serverstat_delta1_minecraftjava_personalsrv);
            }


            return Task.CompletedTask;
        }

       
    }
}

