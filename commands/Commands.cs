using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace GenerationBot.commands
{
    public class Commands : BaseCommandModule
    {
        [Command("generation")]
        public async Task Generation(CommandContext ctx)
        {
            string dominantCategory = SlangCount.GetDominantCategory(ctx.User.Id);
            
            await ctx.Channel.SendMessageAsync($"Your Generation: {dominantCategory}");
        }
        
        [Command("allstats")]
        public async Task AllStats(CommandContext ctx)
        {
            string dominantCategory = SlangCount.GetDominantCategory(ctx.User.Id);
            
            int zoomerCount = SlangCount.GetZoomerCount(ctx.User.Id);
            int millennialCount = SlangCount.GetMillennialCount(ctx.User.Id);
            
            await ctx.Channel.SendMessageAsync(
                $"Zoomer Slang Count: {zoomerCount}\n" +
                $"Millennial Slang Count: {millennialCount}\n" +
                $"Your Generation: {dominantCategory}"
            );
        }

        [Command("cooked")]
        public async Task CookedCount(CommandContext ctx)
        {
            int count = SlangCount.GetCount(ctx.User.Id, "cooked");

            await ctx.Channel.SendMessageAsync(
                $"{ctx.User.Username}, has said **cooked** {count} time{(count == 1 ? "" : "s")}."
            );
        }
    }
}