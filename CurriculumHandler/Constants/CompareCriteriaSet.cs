using CurriculumHandler.Models;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace CurriculumHandler.Constants
{
    public static class CompareCriteriaSet
    {
        public static IList<AttributeMapping> GetCriterias => new List<AttributeMapping>()
        {
            new AttributeMapping
            {
                AttributeName = "Назва навчальних предметів та навчальних доручень",
                Offset1 = 2,
                Offset2 = 2,
                Offset3 = 4,
            },
            new AttributeMapping
            {
                AttributeName = "Курс",
                Offset1 = 3,
                Offset2 = 3,
                Offset3 = 6,
            },
            new AttributeMapping
            {
                AttributeName = "Заг. кільк. год. на семестр",
                Offset1 = 8,
                Offset2 = 9,
                Offset3 = 10,
            },
            new AttributeMapping
            {
                AttributeName = "Лекції_П",
                Offset1 = 10,
                Offset2 = 11,
                Offset3 = 11,
            },
            new AttributeMapping
            {
                AttributeName = "Лабораторні заняття_П",
                Offset1 = 11,
                Offset2 = 12,
                Offset3 = 12,
            },
            new AttributeMapping
            {
                AttributeName = "Практ. зан., семінари_П",
                Offset1 = 12,
                Offset2 = 13,
                Offset3 = 13,
            },
        };
    }
}
