﻿using Microsoft.AspNetCore.Mvc;

namespace CarRental.Common;

public class AppResultOptions
{
    private Func<AppResultException, IActionResult> _resultFactory = default!;

    public Func<AppResultException, IActionResult> ResultFactory
    {
        get => _resultFactory;
        set => _resultFactory = value ?? throw new ArgumentNullException(nameof(value));
    }
}
