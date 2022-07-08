using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Web_Test_II.Models.ViewModels.DataOperationsViewModels;
using Web_Test_II_DAL.Entityes;
using Web_Test_II_Interfaces;

namespace Web_Test_II.Controllers
{
    public class DataOperationsController : Controller
    {
        private readonly ILogger<CreateTestController> _logger;

        private readonly IRepository<Position> _positionRepository;
        private readonly IRepository<Group> _groupRepository;





        public DataOperationsController(
            ILogger<CreateTestController> logger,
            IRepository<Position> positionRepository,
            IRepository<Group> groupRepository)
        {
            _logger = logger;
            _positionRepository = positionRepository;
            _groupRepository = groupRepository;

        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult ViewGroups()
        {
            var groups = _groupRepository.Items.ToList();
            GroupsViewModel model = new GroupsViewModel(groups);
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult ViewPositions()
        {
            var positions = _positionRepository.Items.ToList();
            PositionsViewModel model = new PositionsViewModel(positions);
            return View(model);
        }



        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddGroup(GroupsViewModel model)
        {
            try
            {
                Group group = new Group();
                group.Name = model.Group.Name;
                await _groupRepository.AddAsync(group);
                return RedirectToAction("ViewGroups", "DataOperations");
            }
            catch (SqlException)
            {
                return RedirectToAction("ViewPositions", "DataOperations");
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddPosition(PositionsViewModel model)
        {
            try
            {
                Position position = new Position();
                position.Name = model.Position.Name;
                await _positionRepository.AddAsync(position);
                return RedirectToAction("ViewPositions", "DataOperations");
            }
            catch (Exception ex)
            {
                return RedirectToAction("ViewPositions", "DataOperations");
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditGroup(int id, string parameterName)
        {

            var group = await _groupRepository.GetAsync(id);
            group.Name = parameterName;
            await _groupRepository.UpdateAsync(group);
            return RedirectToAction("ViewGroups", "DataOperations");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditPosition(int id, string parameterName)
        {

            var position = await _positionRepository.GetAsync(id);
            position.Name = parameterName;
            await _positionRepository.UpdateAsync(position);
            return RedirectToAction("ViewPositions", "DataOperations");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            try
            {
                await _groupRepository.RemoveAsync(id);
                return RedirectToAction("ViewGroups", "DataOperations");
            }
            catch (SqlException)
            {
                return RedirectToAction("ViewGroups", "DataOperations");
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeletePosition(int id)
        {
            try
            {
                await _positionRepository.RemoveAsync(id);
                return RedirectToAction("ViewPositions", "DataOperations");
            }
            catch (SqlException)
            {
                ViewBag.ErrorMessage = "При удалении произошла ошибка, попробуйте еще раз";
                return RedirectToAction("ViewPositions", "DataOperations");
            }

        }

    }
}