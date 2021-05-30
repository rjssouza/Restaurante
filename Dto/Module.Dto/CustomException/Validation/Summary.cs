using System;
using System.Collections.Generic;
using System.Text;

namespace Module.Dto.CustomException.Validation
{
    public class Summary
    {
        private readonly Dictionary<string, string> _summary = new();

        public bool ContainsErrors => this._summary.Count > 0;
        
        public void AddError(string key, string message)
        {
            this._summary.Add(key, message);
        }

        public void AddError(string message)
        {
            this._summary.Add(Guid.NewGuid().ToString(), message);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach(var summaryItem in this._summary)
                sb.AppendLine($"{summaryItem.Value}");

            return sb.ToString();
        }
    }
}