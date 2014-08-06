using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions
{
    public interface ITalkProvider
    {
        void Register(ITalk talker);

        ITalk Resolve(TalkProtocol protocol);
    }
}
