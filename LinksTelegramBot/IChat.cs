namespace LinksTelegramBot
{
    public interface IChat
    {
        public delegate void NewChatMessageDelegate();
        event NewChatMessageDelegate OnNewChatMessage;
        void NewChatMessageReceiver() { }
        void PostMessageToChat();
        void Start();
        void Stop();
    }
}
