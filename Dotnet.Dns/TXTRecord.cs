#region
//
// Bdev.Net.Dns by Rob Philpott, Big Developments Ltd. Please send all bugs/enhancements to
// rob@bigdevelopments.co.uk  This file and the code contained within is freeware and may be
// distributed and edited without restriction.
// 

#endregion

using System;
using System.Text;

namespace Bdev.Net.Dns {
    /// <summary>
    /// An MX (Mail Exchanger) Resource Record (RR) (RFC1035 3.3.9)
    /// </summary>
    [Serializable]
    public class TXTRecord : RecordBase {
        // an MX record is a domain name and an integer preference
        private readonly string _txt;

        // expose these fields public read/only
        public string TXT { get { return _txt; } }

        /// <summary>
        /// Constructs an MX record by reading bytes from a return message
        /// </summary>
        /// <param name="pointer">A logical pointer to the bytes holding the record</param>
        internal TXTRecord(Pointer pointer) {
            int length = 0;

            // get  the length of the first label
            length = pointer.ReadByte();
            var desArray = new byte[length];
            Array.Copy(pointer.Message, 1, desArray, 0, length);
            _txt = Encoding.Default.GetString(desArray);
        }
    }
}
