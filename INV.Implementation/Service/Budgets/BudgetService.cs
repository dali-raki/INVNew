using INV.App.Budgets;
using INV.Domain.Entities.Budget;
using INV.Domain.Shared;
using INV.Infrastructure.Storage.Budget;

namespace INV.Implementation.Service.Budgets;

public class BudgetService(IBudgetStorage budgetStorage) : IBudgetService
{
    public async ValueTask<Result> AddArticle(Article article)
    {
        try
        {
            var result = await budgetStorage.InsertArticle(article);
            return Result.Success(result);
        }
        catch (Exception e)
        {
            return Error.Exception(e);
        }
    }

    public async ValueTask<Result<List<Article>>> GetAllArticles()
    {
        try
        {
            var result = await budgetStorage.SelectAllArticles();
            return Result.Success(result);
        }
        catch (Exception e)
        {
            return Error.Exception(e);
        }
    }

    public async ValueTask<Result<Article>> GetArticlesByCodeArticle(int codeArticle)
    {
        try
        {
            var result = await budgetStorage.SelectArticlesByCodeArticle(codeArticle);
            return Result.Success(result);
        }
        catch (Exception e)
        {
            return Error.Exception(e);
        }
    }

    public async ValueTask<Result<List<Article>>> GetArticlesByCodeChapter(int codeChapter)
    {
        try
        {
            var result = await budgetStorage.SelectArticlesByCodeChapter(codeChapter);
            return Result.Success(result);
        }
        catch (Exception e)
        {
            return Error.Exception(e);
        }
    }

    public async ValueTask<Result> AddChapter(Chapter chapter)
    {
        try
        {
            var result = await budgetStorage.InsertChapter(chapter);
            return Result.Success(result);
        }
        catch (Exception e)
        {
            return Error.Exception(e);
        }
    }

    public async ValueTask<Result<List<Chapter>>> GetAllChapitres()
    {
        try
        {
            var result = await budgetStorage.SelectAllChapitres();
            return Result.Success(result);
        }
        catch (Exception e)
        {
            return Error.Exception(e);
        }
    }

    public async ValueTask<Result<Chapter>> GetChapterByCode(int codeChapter)
    {
        try
        {
            var result = await budgetStorage.SelectChapterByCode(codeChapter);
            return Result.Success(result);
        }
        catch (Exception e)
        {
            return Error.Exception(e);
        }
    }
}