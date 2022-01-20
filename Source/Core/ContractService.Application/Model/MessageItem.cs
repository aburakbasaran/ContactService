using ContactService.Application.Enum;

namespace ContactService.Application.Model
{
    public class MessageItem
    {
        public MessageType Type { get; set; }
        public string Message { get; set; }
    }
}
