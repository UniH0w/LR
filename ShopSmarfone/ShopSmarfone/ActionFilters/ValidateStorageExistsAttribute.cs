using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ShopSmarfone.ActionFilters
{
    public class ValidateStorageExistsAttribute: IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        public ValidateStorageExistsAttribute(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var trackChanges = (method.Equals("PUT") || method.Equals("PATCH")) ? true : false;
            var ProductId = (Guid)context.ActionArguments["ProductId"];
            var product = await _repository.Product.GetProductsAsync(ProductId, false);
            if (product == null)
            {
                _logger.LogInfo($"Product with id: {ProductId} doesn't exist in the database.");
                return;
                context.Result = new NotFoundResult();
            }
            var id = (Guid)context.ActionArguments["id"];
            var storage = await _repository.Storage.GetStorageAsync(ProductId, id, trackChanges);
            if (storage == null)
            {
                _logger.LogInfo($"Storage with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("storage", storage);
                await next();
            }
        }
    }
}
