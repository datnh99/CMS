using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_SU21_BE.Services
{
    interface ExcelService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exportData"></param>
        /// <param name="_sheet"></param>
        void WriteData<T>(List<T> exportData, ISheet _sheet, List<string> _headers);
    }
}
