using LabelMaker.Core;

namespace LabelMaker.API.Entites
{
   public class DocumentQuery
    {
        public string Order { get; set; }

        public AppSettings AppSettings { get; set; }

        public string[] LabelContents { get; set; }
    }
}
