using Web_Test_II_DAL.Entityes;
using Web_Test_II_Interfaces;

namespace Web_Test_II.Services.Interfaces
{
    internal interface IAddData
    {
        bool AddTestAndQuestion(Test test, List<Question> questions);
    }
}
