namespace LinksTelegramBot
{
    public class TgBotApiChatHandler : IChat
    {
        public event IChat.NewChatMessageDelegate? OnNewChatMessage;    
        public void NewChatMessageReceiver()
        {
            OnNewChatMessage();
        }

        public void PostMessageToChat()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            NewChatMessageReceiver();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
