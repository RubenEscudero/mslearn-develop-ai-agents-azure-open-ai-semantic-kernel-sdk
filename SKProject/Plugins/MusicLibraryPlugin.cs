﻿using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace SKProject.Plugins
{
    public class MusicLibraryPlugin
    {
        [KernelFunction,
        Description("Get a list of music recently played by the user")]
        public static string GetRecentPlays()
        {
            string dir = Directory.GetCurrentDirectory();
            string content = File.ReadAllText("D:\\Proyectos\\mslearn-develop-ai-agents-azure-open-ai-semantic-kernel-sdk\\SKProject\\data\\recentlyplayed.txt");
            return content;
        }

        [KernelFunction, Description("Add a song to the recently played list")]
        public static string  AddToRecentlyPlayed(
            [Description("The name of the artist")] string artist,
            [Description("The title of the song")] string song,
            [Description("The song genre")] string genre)
        {
            // Read the existing content from the file
            string filePath = "D:\\Proyectos\\mslearn-develop-ai-agents-azure-open-ai-semantic-kernel-sdk\\SKProject\\data\\recentlyplayed.txt";
            string jsonContent = File.ReadAllText(filePath);
            var recentlyPlayed = (JsonArray) JsonNode.Parse(jsonContent);

            var newSong = new JsonObject
            {
                ["title"] = song,
                ["artist"] = artist,
                ["genre"] = genre
            };

            recentlyPlayed.Insert(0, newSong);
            File.WriteAllText(filePath, JsonSerializer.Serialize(recentlyPlayed, 
                new JsonSerializerOptions { WriteIndented = true}));

            return $"Added '{song}' to recently played";
        }

        [KernelFunction, Description("Get a list of music available to the user")]
        public static string GetMusicLibrary()
        {
            string dir = Directory.GetCurrentDirectory();
            string content = File.ReadAllText("D:\\Proyectos\\mslearn-develop-ai-agents-azure-open-ai-semantic-kernel-sdk\\SKProject\\data\\musiclibrary.txt");
            return content;
        }
    }
}
