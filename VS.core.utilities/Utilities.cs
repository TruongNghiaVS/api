using NAudio.Wave;
using System.Net;

namespace VS.core.Utilities
{

    public static class Utils
    {
        static string start = "KPMG_EV";
        static string end = "KPMG_PM";
        private static readonly char[] ShortName1 = { 'A', 'B', 'C', 'D', 'E', 'F', 'Y' };
        private static readonly char[] ShortName2 = { 'G', 'H', 'I', 'J', 'K', 'L', 'Q' };
        private static readonly char[] ShortName3 = { 'M', 'N', 'O', 'P', 'R', 'Z' };
        private static readonly char[] ShortName4 = { 'S', 'T', 'U', 'V', 'W', 'X' };
        public static string GetAvatarColor(this char inputChar)
        {
            if (ShortName1.Contains(inputChar))
                return "#fd7e14";
            if (ShortName2.Contains(inputChar))
                return "#31a8d2";
            if (ShortName3.Contains(inputChar))
                return "#20c997";
            if (ShortName4.Contains(inputChar))
                return "#2e7d32";
            return "#2e7d32";
        }


        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static string getMD5(string data)
        {
            return BitConverter.ToString(encryptData(start + data + end)).Replace("-", "").ToLower();
        }
        private static byte[] encryptData(string data)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashedBytes;
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(data));
            return hashedBytes;
        }
        public static bool IsSubsetOf<T>(this IEnumerable<T> a, IEnumerable<T> b)
        {
            return !a.Except(b).Any();
        }
        public static TimeSpan GetTimeZoneOffset(string timeZoneId)
        {
            TimeZoneInfo systemTimeZoneById;
            if (string.IsNullOrEmpty(timeZoneId) || (systemTimeZoneById = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId)) == null)
                return new TimeSpan(0L);
            return systemTimeZoneById.BaseUtcOffset;
        }

        public static DateTime GetStartDate(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        }

        public static DateTime GetEndDate(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
        }
        public static DateTime ToStartDateTime(this object item)
        {
            return item.ToDateTime(new DateTime()).StartDateTime();
        }
        public static DateTime ToDateTime(this object item, DateTime defaultDateTime = default(DateTime))
        {
            DateTime result;
            if (string.IsNullOrEmpty(item?.ToString()) || !DateTime.TryParse(item.ToString(), out result))
                return defaultDateTime;
            return result;
        }
        public static DateTime StartDateTime(this DateTime item)
        {
            return new DateTime(item.Year, item.Month, item.Day);
        }
        public static DateTime? StartDateTime(this DateTime? item)
        {
            if (!item.HasValue)
                return new DateTime?();
            return new DateTime?(new DateTime(item.Value.Year, item.Value.Month, item.Value.Day));
        }
        public static DateTime? ToStartDateTimeNull(this object item)
        {
            return item.ToDateTimeNull().StartDateTime();
        }
        public static DateTime? ToDateTimeNull(this object item)
        {
            if (string.IsNullOrEmpty(item?.ToString()))
                return new DateTime?();
            DateTime result;
            if (!DateTime.TryParse(item.ToString(), out result))
                return new DateTime?();
            return new DateTime?(result);
        }
        public static DateTime ToEndDateTime(this object item)
        {
            return item.ToDateTime(new DateTime()).EndDateTime();
        }

        public static DateTime? ToEndDateTimeNull(this object item)
        {
            return item.ToDateTimeNull().EndDateTime();
        }
        public static DateTime EndDateTime(this DateTime item)
        {
            return new DateTime(item.Year, item.Month, item.Day, 23, 59, 59);
        }

        public static DateTime? EndDateTime(this DateTime? item)
        {
            if (!item.HasValue)
                return new DateTime?();
            return new DateTime?(new DateTime(item.Value.Year, item.Value.Month, item.Value.Day, 23, 59, 59));
        }

        public static double? GetDurationAudio(this string? filePath)
        {

            string path = @"http://192.168.1.151:3002/api/getFileAudio?filePath=";
            path = path + "" + filePath;
            Byte[] bytes = null;
            using (var webClient = new WebClient())
            {
                try
                {
                    bytes = webClient.DownloadData(path);
                }
                catch (Exception e)
                {
                    return 0;
                    string path2 = @"http://192.168.1.10:3002/api/getFileAudio?filePath=";
                    path2 = path2 + "" + filePath;
                    try
                    {
                        bytes = webClient.DownloadData(path2);
                    }
                    catch (Exception)
                    {
                        return 0;
                        string path3 = @"http://192.168.1.9:3002/api/getFileAudio?filePath=";
                        path3 = path3 + "" + filePath;
                        try
                        {
                            bytes = webClient.DownloadData(path3);
                        }
                        catch (Exception)
                        {

                            string path4 = @"http://192.168.1.151:3002/api/getFileAudio?filePath=";
                            path4 = path4 + "" + filePath;

                            try
                            {
                                bytes = webClient.DownloadData(path4);
                            }
                            catch (Exception)
                            {
                                return 0;
                            }
                        }
                    }

                }
                if (bytes == null)
                {
                    return 0;
                }
                if (bytes.Length > 0)
                {
                    var stream = new MemoryStream(bytes);
                    WaveFileReader wr = new WaveFileReader(stream);
                    TimeSpan span = wr.TotalTime;

                    return span.TotalSeconds;
                }
                return 0;
            }
        }
        public static string GetFileRecordingFile(string fileAudio = "out-0383338840-9005-20230119-081349-1674116029.387926.wav", DateTime? callDate = null)
        {
            if (string.IsNullOrEmpty(fileAudio))
            {
                return "";
            }
            if (callDate == null)
            {
                return "";
            }
            var path = "/var/spool/asterisk/monitor/";
            path = path + "" + callDate.Value.Year + "/";
            path = path + "" + callDate.Value.Month.ToString("00") + "/";
            path = path + "" + callDate.Value.Day.ToString("00") + "/";
            path = path + fileAudio;
            return path;
        }
    }
}