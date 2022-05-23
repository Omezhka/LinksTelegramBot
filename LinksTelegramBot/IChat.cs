using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace LinksTelegramBot
{
    public interface IChat
    {
        //delegate void NewChatMessageDelegate(object sender, EventArgs eventArgs);
        event EventHandler<NewMessageEventArgs> NewChatMessage;
       // void OnNewChatMessage(object sender, EventArgs eventArgs);
          
        void NewChatMessageReceiver();
        void PostMessageToChat();
        void Start();
        void Stop();
    }
}
