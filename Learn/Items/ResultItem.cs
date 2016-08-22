using System.Collections.ObjectModel;

namespace Learn.Items
{
    public class ResultItem
    {

        public string QuestionString { get; set; }
        public string QuestionImagePath { get; set; }
        public int ErrorCount { get; set; }
        
        // use string for this so no need deal with 0.0000000001 problem
        public string AnswerSpeed { get; set; }
        
    }

    public class FullResult
    {
        public ObservableCollection<ResultItem> ResultList = new ObservableCollection<ResultItem>();

        // based on list dunno player max combo
        public int MaxCombo { get; set; }

    }

}
