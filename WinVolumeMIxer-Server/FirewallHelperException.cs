using System;

namespace WindowsFirewallManager
{
    /// 
    /// Describes a FirewallHelperException.
    /// 
    public class FirewallHelperException : Exception
    {
        /// 
        /// Construct a new FirewallHelperException
        /// 
        /// 
        public FirewallHelperException(string message)
            : base(message)
        { }
    }
}