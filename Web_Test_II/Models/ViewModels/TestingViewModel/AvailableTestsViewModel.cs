using Web_Test_II_DAL.Entityes;

namespace Web_Test_II.Models.ViewModels.TestingViewModel
{
    public class AvailableTestsViewModel
    {
        public List<Test> Tests { get; set; }
        public List<int> CountQuest { get; set; }

        public AvailableTestsViewModel() { }

        public AvailableTestsViewModel(List<Test> tests, List<int> countQuest)
        {
            Tests = tests;
            CountQuest = countQuest;
        }
    }
}
