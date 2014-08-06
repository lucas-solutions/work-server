using System.IO;
using System.Threading.Tasks;

namespace Lucas.Solutions
{
    public interface ITalk
    {
        TalkProtocol Protocol { get; }

        Task PullAsync(TalkContext context, Stream stream);

        Task PushAsync(TalkContext context, Stream stream);
    }
}
