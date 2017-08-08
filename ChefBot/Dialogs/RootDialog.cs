using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using ChefBot.EF;
using System.Linq;
using System.Collections.Generic;

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
            var reply = context.MakeMessage();

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
                        List<ReceiptItem> receiptList = new List<ReceiptItem>();
                        recipe.Ingredients.Split('\n').ToList().ForEach(item =>
                        {
                            ReceiptItem listItem = new ReceiptItem()
                            {
                                Title = item,
                                Subtitle = "",
                                Text = null
                            };
                            receiptList.Add(listItem);
                        });
                        ReceiptCard receiptCard = new ReceiptCard()
                        {
                            Title = recipe.Name,
                            Buttons = new List<CardAction>()
                            {
                                new CardAction()
                                {
                                     Title = "Tam tarifi ara",
                                      Type = ActionTypes.OpenUrl,
                                      Value = $"https://www.google.com.tr/search?q={recipe.Name}"
                                }
                            },
                            Items = receiptList
                        };
                        Attachment plAttachment = receiptCard.ToAttachment();
                        reply.Attachments.Add(plAttachment);

                        await context.PostAsync(reply);
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