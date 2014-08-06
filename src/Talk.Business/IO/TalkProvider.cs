using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.IO
{
    public class TalkProvider
    {
        private readonly List<ITalk> _talkers = new List<ITalk>();

        public void Register(ITalk talker)
        {
            _talkers.Add(talker);
        }

        public ITalk Resolve(TalkProtocol protocol)
        {
            return _talkers.LastOrDefault(talker => talker.Protocol == protocol);
        }
    }
}
