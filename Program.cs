using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using GenerationBot.commands;
using GenerationBot.config;

namespace GenerationBot
{
    internal class Program
    {
        public static DiscordClient Client { get; private set; }
        private static CommandsNextExtension Commands { get; set; }

        static async Task Main(string[] args)
        {
            var jsonReader = new JSONReader();
            await jsonReader.ReadJSON();

            var discordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = jsonReader.token,
                TokenType = TokenType.Bot,
                AutoReconnect = true
            };

            Client = new DiscordClient(discordConfig);

            Client.UseInteractivity(new InteractivityConfiguration()
            {
                Timeout = TimeSpan.FromSeconds(25)
            });
            
            Client.MessageCreated += OnMessageCreated;

            Client.Ready += Client_Ready;

            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new string[] { jsonReader.prefix },
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false
            };

            Commands = Client.UseCommandsNext(commandsConfig);
            Commands.RegisterCommands<Commands>();

            await Client.ConnectAsync();
            await Task.Delay(-1); 
        }
        
        private static Task OnMessageCreated(DiscordClient client, MessageCreateEventArgs e)
        {
            if (!e.Author.IsBot)
            {
                SlangCount.CountWords(e.Author.Id, e.Message.Content);
            }

            return Task.CompletedTask;
        }

        public static Task Client_Ready(DiscordClient sender, ReadyEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}
