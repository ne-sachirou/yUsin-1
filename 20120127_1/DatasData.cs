using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Collections;


namespace _20120127_1
{
    public abstract class DatasData
    {
        protected string CurrentPath = string.Empty;
        protected string DatasFileName = string.Empty;

        public DatasData(string currentPath)
        {
            this.CurrentPath = currentPath;
        }

        protected string DataPath
        {
            get
            {
                return CurrentPath + "\\csv\\" + DatasFileName;
            }
        }

        protected string ComputeHashString(string rawString)
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            SHA256 hashM = new SHA256Managed();
            return encoding.GetString(hashM.ComputeHash(encoding.GetBytes(rawString))).
                Replace(',', '.').Replace('\r', '.').Replace('\n', '.');
        }

        protected string EscapeForCsv(string rawString)
        {
            return rawString.Replace(",", "，").Replace("\"", "”").Replace("\r", "\\r").Replace("\n", "\\n");
        }

        protected string UnescapeForCsv(string escapedString)
        {
            return escapedString.Replace("\\r", "\r").Replace("\\n", "\n");
        }

        protected abstract Dictionary<string, string> GenerateDictionary(string[] data);

        protected string GetHeadLine()
        {
            using (StreamReader reader = new StreamReader(DataPath))
            {
                return reader.ReadLine();
            }
        }

        /// <summary>
        /// This may throw InvalidOperationException.
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<string[]> Enumerable()
        {
            if (!File.Exists(DataPath))
            {
                throw new InvalidOperationException("File not Exists");
            }
            List<string[]> records = new List<string[]>();
            using (StreamReader reader = new StreamReader(DataPath))
            {
                reader.ReadLine();
                while (reader.Peek() != -1)
                {
                    string[] record = reader.ReadLine().Split(',');
                    if (record.Length >= 2)
                    {
                        for (int i = 0, len = record.Length; i < len; ++i)
                        {
                            record[i] = record[i].Replace("\"", "");
                        }
                    }
                    records.Add(record);
                }
            }
            foreach (string[] record in records)
            {
                yield return record;
            }
        }

        /// <summary>
        /// This may throw InvalidOperationException.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Dictionary<string, string>> GetEnumerator()
        {
            foreach (string[] data in Enumerable())
            {
                yield return GenerateDictionary(data);
            }
        }

        /// <summary>
        /// This may throw InvalidOperationException.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="colunmNumber"></param>
        /// <returns></returns>
        private string[] FindRecordArray(string column, int colunmNumber)
        {
            if (!File.Exists(DataPath))
            {
                throw new InvalidOperationException("File not Exists: " + DataPath);
            }
            using (StreamReader reader = new StreamReader(DataPath))
            {
                while (reader.Peek() != -1)
                {
                    string[] record = reader.ReadLine().Split(',');
                    if (record.Length > colunmNumber && record[colunmNumber].Replace("\"", "").Equals(column))
                    {
                        for (int i = 0, len = record.Length; i < len; ++i)
                        {
                            record[i] = record[i].Replace("\"", "");
                        }
                        return record;
                    }
                }
            }
            return new string[] { };
        }

        /// <summary>
        /// This may throw InvalidOperationException.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="colunmNumber"></param>
        /// <returns></returns>
        protected Dictionary<string, string> FindRecord(string column, int colunmNumber)
        {
            string[] record = FindRecordArray(column, colunmNumber);
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (record.Length > 0)
            {
                result = GenerateDictionary(record);
            }
            return result;
        }

        /// <summary>
        /// This may throw InvalidOperationException.
        /// </summary>
        /// <param name="fun"></param>
        /// <returns></returns>
        protected string[][] FindRecords(Func<string[], bool> fun)
        {
            List<string[]> records = new List<string[]>();
            foreach (string[] record in this.Enumerable())
            {
                if (fun(record))
                {
                    records.Add(record);
                }
            }
            return records.ToArray();
        }

        /// <summary>
        /// This may throw InvalidOperationException.
        /// </summary>
        /// <param name="fun"></param>
        /// <returns></returns>
        protected Dictionary<string, string>[] FindRecords(Func<Dictionary<string, string>, bool> fun)
        {
            string[][] records = FindRecords(delegate(string[] record)
            {
                return fun(GenerateDictionary(record));
            });
            Dictionary<string, string>[] result = new Dictionary<string, string>[records.Length];
            for (int i = 0; i < records.Length; ++i)
            {
                result[i] = GenerateDictionary(records[i]);
            }
            return result;
        }

        protected void AddRecord(string[] data)
        {
            StreamWriter writer = new StreamWriter(DataPath, true);
            try
            {
                writer.WriteLine("\"" + string.Join("\",\"", data) + "\"");
            }
            finally
            {
                writer.Close();
            }
        }

        protected void RemoveRecord(string column, int colunmNumber)
        {
            string result = GetHeadLine() + Environment.NewLine;
            foreach (string[] record in this.Enumerable())
            {
                if (!(record.Length > colunmNumber && record[colunmNumber] == column))
                {
                    result += "\"" + string.Join("\",\"", record) + "\"" + Environment.NewLine;
                }
            }
            using (StreamWriter writer = new StreamWriter(DataPath, false))
            {
                writer.Write(result);
            }
        }

        protected void RemoveRecords(Func<string[], bool> fun)
        {
            string result = GetHeadLine() + Environment.NewLine;
            foreach (string[] record in this.Enumerable())
            {
                if (!fun(record))
                {
                    result += "\"" + string.Join("\",\"", record) + "\"" + Environment.NewLine;
                }
            }
            using (StreamWriter writer = new StreamWriter(DataPath, false))
            {
                writer.Write(result);
            }
        }

        protected void RemoveRecords(Func<Dictionary<string, string>, bool> fun)
        {
            RemoveRecords(delegate(string[] record)
            {
                return fun(GenerateDictionary(record));
            });
        }
    }
}
