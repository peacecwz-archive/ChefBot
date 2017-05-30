using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using ChefBot.EF;
using System.Linq;

namespace ChefBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            activity.AsTypingActivity();
            if (!string.IsNullOrEmpty(activity.Text))
            {
                using (DataContext db = new DataContext())
                {
                    db.BotHistories.Add(new EF.Tables.BotHistoryEntity()
                    {
                        Message = activity.Text,
                        UserId = activity.From.Id,
                        Source = activity.ChannelId
                    });
                    try
                    {
                        db.SaveChanges();
                    }
                    catch { }
                    var recipe = db.Recipes.FirstOrDefault(p => p.Name == activity.Text.ToLower());
                    if (recipe != null)
                    {
                        await context.PostAsync(recipe.Ingredients);
                    }
                    else
                    {
                        await context.PostAsync("Bu yemek tarifi bulunamadı");
                    }
                }
            }

            context.Wait(MessageReceivedAsync);
        }
    }
}