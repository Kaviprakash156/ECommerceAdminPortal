using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerceAdminPortal.Filters;

public class ActivityLoggingPageFilter
    : IPageFilter
{
    private readonly ILogger<
        ActivityLoggingPageFilter> _logger;

    public ActivityLoggingPageFilter(
        ILogger<ActivityLoggingPageFilter> logger)
    {
        _logger = logger;
    }

    public void OnPageHandlerExecuting(
        PageHandlerExecutingContext context)
    {
        _logger.LogInformation(
            "Page Accessed: {Page}",
            context.ActionDescriptor.DisplayName);
    }

    public void OnPageHandlerExecuted(
        PageHandlerExecutedContext context)
    {
    }

    public void OnPageHandlerSelected(
        PageHandlerSelectedContext context)
    {
    }
}