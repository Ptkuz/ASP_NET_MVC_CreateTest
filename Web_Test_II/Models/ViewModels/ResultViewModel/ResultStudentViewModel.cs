using Web_Test_II_DAL.Entityes;
using Web_Test_II_DAL.Models;

namespace Web_Test_II.Models.ViewModels.ResultViewModel
{
    public class ResultStudentViewModel
    {
        public IQueryable<Result> Results { get; set; }
        public IQueryable<GroupResultsStudents> GroupResultsStudents { get; set; }
        public int QuestionsTest { get; set; }


        public ResultStudentViewModel() { }
        public ResultStudentViewModel(IQueryable<Result> results)
        {
            Results = results;
        }

        public ResultStudentViewModel(IQueryable<GroupResultsStudents> groupResultsStudents)
        {
            GroupResultsStudents = groupResultsStudents;
        }
    }
}
