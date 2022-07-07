using Web_Test_II_DAL.Entityes;

namespace Web_Test_II.Models.ViewModels.DataOperationsViewModels
{
    public class PositionsViewModel
    {
        public List<Position> Positions { get; set; }

        public Position Position { get; set; }

        public PositionsViewModel() { }

        public PositionsViewModel(List<Position> positions) 
        { 
            Positions = positions;
        }
    }
}
