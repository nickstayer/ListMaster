using System;
using ExceLib;

namespace ListMaster
{
    class Enviroment
    {
        public Browser Browser { get; set; }
        public ExcelApp ExcelApp { get; set; }
        public IProgress<int> RowProgress { get; set; }
        public IProgress<string> ExecProgress { get; set; }
        public IProgress<int> RamainsRowsProgress { get; set; }
        public IProgress<Browser> BrowserProgress{ get; set; }
        public FileParser FileParser { get; set; }
        public int StartRow { get; set; }
    }
}
