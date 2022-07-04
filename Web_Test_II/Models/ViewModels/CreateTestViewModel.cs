﻿using System.ComponentModel;
using Web_Test_II_DAL.Entityes;

namespace Web_Test_II.Models.ViewModels
{
    public class CreateTestViewModel
    {
        [DisplayName("Название теста")]
        public Test Test { get; set; }

        [DisplayName("Список вопросов")]
        public List<string> Questions { get; set; }

        public CreateTestViewModel()
        {

        }
    }
}
