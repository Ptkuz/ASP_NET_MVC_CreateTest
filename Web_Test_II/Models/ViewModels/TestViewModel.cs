using Web_Test_II_DAL.Entityes;

namespace Web_Test_II.Models.ViewModels
{
    public class TestViewModel
    {
        public IQueryable<Test> Tests { get; set; }


        public TestViewModel() { }
        public TestViewModel(IQueryable<Test> tests)
        {
            Tests = tests;
        }
    }
}
