using Web_Test_II_DAL.Entityes;

namespace Web_Test_II.Models.ViewModels.ResultViewModel
{
    public class ResultStudentViewModel
    {
        public IQueryable<Result> Results { get; set; }


        public ResultStudentViewModel() { }
        public ResultStudentViewModel(IQueryable<Result> results)
        {
            Results = results;
        }
    }
}
