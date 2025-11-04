using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using GenerationBot.config;


namespace GenerationBot.commands
{
    public class TestCommands : BaseCommandModule
    {
        [Command("test")]
        public async Task FirstTestCommand(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync($"Hi!{ctx.User.Username}");
        }

        [Command("add")]
        public async Task AddCommand(CommandContext ctx, int x, int y)
        {
            int res = x + y;
            await ctx.Channel.SendMessageAsync(res.ToString());
        }

        [Command("embed")]
        public async Task EmbededMessage(CommandContext ctx)
        {
            var message = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                    .WithTitle("Test of Discord Embed")
                    .WithDescription($"This command was executed by {ctx.User.Username}")
                    .WithColor(DiscordColor.Blurple));
            await message.SendAsync(ctx.Channel);
        }

        [Command("add4me")]
        public async Task EmbedAddCommand(CommandContext ctx, int x, int y)
        {
            int res = x + y;
            var message = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                    .WithTitle("I love adding add add add add...")
                    .WithDescription(res.ToString())
                    .WithColor(DiscordColor.Purple)
                );
            await message.SendAsync(ctx.Channel);
        }

        [Command("Which_cat_is_cuter")]
        public async Task CompareCatsCommand(CommandContext ctx, string cat1, string cat2)
        {
            string res = "Nier";
            var message = new DiscordEmbedBuilder
            {
                Title = $"Oh that is easy!",
                Description = res,
                Color = DiscordColor.Blue
            };
            await ctx.Channel.SendMessageAsync(embed: message);
        }
    }
}