﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizServer.Model.Dtos;

namespace QuizServer.Dal
{
    public interface IQuestionTypeDal
    {
        List<QuestionType> GetAllQuestionTypes();
    }
}
