using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class SendSmsResult
    {
        public SendSmsResult()
        {
        }

        public SendSmsResult(int ResultCode)
        {
            this.ResultCode = ResultCode;
        }

        public int ResultCode { get; set; }
        public string ResultDescription { get; set; }
        public string Originator { get; set; }
        public long? TransactionId { get; set; }
        public bool IsSuccess
        {
            get { return ResultCode == 1; }
        }

    }
}
