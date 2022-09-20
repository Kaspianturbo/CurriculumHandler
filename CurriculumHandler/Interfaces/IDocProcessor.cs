using CurriculumHandler.Models;

namespace CurriculumHandler.Interfaces
{
    public interface IDocProcessor
    {
        Report Process(Doc1 doc1, Doc2 doc2, Doc3 doc3);
    }
}
