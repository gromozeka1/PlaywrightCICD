﻿using Microsoft.Playwright;

namespace TestFramework.Driver
{
    public interface IPlaywrightDriver
    {
        Task<IBrowser> Browser { get; }
        Task<IBrowserContext> BrowserContext { get; }
        Task<IPage> Page { get; }
        Task<IAPIRequestContext> ApiRequestContext { get; }
    }
}