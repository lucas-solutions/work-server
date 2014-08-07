
namespace Lucas.Solutions.IO
{
    public class Party
    {
        public virtual string User
        {
            get;
            set;
        }

        public virtual TransferDirection Direction
        {
            get;
            set;
        }

        public virtual string Email
        {
            get;
            set;
        }

        public virtual Host Host
        {
            get;
            set;
        }

        public virtual int HostId
        {
            get;
            set;
        }

        public virtual int Id
        {
            get;
            set;
        }

        public virtual string Name
        {
            get;
            set;
        }

        public virtual string Password
        {
            get;
            set;
        }

        /// <summary>
        /// Relative path
        /// </summary>
        public virtual string Path
        {
            get;
            set;
        }

        public virtual Transfer Transfer
        {
            get;
            set;
        }

        public virtual int TransferId
        {
            get;
            set;
        }

        public virtual bool Recursive
        {
            get;
            set;
        }

        public virtual string Summary
        {
            get;
            set;
        }

        public override string ToString()
        {
            return Name ?? "[Party]";
        }
    }
}
