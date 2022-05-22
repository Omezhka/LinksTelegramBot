﻿using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace LinksTelegramBot
{
    public interface IChat
    {
        delegate void NewChatMessageDelegate(object source, EventArgs eventArgs);
        event NewChatMessageDelegate NewChatMessage;
        void OnNewChatMessage(object source, EventArgs eventArgs);
        void NewChatMessageReceiver();
        void PostMessageToChat();
        void Start();
        void Stop();
    }
}
