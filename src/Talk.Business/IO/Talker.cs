using System.IO;
using System.Threading.Tasks;

namespace Lucas.Solutions.IO
{
    public abstract class Talker : ITalk
    {
        public abstract TalkProtocol Protocol { get; }

        public abstract Task PullAsync(TalkContext context, Stream stream);

        public abstract Task PushAsync(TalkContext context, Stream stream);
    }
}
