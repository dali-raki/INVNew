using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INV.Domain.Entities.Budget;
using INV.Domain.Shared;

namespace INV.App.Budgets
{
    public interface IBudgetService
    {
        ValueTask<Result> AddArticle(Article article);

        ValueTask<Result<List<Article>>> GetAllArticles();

        ValueTask<Result<Article>> GetArticlesByCodeArticle(int codeArticle);

        ValueTask<Result<List<Article>>> GetArticlesByCodeChapter(int codeChapter);

        ValueTask<Result> AddChapter(Chapter chapter);

        ValueTask<Result<List<Chapter>>> GetAllChapitres();

        ValueTask<Result<Chapter>> GetChapterByCode(int codeChapter);
    }
}