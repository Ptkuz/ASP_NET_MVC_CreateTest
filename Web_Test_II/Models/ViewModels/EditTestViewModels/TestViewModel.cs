using Web_Test_II_DAL.Entityes;

namespace Web_Test_II.Models.ViewModels.EditTestViewModels
{
    public class TestViewModel
    {
        public List<Test> Tests { get; set; }
        public List<int> CountQuest { get; set; }


        public TestViewModel() { }
        public TestViewModel(List<Test> tests, List<int> countQuest)
        {
            Tests = tests;
            CountQuest = countQuest;
        }
    }
}
