using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HazeronMapper
{
    #region HMail class
    static class HMail
    {
        static public byte[] Read(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }

        static public bool IsCityReport(int messageType) // Give true if MessageType is a City Report (or related) mail.
        {
            if (messageType == 0x01 // MSG_CityStatusReport
                   || messageType == 0x04 // MSG_CityDistressReport
                   || messageType == 0x06 // MSG_CityStatusReportInfo
                   || messageType == 0x17 // MSG_CityFinalDecayReport
                   )
                return true;
            return false;
        }
        static public bool IsCityReport(HMailObj mail) // Same as above but mail input.
        {
            return IsCityReport(mail.MessageType);
        }
        static public bool IsCityReport(byte[] mailBytes) // Same as above but mailBytes input.
        {
            return IsCityReport(mailBytes[19 + Helper.ToInt32(mailBytes, 15) + 9]);
        }
        static public bool IsCityReport(string filePath) // Same as above but filePath input.
        {
            return IsCityReport(HMail.Read(filePath));
        }

        static public bool IsShipLog(int messageType) // Give true if MessageType is a Ship Log (or related) mail.
        {
            if (messageType == 0x0A // MSG_ShipLog
                || messageType == 0x10 // MSG_ShipLogAlert
                || messageType == 0x11 // MSG_ShipLogDistress
                || messageType == 0x12 // MSG_ShipLogFinal
                )
                return true;
            return false;
        }
        static public bool IsShipLog(HMailObj mail) // Same as above but mail input.
        {
            return IsShipLog(mail.MessageType);
        }
        static public bool IsShipLog(byte[] mailBytes) // Same as above but mailBytes input.
        {
            return IsShipLog(mailBytes[19 + Helper.ToInt32(mailBytes, 15) + 9]);
        }
        static public bool IsShipLog(string filePath) // Same as above but filePath input.
        {
            return IsShipLog(HMail.Read(filePath));
        }

        static public bool IsGovernmentMessage(int messageType) // Give true if MessageType is a Government (or related) mail.
        {
            if (messageType == 0x13 // MSG_Government
                || messageType == 0x18 // MSG_DiplomaticMessage
                )
                return true;
            return false;
        }
        static public bool IsGovernmentMessage(HMailObj mail) // Same as above but mail input.
        {
            return IsGovernmentMessage(mail.MessageType);
        }
        static public bool IsGovernmentMessage(byte[] mailBytes) // Same as above but mailBytes input.
        {
            return IsGovernmentMessage(mailBytes[19 + Helper.ToInt32(mailBytes, 15) + 9]);
        }
        static public bool IsGovernmentMessage(string filePath) // Same as above but filePath input.
        {
            return IsGovernmentMessage(HMail.Read(filePath));
        }

        static public bool IsShipReport(int messageType) // Give true if MessageType is a ShipReport (or related) mail.
        {
            if (messageType == 0x15 // MSG_ShipReport
                )
                return true;
            return false;
        }
        static public bool IsShipReport(HMailObj mail) // Same as above but mail input.
        {
            return IsShipReport(mail.MessageType);
        }
        static public bool IsShipReport(byte[] mailBytes) // Same as above but mailBytes input.
        {
            return IsShipReport(mailBytes[19 + Helper.ToInt32(mailBytes, 15) + 9]);
        }
        static public bool IsShipReport(string filePath) // Same as above but filePath input.
        {
            return IsShipReport(HMail.Read(filePath));
        }
    }
    #endregion

    #region HMailObj class
    class HMailObj
    {
        // Anr's mail header info sheet: http://goo.gl/E0yoYd

        protected string _filePath;
        public string FilePath
        {
            get { return _filePath; }
        }
        protected byte[] _mailBytes;
        public byte[] MailBytes
        {
            get { return _mailBytes; }
        }

        public int Date
        {
            get { return Helper.ToInt32(_mailBytes, 6); }
        }
        public int Time
        {
            get { return Helper.ToInt32(_mailBytes, 10); }
        }
        public DateTime DateTime
        {
            get
            { // http://mikearnett.wordpress.com/2011/09/13/c-convert-julian-date/
                long L = Date + 68569;
                long N = (long)((4 * L) / 146097);
                L = L - ((long)((146097 * N + 3) / 4));
                long I = (long)((4000 * (L + 1) / 1461001));
                L = L - (long)((1461 * I) / 4) + 31;
                long J = (long)((80 * L) / 2447);
                int Day = (int)(L - (long)((2447 * J) / 80));
                L = (long)(J / 11);
                int Month = (int)(J + 2 - 12 * L);
                int Year = (int)(100 * (N - 49) + I + L);
                return new DateTime(Year, Month, Day).Add(TimeSpan.FromMilliseconds(Time));
            }
        }
        public string DateTimeString
        {
            get
            {
                return DateTime.ToString("dd-MM-yyyy HH:mm:ss"); // TimeDate format information: http://msdn.microsoft.com/en-us/library/8kb3ddd4(v=vs.110).aspx
            }
        }

        protected int _from_l, _subj_l, _body_l;
        public string From
        {
            get { return Helper.ToString(_mailBytes, 19, _from_l); }
        }
        public string Subject
        {
            get { return Helper.ToString(_mailBytes, 19 + _from_l + 14, _subj_l); }
        }
        public string Body
        {
            get { return Helper.ToString(_mailBytes, 19 + _from_l + 14 + _subj_l + 4, _body_l); }
        }

        public int MessageType
        {
            get { return _mailBytes[19 + _from_l + 9]; }
        }

        public HMailObj(string filePath)
        {
            _filePath = filePath;
            _mailBytes = HMail.Read(_filePath);
            _from_l = Helper.ToInt32(_mailBytes, 15);
            _subj_l = Helper.ToInt32(_mailBytes, 19 + _from_l + 10);
            _body_l = Helper.ToInt32(_mailBytes, 19 + _from_l + 14 + _subj_l);
        }
    }
    #endregion

    #region Helper class
    class Helper
    {
        static public string ToHex(byte[] bytes) // http://stackoverflow.com/a/10048895
        {
            char[] c = new char[bytes.Length * 3];

            byte b;

            for (int bx = 0, cx = 0; bx < bytes.Length; ++bx, ++cx)
            {
                b = ((byte)(bytes[bx] >> 4));
                c[cx] = (char)(b > 9 ? b - 10 + 'A' : b + '0');

                b = ((byte)(bytes[bx] & 0x0F));
                c[++cx] = (char)(b > 9 ? b - 10 + 'A' : b + '0');
                c[++cx] = '-';
            }

            return new string(c);
        }
        static public string ToHex(byte singleByte) // same as above my modified to only be one byte
        {
            char[] c = new char[2];

            byte b;

            b = ((byte)(singleByte >> 4));
            c[0] = (char)(b > 9 ? b - 10 + 'A' : b + '0');

            b = ((byte)(singleByte & 0x0F));
            c[1] = (char)(b > 9 ? b - 10 + 'A' : b + '0');

            return new string(c);
        }

        static public int ToInt32(byte[] bytes, int startIndex)
        {
            byte[] subBytes = Helper.SubArray(bytes, startIndex, 4);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(subBytes);
            return BitConverter.ToInt32(subBytes, 0);
        }

        static public string ToString(byte[] bytes, int startIndex, int length)
        {
            byte[] subBytes = Helper.SubArray(bytes, startIndex, length);
            //subBytes = Helper.ConcatinateArray(new byte[] { 0xFE, 0xFF }, subBytes);
            //if (BitConverter.IsLittleEndian)
            //    Array.Reverse(subBytes);
            string text = Encoding.BigEndianUnicode.GetString(subBytes); // UTF-16 BigEndian to string.
            return text;
        }

        static public byte[] SubArray(byte[] bytes, int startIndex, int length)
        {
            byte[] rv = new byte[length];
            System.Buffer.BlockCopy(bytes, startIndex, rv, 0, length);
            return rv;
            //return new List<byte>(bytes).GetRange(startIndex, length).ToArray(); // Another ways of doing it.
        }

        static public byte[] ConcatinateArray(byte[] array1, byte[] array2)
        {
            byte[] rv = new byte[array1.Length + array2.Length];
            System.Buffer.BlockCopy(array1, 0, rv, 0, array1.Length);
            System.Buffer.BlockCopy(array2, 0, rv, array1.Length, array2.Length);
            return rv;
        }
        static public byte[] ConcatinateArray(byte[] array1, byte[] array2, byte[] array3)
        {
            byte[] rv = new byte[array1.Length + array2.Length + array3.Length];
            System.Buffer.BlockCopy(array1, 0, rv, 0, array1.Length);
            System.Buffer.BlockCopy(array2, 0, rv, array1.Length, array2.Length);
            System.Buffer.BlockCopy(array3, 0, rv, array1.Length + array2.Length, array3.Length);
            return rv;
        }

        static public string CleanText(string input) // Removes the html code tags.
        {
            int tagStart, tagEnd;
            string processed = "";
            while (input.Contains("<") && input.Contains(">"))
            {
                tagStart = input.IndexOf('<');
                tagEnd = input.IndexOf('>') - tagStart;
                processed += input.Remove(tagStart);
                string tag = input.Substring(tagStart + 1, tagEnd - 1);
                input = input.Substring(tagStart + tagEnd + 1);
                switch (tag.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0])
                {
                    case "br":
                    case "div":
                    case "/div":
                    case "/td":
                    case "/tr":
                        processed += Environment.NewLine;
                        break;
                    //case "b":
                    //case "/b":
                    //case "td":
                    //case "tr":
                    //    break;
                }
            }
            return processed.Trim().Replace(Environment.NewLine + Environment.NewLine + Environment.NewLine, Environment.NewLine).Replace("&nbsp;"," "); // Trim for good measure and remove triple NewLine.
        }
    }
    #endregion
}
