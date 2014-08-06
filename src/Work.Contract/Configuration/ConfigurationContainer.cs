using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Lucas.Solutions.Configuration
{
    using Lucas.Solutions.Authentication;
    using Lucas.Solutions.Authorization;
    using Lucas.Solutions.Automation;
    using Lucas.Solutions.IO;

    public class ConfigurationContainer
    {
        private MasterdataSection _masterdata;
        private ICollection<Host> _hosts;
        private ICollection<Role> _roles;
        private ICollection<Task> _tasks;
        private ICollection<Transfer> _transfers;
        private ICollection<User> _users;

        public MasterdataSection Masterdata
        {
            get { return _masterdata ?? (_masterdata = ConfigurationManager.GetSection("application/masterdata") as MasterdataSection); }
        }

        public ICollection<Host> Hosts
        {
            get { return _hosts ?? (_hosts = Array.AsReadOnly(Masterdata.Hosts.Select(el => (Host)el).ToArray())); }
        }

        public ICollection<Role> Roles
        {
            get { return _roles ?? (_roles = Array.AsReadOnly(Masterdata.Roles.Select(el => (Role)el).ToArray())); }
        }

        public ICollection<Task> Tasks
        {
            get
            {
                return _tasks ?? (_tasks = Array.AsReadOnly(
                    Transfers.Select(transfer => (Task)transfer)
                        .ToArray()));
            }
        }

        public ICollection<Transfer> Transfers
        {
            get
            {
                if (_transfers != null) return _transfers;

                var hosts = Hosts;
                _transfers = Array.AsReadOnly(Masterdata.Transfers.Select(el => (Transfer)el).ToArray());
                
                foreach (var transfer in _transfers)
                {
                    foreach (var party in transfer.Parties)
                    {
                        var name = party.Host.Name;
                        party.Host = hosts.SingleOrDefault(host => host.Name == name);
                    }
                }

                var partiesWithoutHost = _transfers.SelectMany(transfer => transfer.Parties.Where(party => party.Host == null)).ToArray();

                if (partiesWithoutHost.Any())
                {
                }

                return _transfers;
            }
        }

        public ICollection<User> Users
        {
            get { return _users ?? (_users = Array.AsReadOnly(Masterdata.Users.Select(el => (User)el).ToArray())); }
        }
    }
}
